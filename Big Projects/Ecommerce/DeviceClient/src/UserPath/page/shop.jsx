import React, { useContext, useEffect, useState } from "react";
import { Col, Pagination, Row } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import { Context } from "../..";
import ProductList from "../components/ProductList";
import ProductFilter from "../components/ProductFilter";
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
        <div>
          <h2 className="text-center mt-2">
            {userProduct.selectedCategory !==null 
            ? userProduct.selectedCategory.name
            :userProduct.selectedSubCategory.name}
          </h2> 
          
          <Row>
          {
            <Col>
              {
                userProduct.selectedCategory !==null ?
                userProduct.selectedCategory.subCategories.map(subcategory=>(

                  <ProductFilter categoryCharacteristics={subcategory.characteristics} 
                  categoryId = {userProduct.selectedCategory.id}/>))
                  :
                  <ProductFilter categoryCharacteristics={ userProduct.selectedSubCategory.characteristics}
                  categoryId = { userProduct.selectedSubCategory.categoryId}/>
              }
            </Col>
          }
          <ProductList products={userProduct.product} categoryName = {userProduct.selectedCategory !==null 
            ? userProduct.selectedCategory.name
            :userProduct.selectedSubCategory.name}
          />

          </Row>

            <div className="mt-5 d-flex" style={{justifyContent:"center"}}>
                <Pagination activePage={userProduct.currentPage} 
                itemsCountPerPage={itemTakeCount} 
                totalItemsCount={userProduct.totalItems}>
                {renderPaginationItems()}
                </Pagination>
          </div>
        </div>
    )
});
export default Shop;