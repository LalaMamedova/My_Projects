import "../css/main.css"
import {  Container, Row } from "react-bootstrap";
import { useContext, useEffect, useState } from "react";
import { getAllCategory } from "../../http/deviceRequest/deviceGetRequest";
import { Context } from "../..";
import { MAIN_ADMIN_PAGE } from "../utilits/constPath";
import { useNavigate } from "react-router-dom";
import Categories from "../components/Categories";
import SubCategories from "../components/SubCategories";
import Products from "../components/Products";
import AddSubCategory from "../components/AddSubCategory";
import AddProduct from "../components/AddProduct";
import { observer } from "mobx-react-lite";
import {  useCookies } from "react-cookie";
import Brands from "../components/Brands";

const MainPage = observer(()=>{
    const { user,adminProduct,breadPath } = useContext(Context);
    const [adminOption,setAdminOption] = useState();
    const navigation = useNavigate();
    const [cookies, setCookie, removeCookie] = useCookies();

    function optionHandler(option)
    {
      setAdminOption(option)
      navigation(MAIN_ADMIN_PAGE + "/" + option);
    }

    function signOutHandler(){
      user.setIsAuth(false);
      user.setIsAdmin(false)
      removeCookie(["user"]);
  }
    useEffect(() => {
        try {
          async function fetch(){
           await getAllCategory()
            .then(data => adminProduct.setCategory(data));
            
            adminProduct.category.length>0 
            ? adminProduct.setSelectedCategory(adminProduct.category[0].id)
            : adminProduct.setSelectedCategory(0);
          }

          
            breadPath.setPaths([
              { name: 'AdminPanel', url: MAIN_ADMIN_PAGE },
            ]);

            const locationPath = window.location.href.split("/");

            if(locationPath.length>5){
              setAdminOption(locationPath[locationPath.length]);              
            }
            fetch();

          } catch (error) {}
        }, []);
      

      return (
        <div>
          {user.isAdmin? (
            <div className="d-flex">
              <Row className="left-side ">

                <div className="admin-info-div ">
                  <img src="https://www.uab.edu/humanresources/home/images/RecordsAdmin/RecordsIcon_Oracle_SelfService.png" alt="admin-avatar" />
                  <p className="mt-4">{user.userInfo.userName}</p>
                  <p>{user.userInfo.email}</p>
                </div>
      
                <div className="hr"></div>

                <Row className="buttons ml-4">
                  <button onClick={() => optionHandler("add-subcategory")} className="left-side-btns">
                    Add Subcategory
                  </button>

                  <button onClick={() => optionHandler("add-product")} className="left-side-btns">
                    Add Product
                  </button>
      
                  <div style={{width:"93%"}} className="hr"></div>

                <div className="d-block-inline">
                    <button onClick={() => optionHandler("categories")} className="left-side-btns">
                      Categories
                    </button>
                </div>

                  <button onClick={() => optionHandler("products")} className="left-side-btns">
                    Products
                  </button>
                  
                  <div className="d-block-inline">
                  <button onClick={() => optionHandler("brands")} className="left-side-btns">
                    Brands
                  </button>
                  </div>
                </Row>
                
                  <button style={{position:"absolute", bottom:"40px",left:"25px"}}  onClick={signOutHandler} className="left-side-btns">
                    Logout
                  </button>
              </Row>

      
              <Container className="center-admin-side ">
                {adminOption === "add-subcategory" && <AddSubCategory />}
                {adminOption === "add-product" && <AddProduct />}
                {adminOption === "categories" && <Categories adminOption={setAdminOption} />}
                {adminOption === "subcategories" && <SubCategories />}
                {adminOption === "products" && <Products />}
                {adminOption === "brands" && <Brands />}

              </Container>
            </div>
          ) : (
            <div>
              You are not admin
            </div>
          )}
        </div>
      );
      
})
export default MainPage;