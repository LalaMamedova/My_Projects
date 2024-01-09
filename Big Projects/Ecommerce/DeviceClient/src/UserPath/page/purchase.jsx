import React,{ useContext, useState } from "react";
import { Context } from "../..";
import { Col, Container, Row } from "react-bootstrap";
import "../css/purchase.css"
import Cards from "react-credit-cards-2";
import "react-credit-cards-2/dist/es/styles-compiled.css";
import PurchaseProducts from "../components/modals/PurchaseProducts";
import { observer } from "mobx-react-lite";
import ProductPrice from "../components/modals/ProductPrice";
import { purchasedProductJson } from "../../AdminPath/staticJsonFiles/deviceStaticJson";
import { purchaseProductsPost } from "../../http/deviceRequest/devicePostRequest";
import { NotificationManager,NotificationContainer } from "react-notifications";

const Purchase = observer(()=>{
    const {user,userProduct} = useContext(Context)
    const [state, setState] = useState({
        number: "",
        name: "",
        expiry: "",
        cvc: "",
        name: "",
        focus: "",
      });

      function handleInputChange (e) {
        const { name, value } = e.target;
        setState((prev) => ({ ...prev, [name]: value }));
      };
      const handleInputFocus = (e) => {
        setState((prev) => ({ ...prev, focus: e.target.name }));
      };

      async function buyProductsHandler(){
        const userId = user.userInfo.id;
        const allPurchaseProducts = [];

        user.basket.forEach(product => {
          const purchaseProduct = purchasedProductJson
          (userId,product.id, product.price * product.count);

          allPurchaseProducts.push(purchaseProduct);
        });

        try {
          await purchaseProductsPost(allPurchaseProducts,userId)
          .then(()=>NotificationManager.success('Congratulations, your purchase was successful', 'Success'));

        } catch (error) {
          NotificationManager.success(error, 'Error')
        }
      }

      return (
        <Container className="purchase-container mt-4">
          <NotificationContainer></NotificationContainer>
        <Container className="d-flex">
        <Row className="purchase-div mt-4">
          <Cards 
            number={state.number}
            expiry={state.expiry}
            cvc={state.cvc}
            name={state.name}
            focused={state.focus}
            />
          <div className="mt-3">
            <form>
              <div className="mb-3">
                <input
                  type="number"
                  name="number"
                  className="form-control"
                  placeholder="Card Number"
                  value={state.number}
                  onChange={handleInputChange}
                  onFocus={handleInputFocus}
                  required
                  />
              </div>
              <div className="mb-3">
                <input
                  type="text"
                  name="name"
                  className="form-control"
                  placeholder="Name"
                  onChange={handleInputChange}
                  onFocus={handleInputFocus}
                  required
                />
              </div>
              <div className="row">
                <div className="col-6 mb-3">
                  <input
                    name="expiry"
                    className="form-control"
                    placeholder="Valid Thru"
                    pattern="\d\d/\d\d"
                    value={state.expiry}
                    onChange={handleInputChange}
                    onFocus={handleInputFocus}
                    required
                    />
                </div>
                <div className="col-6 mb-3">
                  <input
                    type="number"
                    name="cvc"
                    className="form-control"
                    placeholder="CVC"
                    pattern="\d{3,4}"
                    value={state.cvc}
                    onChange={handleInputChange}
                    onFocus={handleInputFocus}
                    required
                    />
                </div>
              </div>
            </form>
          </div>
        </Row>
      
        <Col className="ml-4">
          {
            user.basket.length >0 ? user.basket.map(product=>(
              <PurchaseProducts product ={product} isPurchase = {true} />
            ))
            :<h1>Empty</h1>
          }
        </Col>
        </Container>
        
        <Row style={{textAlign:"center",color:"#DF81FF"}} className="mt-4 mb-4 d-block">
          <ProductPrice price={`Total ${userProduct.totalSum}`}></ProductPrice>
          <button onClick={()=>buyProductsHandler()} style={{fontSize:"25px"}} className="mt-2" id="product-info-btn">BUY</button>
        </Row>
        </Container>
      );
})

export default Purchase;