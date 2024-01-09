import React, { useContext, useEffect, useState } from "react";
import { Container, Pagination, Row } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import { Context } from "../..";
import ProductList from "../components/ProductList";
import "react-image-gallery/styles/css/image-gallery.css";

const Shop = observer(() => {
   
    const itemTakeCount =  6;
    const {userProduct} = useContext(Context);

    useEffect(() => {}, [userProduct.currentPage]);

    const handlePageChange = async(pageNumber) => {
      userProduct.setCurrentPage(pageNumber)
      };
   
      const renderPaginationItems = () => {
        const paginationItems = [];
        for (let page = 1; page <= userProduct.totalPages; page++) {
          paginationItems.push(
            <Pagination.Item key={page} active={page === userProduct.currentPage} 
             onClick={() => handlePageChange(page)}>
              {page}
            </Pagination.Item>
          );
        }
        return paginationItems;
      };


    return (
        <Container>
          <h2 className="text-center mt-2">{userProduct.selectedCategory}</h2> 

          <ProductList products={userProduct.product} categoryName = {userProduct.selectedCategory}></ProductList>
            <Row className="mt-5" style={{justifyContent:"center"}}>
                <Pagination activePage={userProduct.currentPage} 
                itemsCountPerPage={itemTakeCount} 
                totalItemsCount={userProduct.totalItems}>
                {renderPaginationItems()}
                </Pagination>
          </Row>
        </Container>
    )
});
export default Shop;