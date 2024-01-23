import { Card } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import { useContext, useEffect, useState } from "react";
import { Context } from "../../..";
import { NotificationContainer, NotificationManager } from 'react-notifications';
import { reviewJson } from "../../../AdminPath/staticJsonFiles/deviceStaticJson";
import { reviewPost } from "../../../http/deviceRequest/devicePostRequest";
import Rating from "react-rating";
import ProductPrice from "./ProductPrice";
import useBasketFunctions from "../../functions/basketFunctions";
import useProductsFunctions from "../../functions/productsFunctions"
import "../../css/shop.css"
import 'react-notifications/lib/notifications.css';

const ProductCard = observer(({device})=>{

    const { user } = useContext(Context);
    const { toBasketHandler} = useBasketFunctions();
    const { selectedProductHandler, removeOrAddHandler,
            setLikedProductHandler,likedProductId} = useProductsFunctions();

    useEffect(() => {
    if (Object.keys(user.userInfo).length > 0) {
        setLikedProductHandler(device);
    }
    }, [setLikedProductHandler]); 

  
    const handleRating = async(rate) => {

        if(user.isAuth){
          const review = reviewJson(rate,user.userInfo.id, device.id);
          await reviewPost(review)
                .catch(error=> NotificationManager.error(error, 'Error')
          );
       
       }else{
          NotificationManager.warning(`Please, sign in for rating`, 'Warning');
       }
      }

    return(
        <Card key={device.id}>
            <span style={{color : likedProductId === 0 ? "#A3A3FF ":"#ff81dd" }} 
                onClick={()=>removeOrAddHandler(device)} 
                className="wish-list">
                <i className="fa fa-heart"></i>
            </span>

            <img
                onClick={()=>(selectedProductHandler(device.id))}
                    src={
                        device.productsImg &&
                        device.productsImg.length !== 0
                        ? device.productsImg[0].imagePath
                        : "https://st4.depositphotos.com/14953852/24787/v/450/depositphotos_247872612-stock-illustration-no-image-available-icon-vector.jpg"
                    }
                    alt="Product Image"
                />
                <div className="mt-2">
                    <h5>{device.name}</h5>
                    
                    <Rating className="mt-2"
                      emptySymbol="fa fa-star-o fa-xl" 
                      fullSymbol="fa fa-star fa-xl"
                      initialRating = {device.productRating}
                      onClick={(value)=>handleRating(value)}
                    />
                    <ProductPrice price={device.price}></ProductPrice>
                </div>

                <button onClick={()=>toBasketHandler(device)} 
                    id="to-basket-btn" className="shop-card-btn">
                    {"To basket"} <i class="fas fa-shopping-basket"></i>
                </button>
            <NotificationContainer />
        </Card>
    )
})

export default ProductCard;