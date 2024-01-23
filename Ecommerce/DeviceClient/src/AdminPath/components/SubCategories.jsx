import { useContext, useEffect, useState } from "react";
import { Context } from "../..";
import { Card, Col, Container, Row } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import RedactItem from "./modals/RedactNameAndIcon";
import { getSubСategoryById,getСategoryById } from "../../http/deviceRequest/deviceGetRequest";
import { deleteSubCategory } from "../../http/deviceRequest/deviceDeleteRequest";
import { subCategoryUpdate } from "../../http/deviceRequest/deviceUpdateRequest";
import RedactCharacteristics from "./modals/RedactCharacteristics";
import { NotificationContainer, NotificationManager } from 'react-notifications';
import { subCategoryJson } from "../staticJsonFiles/deviceStaticJson";
import { MAIN_ADMIN_PAGE } from "../utilits/constPath";
import "../css/categories.css"
import Selects from "./modals/Selects";

const SubCategories = observer(()=>{

    const { adminProduct,breadPath } = useContext(Context);
    const [editItem,setEditItem]  = useState(null);
    const [updateCharacteristic, setUpdateCharacteristic] = useState([])

        
    useEffect( () => {
        breadPath.setPaths([
            { name: 'AdminPanel', url: MAIN_ADMIN_PAGE },
            { name: "SubCategories", url: MAIN_ADMIN_PAGE + '/subcategories' },
          ]);
    }, []);


    async function selecteditItemSubCategoryHandler(id){
        await getSubСategoryById(id)
        .then(data=>
        {
          setEditItem(data);
          setUpdateCharacteristic(data.characteristics);
            
      });
    }
   

    async function removeSubCategory(id,categoryId){
        try {
            await deleteSubCategory(id);
            NotificationManager.success(`${categoryId} was successfully removes`, 'Success');
            await getСategoryById(categoryId)
            .then(data => adminProduct.setSelectedCategory(data))

        } catch (error) {
            NotificationManager.Error('Error', 'Error');
        }
    }

    async function submitUpdateHandler() {
      const subcategory = subCategoryJson(editItem.id,editItem.name, editItem.categoryId, 
                                          editItem.icon, updateCharacteristic);

                                          console.log(subcategory)
        try {
          await subCategoryUpdate(subcategory)
          .then(data=> {
            setEditItem(data);
            setUpdateCharacteristic(data.characteristics);
            NotificationManager.success(`${subcategory.name} was successfully updated`, 'Success'); 
        }); 
        } 
      catch (error) {
        NotificationManager.error(error, 'Error');

      }
    }


    return(
        <div>
            <Container >
                {
                    editItem!==null ? 
                    <div className="redact-div">
                     <RedactItem setItem = {setEditItem} item = {editItem}/>
                     <Selects setItem = {setEditItem} item = {editItem} 
                                    list = {adminProduct.category} fildName = "categoryId"/>

                     <RedactCharacteristics editItem = {editItem} updateCharacteristic = {updateCharacteristic}
                                            setUpdateCharacteristic ={setUpdateCharacteristic}  />

                        <button onClick={submitUpdateHandler} className="submit-btn">Submit</button>
                     </div>:


                    <Row className="items-list-div">
                        {
                            adminProduct.selectedCategory 
                            && adminProduct.selectedCategory.subCategories.map(subcategory=>(
                                <Card>
                                <Card.Img className="mt-2"
                                variant="top" src={subcategory.icon} />

                                <Card.Title>{subcategory.name}</Card.Title>
                                <Col className="mt-4">

                                    <button  onClick={()=>removeSubCategory(subcategory.id,subcategory.categoryId)
                                    .then(data=>console.log(data))} className="remove-btn">Remove</button>

                                    <button onClick={()=>selecteditItemSubCategoryHandler(subcategory.id)}
                                    className="edit-btn">Edit</button>
                                </Col>
                            </Card>
                            ))
                        }
                    </Row>
            }
            <NotificationContainer></NotificationContainer>
            </Container>
        </div>
    )
})

export default SubCategories;