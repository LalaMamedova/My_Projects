import { useContext, useState } from "react";
import { getProductsByCategory, getProductsBySubCategory, 
        getСategoryById,getSubСategoryById } from "../../../http/deviceRequest/deviceGetRequest";
import { Context } from "../../..";
import { useNavigate } from "react-router-dom";
import { SHOP } from "../../utilits/constPath";
import { observer } from "mobx-react-lite";
import useProductsFunctions from "../../functions/productsFunctions";

const CatalogItems = observer((props)=>{
    const [hoveredCategory, setHoveredCategory] = useState();
    const [hoveredSubCategory,setHoveredSubCategory] = useState();
    const {userProduct} = useContext(Context);
    const {setPagginations} = useProductsFunctions()
    const navigate = useNavigate();

    function mouseLeaveCategoryHandler(){
        setHoveredCategory(null); 
        setHoveredSubCategory(null);
    } 
    
    async function selectedCategoryHandler(e,category,isCategory){
        e.stopPropagation(); 
        userProduct.setCurrentPage(1);
        let products = [];

        if(isCategory){
            products = await getProductsByCategory(category.id, userProduct.currentPage);
             
            await getСategoryById(category.id)
            .then(data=>userProduct.setSelectedCategory(data))
            .then(data=>userProduct.setSelectedSubCategory(null))

        }else{
            products = await getProductsBySubCategory(category.id,userProduct.currentPage);
            await getSubСategoryById(category.id)
            .then(data=>userProduct.setSelectedSubCategory(data))
            .then(data=>userProduct.setSelectedCategory(null))
        }

        setPagginations(products);

        navigate(SHOP + "/" + category.name)
        props.setCatalogIsOpen(false)
        
    } 
    

    return(
        <ul>
            <li onMouseMove={()=>setHoveredCategory(props.category.id)}
                onMouseLeave={(mouseLeaveCategoryHandler)}
                onClick={(e)=>selectedCategoryHandler(e,props.category,true)}>
                    <img className="category-icon" src={props.category.icon}></img>
                    <span style={{color: props.category.id === hoveredCategory? "#DF81FF" :"white"}} className="category-name">
                        {props.category.name} {props.subcategory && props.subcategory.length !==0?" >":""}
                        
                    </span> 
                    
                <ul className="subcategory-ul">
                    {
                        hoveredCategory === props.category.id ?
                        props.subcategory.map(subcategory=>(
                        <div key={subcategory.id}  className="d-flex">
                            <img className="category-icon" src={subcategory.icon}></img>
                            <span style={{color: subcategory.id === hoveredSubCategory? "#DF81FF" :"white"}}
                                onMouseLeave={()=>setHoveredSubCategory(null)}
                                onMouseMove={()=>setHoveredSubCategory(subcategory.id)}
                                onClick={(e)=>selectedCategoryHandler(e,subcategory,false)}>
                                {subcategory.name}
                            </span>
                        </div>
                        ))
                        :<br></br>
                    }
                </ul>
            </li>
        </ul>    
    )
})

export default CatalogItems;