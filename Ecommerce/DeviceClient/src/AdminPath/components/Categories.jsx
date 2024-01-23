import "../css/categories.css"
import { useContext, useEffect, useState } from "react";
import { Context } from "../..";
import { Card, Col, Container, Row } from "react-bootstrap";
import { deleteCategory } from "../../http/deviceRequest/deviceDeleteRequest";
import { getAllCategory,getСategoryById } from "../../http/deviceRequest/deviceGetRequest";
import { observer } from "mobx-react-lite";
import  RedactItem  from "./modals/RedactNameAndIcon";
import { categoryUpdate } from "../../http/deviceRequest/deviceUpdateRequest";
import { MAIN_ADMIN_PAGE } from "../utilits/constPath";
import { NotificationContainer, NotificationManager } from 'react-notifications';

const Categories = observer(({adminOption})=>{

    const { adminProduct,breadPath } = useContext(Context);
    const [edit,setEdit]  = useState(null);

    useEffect(() => {
        try {
            getAllCategory().then(data => adminProduct.setCategory(data))
            breadPath.setPaths([
                { name: 'AdminPanel', url: MAIN_ADMIN_PAGE },
                { name: "Categories", url: MAIN_ADMIN_PAGE + '/category' },
              ]);
        } catch (error) { }
      }, []);

    function selectCardHandler(category){
        adminProduct.setSelectedCategory(category);
        adminOption("subcategories")
    }
    async function selectEditCategoryHandler(id){
        
        await getСategoryById(id)
        .then(data=>{
            breadPath.pushToPaths({"name": data.name, "url": MAIN_ADMIN_PAGE + '/category/'+ data.name});
            setEdit(data);
        })
    }

    async function submitUpdateHandler() {
        try {
            await categoryUpdate(edit, edit.id);
            NotificationManager.success('Success', 'Success');
        } catch (error) {
          NotificationManager.error('Error', 'Error');
        }
    }
            
    return(
            <Container >
                {
                    edit!==null ? 
                    <div className="redact-div ">
                        <RedactItem setItem = {setEdit} item = {edit}/>
                        <button onClick={submitUpdateHandler} className="submit-btn">Submit</button>
                     </div>
                     
                    : <Row className="items-list-div mt-5">{
                        adminProduct.category && 
                        adminProduct.category.map(category=>(
                            <Card >
                                <Card.Img className="mt-2"
                                variant="top" src={category.icon}  
                                onClick={()=>selectCardHandler(category)}/>
    
                                  <Card.Title>{category.name}</Card.Title>
                                  <Col className="mt-4">

                                    <button style={{color:"blueviolet"}} 
                                    onClick={()=>selectCardHandler(category)} 
                                    className="edit-btn">Details</button>

                                    <button onClick={()=>(deleteCategory(category.id).then(data=>console.log(data)))} className="remove-btn">Remove</button>
                                    <button onClick={()=>selectEditCategoryHandler(category.id)}  className="edit-btn">Edit</button>
                                  </Col>
                            </Card>
                        ))
                    }
                    </Row>
                }
            <NotificationContainer></NotificationContainer>

            </Container>
    )
})

export default Categories;