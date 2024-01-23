import { observer } from "mobx-react-lite";
import { useContext, useEffect, useRef, useState } from "react";
import { Context } from '../..';
import { getSubСategoryById, getСategoryById,getAllBrands } from "../../http/deviceRequest/deviceGetRequest";
import { productPost } from "../../http/deviceRequest/devicePostRequest";
import { Col, Container, Row } from "react-bootstrap";
import { toJS } from "mobx";
import { productCharsJson, productJson } from "../staticJsonFiles/deviceStaticJson";
import { MAIN_ADMIN_PAGE } from "../utilits/constPath";
import { NotificationContainer, NotificationManager } from 'react-notifications';
import useProductsFunctions from "../functions/productsFunctions";
import "../css/main.css"

const AddProduct =observer(()=>{
    
    const {adminProduct,breadPath } = useContext(Context);
    const [productImgs,setProductImgs] = useState([]);
    const [productOriginalImgs,setProductOriginalImgs] = useState([]);
    const [productName,setProductName] = useState();
    const [productPrice,setProducPrice] = useState();
    const {resizeAndAddToProductImgs} = useProductsFunctions(setProductImgs);
    const inputFile = useRef(null) 
    const [imgSize] = useState([]);
    let [selectBrand,setSelectBrand] = useState(1);

    useEffect( () => {
        try { 
            getAllBrands().then(data=>adminProduct.setBrand(data));
            adminProduct.setSelectedCategory(adminProduct.category[0]);
            if(adminProduct.category[0].subCategories.length>0){
                 getSelectedSubCategoryHandler(adminProduct.category[0].subCategories[0].id);
            }
            breadPath.setPaths([
                { name: 'AdminPanel', url: MAIN_ADMIN_PAGE },
                { name: "Products", url: MAIN_ADMIN_PAGE + '/add-product' },
              ]);
        } catch (error) { }
    }, []);

    function onButtonClick(){
        inputFile.current.click();
      };

    async function getSelectedCategoryHandler(e) { 
        const category = await getСategoryById(e);
        await adminProduct.setSelectedCategory(category);

        if(category.subCategories.length>0){
            await getSelectedSubCategoryHandler(category.subCategories[0].id)
        }
    }
    
    async function getSelectedSubCategoryHandler(e){ 
        await getSubСategoryById(e)
        .then(data=>adminProduct.setSelectedSubCategory(data));
    }

  
    
    function addProductImgHandler(e) {
        e.stopPropagation();
        const fileInput = e.target.files[0];
    
        if (fileInput) {
            imgSize.push(fileInput.size);
            const reader = new FileReader();
    
            reader.onload = function (event) {
                const fileData = event.target.result;
    
                if (productImgs.length < 6) {
                    setProductOriginalImgs(oldArray => [...oldArray, fileData]);
                    resizeAndAddToProductImgs(fileInput, true);
                }
            }
            reader.readAsDataURL(fileInput);
        }
    }
    
    
    function removeImgHandler(img,index,e) {
        e.stopPropagation();
        setProductImgs((current) => current.filter((imgs) => imgs !== img));
        imgSize.slice(index,1);

    }

    async function addCharsHandler(){
        adminProduct.setChars([])

        const charValues = document.querySelectorAll(".char-value");
        const subCategoryChars = adminProduct.selectedSubCategory.characteristics;

        for (let i = 0; i < subCategoryChars.length; i++) {
            const chardId = subCategoryChars[i].id;
            const value = charValues[i].value;
            const chars = productCharsJson(chardId,value)
            adminProduct.chars.push(chars);
        }       
    }
    async function addJsonImgs(){
        const allJsonImg = [];
        const allOriginalJsonImg = [];
        
        for (let i = 0; i < productImgs.length; i++) {
            const jsonImg = {"imagePath":  productImgs[i]}
            const originalJsonImg = {"originalImgPath":  productOriginalImgs[i]}

            allJsonImg.push(jsonImg);
            allOriginalJsonImg.push(originalJsonImg)
        }
    
        return {allJsonImg, allOriginalJsonImg};
    }
  
    
    async function addProduct(){
        await addCharsHandler();
        const {allJsonImg,allOriginalJsonImg} = await addJsonImgs();

        const product = productJson(productName,parseFloat(productPrice),parseInt(selectBrand),
                                    adminProduct.selectedSubCategory.id, allJsonImg, allOriginalJsonImg,
                                    toJS(adminProduct.chars))
                                    
        const sum = imgSize.reduce((accumulator, currentValue) => accumulator + currentValue,0,);
        
        if(sum<1000000){
            try {
                await productPost(product)
                .then(()=> NotificationManager.success('Success', 'Success') );
            } catch (error) {
                NotificationManager.error(error, 'Error');
            }
        }else{
            NotificationManager.error('Imgs size too big', 'Imgs sizes')
        }

    }
    
    return(
        <Container className="center-side-container mt-5">

            <Row className="d-block">
                <Col className="d-flex">

                <fieldset >
                    <legend>Category</legend>
                    <div className="select-div">
                    <select onChange={(e) => { getSelectedCategoryHandler(e.target.value) }}>
                        {
                            adminProduct.category && adminProduct.category.map(category => (
                                <option value={category.id}>{category.name}</option>
                                ))
                            }
                    </select>
                    </div>
                    </fieldset>

                    <fieldset>
                    <legend>SubCategory</legend>
                    <div className="select-div" >
                        <select onChange={(e) => { getSelectedSubCategoryHandler(e.target.value) }}>
                            {
                                adminProduct.selectedCategory.subCategories 
                                && adminProduct.selectedCategory.subCategories.map(subcategry => (
                                    <option value={subcategry.id}>{subcategry.name}</option>
                                    ))
                                }
                        </select>
                    </div>
                    </fieldset>

                </Col>

                <Col className="d-flex"> 
                <fieldset>
                    <legend>Brand</legend>
                    <div className="select-div">
                    <select onChange={(e)=>setSelectBrand(e.target.value)}>
                        {
                            adminProduct.brand  && adminProduct.brand.map(brand => (
                                <option value={brand.id}>{brand.name}</option>
                                ))
                        }
                    </select>
                    </div>
                </fieldset>
                </Col>

            </Row>

            <Row className="centeresed-div mt-4 d-block">
                <div >
                    <legend>Product</legend>
                    <input style={{width:"65%",marginRight:"40px"}} 
                    placeholder="Product name" 
                    onChange={(e)=>(setProductName(e.target.value))}></input>

                    <input style={{width:"30%"}} placeholder="Price"
                     onChange={(e)=>(setProducPrice(e.target.value))}></input>
                </div>
                    
                <div className="mt-5">
                    <h3 style={{position:"relative",left:"30%"}}>Characteristics</h3>
                        {
                            adminProduct.selectedSubCategory.characteristics && 
                            adminProduct.selectedSubCategory.characteristics.map(chars=>(
                                <Row className="mt-5">
                                    <Col>
                                        <h3>{chars.name}</h3>
                                    </Col>

                                    <Col>
                                        <input style={{width:"100%"}} className="char-value" placeholder="Value"></input>
                                    </Col>
                                
                                    <div className="hr"></div>
                                </Row>
                            ))
                        }
                        
                </div>
                <Row onClick={onButtonClick} className="add-img-div d-flex">
                {
                  productImgs &&
                  productImgs.map((img, index) => (
                    <div className="d-flex" key={index}>
                      <div className="d-block" >
                        <img src={img} alt={`Product Image ${index}`} onClick={(e) => e.stopPropagation()} />
                        <h4 className="text-center">{`${imgSize[index]} Kb`}</h4>
                        <div className="d-flex align-items-center">
                          <button className="close" 
                            onClick={(e) =>{ removeImgHandler(img,index,e) }}>
                            Remove
                          </button>
                        </div>
                      </div>
                    </div>
                  ))                  
                }
                {
                    productImgs.length===0  && 
                     <label onClick={(e)=> e.stopPropagation()} htmlFor="product"  
                         className="upload-img">Upload Imgs
                     </label>
                }
            

                <input
                    id="product"
                    type="file"
                    onChange={(e)=>addProductImgHandler(e)}
                    style={{ display: "none" }}
                    ref={inputFile}
                    accept=".jpg, .png, .jpeg, .gif|image" />

                </Row>
            </Row>
            
            <div className="mt-2">
                <button className="submit-post-btn" onClick={addProduct}>Submit</button>
            </div>

            <NotificationContainer></NotificationContainer>
        </Container>
    )
})
export default AddProduct;