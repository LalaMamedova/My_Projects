import { Col, Row } from "react-bootstrap";

const ProductFilter = ({categoryCharacteristics})=>{

    return(
        <div style={{width:"150px"}} className="bg-dark text-center mr-2">
            {
                categoryCharacteristics.map(chars=>(
                    <Col className="mt-2">
                        <h5>{chars.name}</h5>
                    </Col>
                ))
            }
        </div>
    )
}

export default ProductFilter;