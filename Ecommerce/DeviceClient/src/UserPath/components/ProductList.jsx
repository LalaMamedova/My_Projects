
import { Container, Row } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import ProductCard from "./modals/ProductCard";
import "../css/shop.css"

const ProductList = observer(({products})=>{
    
    return (
        <Container className="device-list">
            <Row className="d-flex">
            {products && products.length > 0 ? (
                products.map((device) => (
                    <ProductCard key={device.id}  device={device} ></ProductCard>
                    ))) 
                    :(
                        <div className="text-center">No result</div>
                    )}
            </Row>
        </Container>
    );
    
})
export default ProductList;