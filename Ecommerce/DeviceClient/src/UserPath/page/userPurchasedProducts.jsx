import { useContext, useEffect, useState } from "react";
import { Card, Container, Row } from "react-bootstrap";
import { getUserAllPurchasedProducts } from "../../http/userRequest";
import { Context } from "../..";
import ProductPrice from "../components/modals/ProductPrice";

const UserPurchasedProducts = ()=>{

    const {user} = useContext(Context);
    const [userPuchasedProducts,setUserPuchasedProducts] = useState();

    useEffect(()=>{
        getUserAllPurchasedProducts(user.userInfo.id)
        .then(data=>setUserPuchasedProducts(data));
    },[]);

    return(
        <Container className="mt-4 text-center">
            <Row>
            {userPuchasedProducts && userPuchasedProducts.map(product=>(
                <Card style={{width:"350px", height:"200px"}} className="bg-dark mr-2 mt-2">
                    <Card.Img  height={100}  src={
                        product.product.productsImg &&
                        product.product.productsImg.length !== 0
                        ? product.product.productsImg[0].imagePath
                        : "https://st4.depositphotos.com/14953852/24787/v/450/depositphotos_247872612-stock-illustration-no-image-available-icon-vector.jpg"}>  
                        </Card.Img>
                    <Card.Title className="mt-2">{ product.product.name}</Card.Title>

                    <ProductPrice price={`Total ${product.totalSum}`}></ProductPrice>

                </Card>
            ))}
            </Row>
        </Container>
    );
}

export default UserPurchasedProducts;