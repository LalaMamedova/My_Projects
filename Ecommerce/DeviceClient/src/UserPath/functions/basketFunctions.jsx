import { useContext } from "react";
import { Context } from "../..";
import { NotificationManager } from 'react-notifications';
import { toJS } from "mobx";


const useBasketFunctions = () => {
    const { user,userProduct } = useContext(Context);

    const totalSumHandler=()=>{
      const totalSum = user.basket.reduce((acc, product) => acc + (product.count * product.price), 0);
      userProduct.setTotalSum(totalSum);
    }
  
    function addBasketToLocalStorage(){
      const basketString = JSON.stringify(toJS(user.basket));
      if (basketString.length > 0) {
        localStorage.setItem("basket", basketString);
      }
    }

  function toBasketHandler(device){
    const basket = localStorage.getItem("basket");

    if(basket !==null){
        let item = user.basket.find(items => items.id===device.id)
        
        if(item===undefined ||item===null ){
            user.setBasket([...user.basket, device]); 
        }else{
            user.basket.find(items => items.id===device.id).count+=1;
        }
    }else{
        user.pushBasket(device);
    }
    
    addBasketToLocalStorage();
    NotificationManager.success(`${device.name} was added to your basket`, 'Success', {
        onClick: () => NotificationManager.remove()
      });
    }

  return {totalSumHandler,toBasketHandler,addBasketToLocalStorage}
}
  
export default useBasketFunctions;