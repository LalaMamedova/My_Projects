import {  useEffect, useState } from "react"
import { Container } from "react-bootstrap"
import { observer } from "mobx-react-lite";
import ProductList from "../components/ProductList";
import { useCookies } from "react-cookie";
import { getProductsById } from "../../http/deviceRequest/deviceGetRequest";

const LikedProducts = observer(()=>{

    const [userLikedProducts,setUserLikedProducts] = useState([]);
    const [cookies] = useCookies();

    useEffect(() => {
        const fetch = async () => {
            const productsFromCookies = cookies["userLikedProducts"] || [];            
            productsFromCookies.map(async product => {
                const data = await getProductsById(product.productId);
                setUserLikedProducts(userLikedProducts => [...userLikedProducts, data]);
            });
        };
    
        fetch();
    }, [setUserLikedProducts]);

    
    return (
        <Container>
            <ProductList products = {userLikedProducts}></ProductList>
        </Container>
    )
})
export default LikedProducts;