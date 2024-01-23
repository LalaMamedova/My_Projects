import { useContext, useEffect, useState } from "react";
import { Container, Col,  Row } from "react-bootstrap";
import { Context } from "../..";
import { getAllBrands } from "../../http/deviceRequest/deviceGetRequest";
import { deleteBrand } from "../../http/deviceRequest/deviceDeleteRequest";
import { observer } from "mobx-react-lite";
import { MAIN_ADMIN_PAGE } from "../utilits/constPath";
import { NotificationContainer, NotificationManager } from 'react-notifications';
import "../css/brands.css"
import { brandUpdate } from "../../http/deviceRequest/deviceUpdateRequest";
import { brandPost } from "../../http/deviceRequest/devicePostRequest";

const Brands = observer(()=>{

    const {breadPath } = useContext(Context);
    const {adminProduct} = useContext(Context)
    const [editItem,setEditItem]  = useState(null);
    const[newBrand, setnewBrand] = useState(null);
    
      useEffect(() => {
        const fetchData = async () => {

            await getAllBrands().then(data=>adminProduct.setBrand(data));
            breadPath.setPaths([
            { name: 'AdminPanel', url: MAIN_ADMIN_PAGE },
            { name: "Brands", url: MAIN_ADMIN_PAGE + '/brands/' },
          ]);

        };
        fetchData();
      }, []);

    async function removeBrandHandler(id){
        await deleteBrand(id)
        .then(()=>NotificationManager.success("Deleted", "Success"));
        await getAllBrands().then(data=>adminProduct.setBrand(data));
    }

    async function updateBrandHandler(brandId){
        const updatedBrand  = {id:brandId, name : editItem.name};
        console.log(updatedBrand);
        await brandUpdate(updatedBrand)
        .then(()=>NotificationManager.success("Updated", "Success"))
        .then(()=>getAllBrands().then(data=>adminProduct.setBrand(data)));
      }
      
    return (
        <Container className="chars-list">
          {newBrand === null && adminProduct.brand  ? (
            adminProduct.brand.map((brand) => (
              <Row
              key={brand.id} 
              className="mt-4">
                <Col>
                    {editItem !== brand ? (
                    <h4 className="mt-2">{brand.name}</h4>
                    ) 
                    : 
                    (
                    <>
                    <input
                        type="text"
                        className="text-center"
                        value={editItem.name}
                        onChange={(e)=>(editItem.name = e.target.value)}/>
                    </>
                    )}
                </Col>
                <Col className="mt-2">
                { editItem!==brand ?  
                    <button onClick={() => removeBrandHandler(brand.id)} className="remove-btn">
                    Remove 
                  </button>

                  :<button className="add-btn" onClick={()=>updateBrandHandler(brand.id)}>
                    Submit
                  </button>
                }
                  <button onClick={()=>setEditItem(editItem === null || editItem!==brand ? brand:null)}
                   className="edit-btn">
                    Edit
                  </button>
                </Col>
              </Row>
            ))
          ) : (
            <Col className="text-center mt-4">
              <input 
                placeholder="Brand name"
                value={newBrand ? newBrand.name : ""}
                onChange={(e) => setnewBrand({ ...newBrand, name: e.target.value })}
              />
          
              <button className="add-btn" 
              onClick={async()=>{await brandPost(newBrand) ;await getAllBrands().then(data=>adminProduct.setBrand(data));} }>Add</button>
            </Col>
          )}
          <Col>
            <button
              className="add-btn mt-2" 
              onClick={() => setnewBrand(newBrand === null ? { name: '', id: 0 } : null)}
            >
              {newBrand === null ? "Add Brand" : "Cancel"}
            </button>
          </Col>
          

        <NotificationContainer></NotificationContainer>
        </Container>
      );
    
  
})
export default Brands;