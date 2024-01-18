import { useNavigate } from "react-router-dom";
import { getProductRecomendation, getProductById, getUserProducts } from "../../http/deviceRequest/deviceGetRequest";
import { useContext, useState } from "react";
import { Context } from "../..";
import { PRODUCT } from "../utilits/constPath";
import { useCookies } from "react-cookie";
import { likedProductJson} from "../../AdminPath/staticJsonFiles/deviceStaticJson";
import { likedProductPost } from "../../http/deviceRequest/devicePostRequest";
import { NotificationManager } from 'react-notifications';
import { deleteLikedProduct } from "../../http/deviceRequest/deviceDeleteRequest";

const useProductsFunctions = () => {
  
    const navigation = useNavigate();
    const {userProduct,user} = useContext(Context);
    const [cookies,setCookies] = useCookies();
    const [likedProductId,setLikedProductId] = useState();

    const addToViewedProducts = (id) => {
      let viewedProductArray = cookies["viewedProducts"];
    
      if (viewedProductArray !== undefined) {    
        const existViewedProduct = viewedProductArray.some((viewedProductId) => viewedProductId === id);
    
        if (!existViewedProduct) {
          viewedProductArray.push(id);
          setCookies("viewedProducts", viewedProductArray);
        }
      } else {
        setCookies("viewedProducts", [id]); 
      }
    };
    

      const selectedProductHandler = async (deviceId) => {
        try {
          const data = await getProductById(deviceId);
          if (data) {
          
          userProduct.setSelectedProduct(data);
          addToViewedProducts(deviceId);

          await getRecomendationHandler();
          navigation(PRODUCT + "/" + deviceId);
          
        } else {
          console.error(`No data found for device with ID: ${deviceId}`);
        }
      } catch (error) {
        console.error(`Error fetching product with ID ${deviceId}:`, error);
      }
    };
  
    const setPagginations=(products)=>{
      userProduct.setProduct(products.items);
      userProduct.setTotalPages(products.totalPages)
      userProduct.setTotalItems(products.totalItemCount)
    }

    async function getRecomendationHandler(){            

      const viewedProduct = cookies["viewedProducts"];
      await getProductRecomendation(userProduct.selectedProduct.subCategory.categoryId,
                                    userProduct.selectedProduct.id, viewedProduct)
      .then(data => userProduct.setRecomendationProducts(data));
  }

  function isProductLikedHandler(deviceId){

    const likedProductsInCookies = cookies["userLikedProducts"] || []
    user.userInfo.products = likedProductsInCookies;

    const existLikedProduct = user.userInfo.products.find(x=>x.productId === deviceId);    
    return existLikedProduct !== undefined ? existLikedProduct.id : 0;
   
  }

  async function addLikedProductHandler (deviceId) {
    const exist =  isProductLikedHandler(deviceId);

    if(exist === 0){
      const product = likedProductJson(deviceId, user.userInfo.id);

      await likedProductPost(product)
      .then(
        data =>{
          user.userInfo.products.push(data);
          setCookies("userLikedProducts", user.userInfo.products);
        });
    }
  }

   async function removeLikedProductHandler (deviceId, likedProductId) {
    const exist =  isProductLikedHandler(deviceId);

    if(exist!==0){
      await deleteLikedProduct(likedProductId,user.userInfo.id);
      await getUserProducts(user.userInfo.id)
      .then(
        data=>{
          setCookies("userLikedProducts",data);
          user.userInfo.products = data  || []
        });
    }
      
  }

  function setLikedProductHandler(selectedProduct) {
    setLikedProductId(isProductLikedHandler(selectedProduct.id));
  }
    
async function removeOrAddHandler(selectedProduct){
    if(user.isAuth){

      setLikedProductHandler(selectedProduct);

        if(likedProductId === 0){
            await addLikedProductHandler(selectedProduct.id)
        }else{
            await removeLikedProductHandler(selectedProduct.id,likedProductId)
        }

    }else{
        NotificationManager.warning(`Please, sign in for like`, 'Warning');
    }
}

  return { selectedProductHandler,setPagginations,getRecomendationHandler,
            addLikedProductHandler,isProductLikedHandler,removeLikedProductHandler,
            removeOrAddHandler,setLikedProductHandler,likedProductId};
  };
  
  export default useProductsFunctions;