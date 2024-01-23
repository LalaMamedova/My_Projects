import { useContext, useEffect, useState } from "react";
import { Col, Container, Row } from "react-bootstrap";
import { Context } from "../..";
import { observer } from "mobx-react-lite";
import { useNavigate } from "react-router-dom";
import { PURCHASE } from "../utilits/constPath";
import { reviewJson } from "../../AdminPath/staticJsonFiles/deviceStaticJson";
import { reviewPost } from "../../http/deviceRequest/devicePostRequest";
import { NotificationManager,NotificationContainer } from "react-notifications";
import Characteristics from "../components/modals/Characteristics";
import CharDescription from "../components/modals/CharDescription";
import Rating from "react-rating";
import ProductRecomendations from "../components/modals/ProductRecomendations";
import ProductPrice from "../components/modals/ProductPrice";
import useProductsFunctions from "../functions/productsFunctions";
import "../css/productInfo.css"

const ProductInfo = observer(() =>{

    const {userProduct,user} = useContext(Context);
    const [selectedImg, setSelectedImg] = useState();
    const [showCharDescription,setShowCharDescription] = useState(null);
    const selectedProduct = userProduct.selectedProduct;
    const navigation = useNavigate();
    const {getRecomendationHandler} = useProductsFunctions();
    const {  removeOrAddHandler,setLikedProductHandler,likedProductId} = useProductsFunctions();

    useEffect(() => {
        if(selectedProduct.originalImgs.length ===0){
            setSelectedImg("https://st4.depositphotos.com/14953852/24787/v/450/depositphotos_247872612-stock-illustration-no-image-available-icon-vector.jpg") 
        }else{
            setSelectedImg(selectedProduct.originalImgs[0].originalImgPath);
        }
        if (Object.keys(user.userInfo).length > 0) {
            setLikedProductHandler(selectedProduct);
        }
         getRecomendationHandler();
    }, [setLikedProductHandler]);


   

    function buyProduct(product){
        user.setPurchaseProduct(product);
        navigation(PURCHASE);
    }
    const handleRating = async(rate) => {

        const productId = selectedProduct.id;
        const userId = user.user.id;

        if(user.isAuth){
          const review = reviewJson(rate,userId,productId);
          console.log(review);
          await reviewPost(review)
              .catch(error=> NotificationManager.error(error, 'Error')
        );
       
       }else{
          NotificationManager.warning(`Please, sign in for rating`, 'Warning');
       }
    }
  
    return (
        <Container className="product-info-div mt-5">
            <Col className="top-part d-flex">

                <Col className="product-imgs d-block">
                    <img src={selectedImg}/>
                    <div className="bottom-imgs d-flex">

                        {
                            selectedProduct.productsImg 
                            && selectedProduct.originalImgs.length > 0 
                            && selectedProduct.originalImgs.map(img=>(
                                <div className="another-imgs">
                                    <img alt={img.originalImgPath} 
                                    onClick={()=>setSelectedImg(img.originalImgPath)} 
                                    key={img.id} src={img.originalImgPath}></img>
                                </div>
                            ))
                        }
                    </div>
                  
                </Col>

                <Col className="main-info-div col-8">
                    <h1>{selectedProduct.name}</h1>
                    <ProductPrice price={selectedProduct.price}></ProductPrice>

                <div className="mt-4">

                    <Rating 
                      emptySymbol="fa fa-star-o fa-2x" 
                      fullSymbol="fa fa-star fa-2x"
                      initialRating = {selectedProduct.productRating}
                      onClick={(value)=>handleRating(value)}
                      />
                      <span>{` (${selectedProduct.reviews.length})`}</span>
                </div>

                    <div className="btns d-flex mt-5">
                        <button onClick={()=>buyProduct( selectedProduct)}
                         id="product-info-btn" className="buy-btn">Buy</button>
                         
                        <button style={{color : likedProductId === 0 ? "white ":"#ff81dd" }} 
                          onClick={()=>removeOrAddHandler(selectedProduct)} 
                         id="product-info-btn"><i class="fa fa-heart" aria-hidden="true"></i>
                         </button>
                        <button id="product-info-btn"><i class="fa fa-balance-scale" aria-hidden="true"></i></button>
                    </div>
                </Col>
            </Col>

            <Col className="bottom-part mt-4 m-2">
                <h3 className="mb-4">Characteristics</h3>
                <Characteristics brand = {selectedProduct.brand} 
                characteristic = {selectedProduct.productСharacteristics}
                                 setShowCharDescription = {setShowCharDescription} />
            </Col>

            {
                showCharDescription &&
                <CharDescription description = {showCharDescription}
                                close = {setShowCharDescription}/>
            }

            <Col className="d-block mt-4 mb-3" >
                 <h3 className="text-center mb-3">Recomendation</h3>

                <Row >
                {
                    userProduct.recomendationProducts && 
                    userProduct.recomendationProducts.length>0 ? 
                    userProduct.recomendationProducts.map(product=>(
                        <ProductRecomendations product = {product}/>
                        )):
                        (
                            <h3 className="text-center ml-3 mb-3">No recomendation</h3>
                        ) 
                }
                </Row>
            </Col>
            <NotificationContainer/>
        </Container>
    )
})

export default ProductInfo;