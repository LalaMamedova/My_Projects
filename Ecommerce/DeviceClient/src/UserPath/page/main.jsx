import React, { useContext, useEffect } from "react";
import { Context } from "../..";
import { getNewestProducts, getCategoryByName, getProductsByCategory } from "../../http/deviceRequest/deviceGetRequest";
import ImageGallery from "react-image-gallery";
import ProductList from "../components/ProductList";
import { useNavigate } from "react-router-dom";
import { SHOP } from "../utilits/constPath";
import useProductsFunctions from "../functions/productsFunctions";
import { observer } from "mobx-react-lite";
import "../css/shop.css";

const MainPage = observer(() => {
  
  const { userProduct } = useContext(Context);
  const {setPagginations} = useProductsFunctions()
  const navigation = useNavigate();

  useEffect(() => {
    userProduct.setSelectedCategory(null);

    async function fetch(){
      await getNewestProducts()
      .then((data) => userProduct.setProduct(data.items));
    }
    fetch();
  }, []);

  async function getCategoryByNameHandler(name){
    const category = await getCategoryByName(name);

    userProduct.setSelectedCategory(category.name)
    userProduct.setProduct([]);

    if (category!==null) {
      
      const products = await getProductsByCategory(category.id,1);
      setPagginations(products);
      navigation(SHOP + "/" + category.name)

    } else {
      console.error("Invalid or missing subCategories in data");
    }
  }
  const images = [
    {
      original: "https://wallpapers.com/images/featured/laptop-murjp1nk4lp1idlt.jpg",
      button: {
        onClick: async () => {await getCategoryByNameHandler("Portable laptop");},
        theme:"Portable laptop",
      },
    },
    
    {
      original: "https://www.androidauthority.com/wp-content/uploads/2022/02/Samsung-Galaxy-Tab-S8-with-S-Pen.jpg",
      button: {
        onClick: async () => { await getCategoryByNameHandler("Tablets");},
        theme:"Tablets",
      },
    },
    {
      original: "https://i.etsystatic.com/34157568/r/il/a8db7d/4169466285/il_fullxfull.4169466285_cvis.jpg",
      button: {
        theme:"Desktops",
        onClick: async () => { await getCategoryByNameHandler("Desktops");},
      },
    },
  ];

  const customRenderItem = (item) => {
    return (
      <div className="custom-image-gallery-item">
        <img key={item.original} src={item.original} alt={item.original} />
  
        {item.button && (
          <div className="custom-gallery-field">
            <button className="submit-post-btn" onClick={item.button.onClick}>
              {`Show me more ${item.button.theme}`}
            </button>
          </div>
        )}
      </div>
    );
  };

  return (
    <div>
      <div >
        <ImageGallery
          showPlayButton={false}
          showFullscreenButton={false}
          items={images}
          renderItem={customRenderItem}
        />
      </div>

      <div style={{alignItems:"center", textAlign:"center"}} className="mt-4">
      <h2>Recomendation</h2>

       <ProductList products = {userProduct.product} />

      </div>
    </div>
  );
});

export default MainPage;
