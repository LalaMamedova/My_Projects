import { Col, Row } from "react-bootstrap";
import ProductPrice from "./ProductPrice";
import "../../css/recomendationproduct.css"
import useBasketFunctions from "../../functions/basketFunctions";
import useProductsFunctions from "../../functions/productsFunctions";

const ProductRecomendations  = ({product})=>{
  
    const {toBasketHandler} = useBasketFunctions();
    const {selectedProduct} = useProductsFunctions();

    return(
        <Col style={{cursor:"poiner"}} onClick={()=>selectedProduct(product.id)} 
        className="recomendation-div d-flex">
            
            <div  className="d-flex">
            <img  src={
                product.productsImg &&
                product.productsImg.length !== 0
                ? product.productsImg[0].imagePath
                : "https://st4.depositphotos.com/14953852/24787/v/450/depositphotos_247872612-stock-illustration-no-image-available-icon-vector.jpg"}/>
           
            <div className="d-block">
                <span>{product.name}</span>
                <ProductPrice price={product.price}></ProductPrice>
                <button onClick={()=>toBasketHandler(product)} 

                    id="to-basket-btn" className="shop-card-btn">
                    {"To basket "} <i class="fas fa-shopping-basket"></i>
                </button>
            </div>

            </div>

        </Col>
    )
}

export default  ProductRecomendations;