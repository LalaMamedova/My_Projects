import { useContext, useEffect, useState } from "react";
import { Card, Col, Container, Pagination, Row } from "react-bootstrap";
import { Context } from "../..";
import { getProductsByPage } from "../../http/deviceRequest/deviceGetRequest";
import { observer } from "mobx-react-lite";
import { deleteProduct } from "../../http/deviceRequest/deviceDeleteRequest";
import { MAIN_ADMIN_PAGE } from "../utilits/constPath";
import { NotificationContainer, NotificationManager } from 'react-notifications';
import "../css/categories.css"
import "../css/products.css"

const Products = observer(()=>{

    let [currentPage,setCurrentPage] = useState(1);
    const[totalPage, setTotalPage] = useState();
    const [totalItems,setTotalItems] = useState();
    const {breadPath } = useContext(Context);
    const{adminProduct} = useContext(Context)
    const itemTakeCount =  5;


    async function productsRequest(){
      await getProductsByPage(currentPage,itemTakeCount).then(data =>{ 
        {adminProduct.setDevice(data.items);
          setTotalPage(data.totalPages);
          setTotalItems(data.totalItemCount);
        }})
        
      }
      useEffect(() => {
        const fetchData = async () => {

          await productsRequest()
          breadPath.setPaths([
            { name: 'AdminPanel', url: MAIN_ADMIN_PAGE },
            { name: "Products", url: MAIN_ADMIN_PAGE + '/products/' },
          ]);

        };
        fetchData();
      }, [currentPage]);
      
   
      const handlePageChange = async(pageNumber) => {
        setCurrentPage(pageNumber);
        await productsRequest();
      };

      const handleDeleteProduct = async(id)=>{
        try {
          await deleteProduct(id)
          .then(()=>NotificationManager.success('Deleted', 'Success'));
        } catch (error) {
          NotificationManager.error('Error', 'Error');
        }
        await productsRequest();
      }


      const renderPaginationItems = () => {
        const paginationItems = [];
        for (let page = 1; page <= totalPage; page++) {
          paginationItems.push(
            <Pagination.Item key={page} active={page === currentPage}  onClick={() => handlePageChange(page)}>
              {page}
            </Pagination.Item>
          );
        }
        return paginationItems;
      };

      
    return(
      <Container >
        <Row className="items-list-div mt-5">
          {
            adminProduct.device && adminProduct.device.map(device => (
                <Card style={{height:"250px"}}>
                 <Card.Img
                variant="top"
                style={{ width: "100%", height: "120px" }}
                src={
                    device.productsImg && device.productsImg.length > 0
                        ? device.productsImg[0].imagePath
                        : "https://st4.depositphotos.com/14953852/24787/v/450/depositphotos_247872612-stock-illustration-no-image-available-icon-vector.jpg"
                }
                />

                  <Card.Title>{device.name}</Card.Title>

                    <Col>
                    <button onClick={()=>handleDeleteProduct(device.id)} className="remove-btn">Remove</button>
                    <button className="edit-btn">Edit</button>
                    </Col>
                </Card>
            ))
          }
          </Row>

          <Row className="mt-5" style={{justifyContent:"center"}}>
            <Pagination activePage={currentPage} itemsCountPerPage={itemTakeCount} totalItemsCount={totalItems}>
              {renderPaginationItems()}
            </Pagination>
          </Row>
          <NotificationContainer></NotificationContainer>
        </Container>

       )
})
export default Products;