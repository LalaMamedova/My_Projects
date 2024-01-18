import { useContext, useEffect } from "react";
import { Context } from "../..";
import { observer } from "mobx-react-lite";
import { Col, Container, Row } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { MAIN_PAGE, PURCHASE, PURCHASED_PRODUCTS } from "../utilits/constPath";
import PurchaseProducts from "../components/modals/PurchaseProducts";
import ProductPrice from "../components/modals/ProductPrice";

const Basket = observer(() => {
    const { user,userProduct } = useContext(Context);
    const navigation = useNavigate();
  
    useEffect(() => {
      const storedBasketString = JSON.parse(localStorage.getItem("basket")) || [];
      user.setBasket(storedBasketString) 
    }, []);

    function removeAllHandler(){
      user.setBasket([]);
      localStorage.removeItem("basket");
    }
  
    return (
      <Container className="d-flex mt-4">
        <Row className="col-12">

          <Col className="text-center">
            <button id="product-info-btn" onClick={()=>navigation(PURCHASED_PRODUCTS)}>
            <i class="fa fa-credit-card-alt" aria-hidden="true"></i>
              {" Purchased Products"}
              </button>
          </Col>

          {user.basket && user.basket.length > 0 ? (
            <div className="d-block col-12">
              {user.basket.map(device => (
                <PurchaseProducts key={device.id} product={device} isPurchase={false}/>
              ))}
              <Row style={{textAlign:"center",color:"#DF81FF"}} className="mt-4 mb-4 d-block">
                
              <ProductPrice price={`Total ${userProduct.totalSum}`}></ProductPrice>

                <div className="mt-2">
                  <button onClick={()=>(navigation(PURCHASE))} id="product-info-btn">Purchase</button>
                  <button onClick={removeAllHandler} className="remove-from-basket">Remove all</button>
                </div>
              </Row>
            </div>
          )
          : (
            <div>
              <h1>Empty</h1>
              <button style={{fontSize:"35px"}} id="product-info-btn" 
                onClick={() => navigation(MAIN_PAGE)}>Go to shopping</button>
            </div>
          )}
        </Row>
      </Container>
    );
  });
  
  
  export default Basket;
  