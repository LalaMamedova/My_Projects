import { useContext, useEffect, useState } from "react";
import { Col, Container, Row } from "react-bootstrap";
import "../../css/purchaseproduct.css"
import { observer } from "mobx-react-lite";
import { toJS } from "mobx";
import { Context } from "../../..";
import useProductsFunctions from "../../functions/productsFunctions";
import useBasketFunctions from "../../functions/basketFunctions";
import ProductPrice from "./ProductPrice";


const PurchaseProducts  = observer(({product, isPurchase})=>{

    const {user,userProduct} = useContext(Context);
    const { selectedProduct } = useProductsFunctions();
    const {totalSumHandler,addBasketToLocalStorage} = useBasketFunctions();

    useEffect(() => {
        removeFromLocalStorage();
        totalSumHandler();
    }, []);

    function removeFromLocalStorage(){
        if(user.basket.length ===0){
            localStorage.removeItem("basket");
            user.setBasket([]);
        }
    }


    function setQuentityHandler(value){
        const quantity = parseInt(value);

        quantity>0
        ? product.count = quantity
        : product.count = 1
  
        user.basket.find(items => items.id === product.id).count = product.count;

        totalSumHandler();

        addBasketToLocalStorage();
    }

    function removeFromBasket(removeProduct){
        const products = user.basket.filter(item => item.id !== removeProduct.id);
        products.length > 0 ? user.setBasket(toJS(products)): user.setBasket([]);
      
        const basketString = JSON.stringify(toJS(user.basket));
          
        localStorage.setItem("basket", basketString);
        
        totalSumHandler();
        removeFromLocalStorage();
    }
  
    return(
    <Container className="purchase-product-div">
            <Col>
                <div className="d-flex ">
                <img onClick={()=>selectedProduct(product.id)} src={
                    product.productsImg &&
                    product.productsImg.length !== 0
                    ? product.productsImg[0].imagePath
                    : "https://st4.depositphotos.com/14953852/24787/v/450/depositphotos_247872612-stock-illustration-no-image-available-icon-vector.jpg"
                }/>

                {!isPurchase && <h4 className="mt-2">{product.name}</h4>}
                {
                    isPurchase &&
                    <div className="d-block"> 
                        <h4 className="mt-2">{product.name}</h4>
                        <ProductPrice price={product.price*product.count}></ProductPrice>
                        <button onClick={()=>removeFromBasket(product)} 
                        className="remove-from-basket mt-2"><span aria-hidden="true">&times;</span></button>
                    </div>
                }
                
                </div>
                {
                    !isPurchase ? 
                    (
                    <div className="d-block">
                        <div>
                        <ProductPrice price={product.price*product.count}></ProductPrice>
                        </div>
                        <div>
                        <input  value={product.count} type="number" 
                                onChange={(e)=>setQuentityHandler(e.target.value)}>
                        </input>
                        </div>
                        <div className="mt-4 mb-4 " style={{textAlign:"center", }}>
                            <button onClick={()=>removeFromBasket(product)} 
                            className="remove-from-basket">
                            Remove from basket</button>
                        </div>
                    </div>
                    )
                    :(<Col>
                        <span>{`Quantity: ${product.count}`}</span>
                    </Col>)
                }
            </Col>
           <div className="hr"></div>
    </Container>
    )
})

export default  PurchaseProducts;