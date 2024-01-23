export const deleteCategory = async (id) => {
  var token = localStorage.getItem("authToken")

    try {
      const response = await fetch(`https://localhost:7284/api/v1/Category/Delete/${id}`, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}` ,
        },
      });

      if(!response.ok){
        throw JSON.stringify(await response.json());
      }

      return await response.json();
    } catch (error) {
      throw error; 
    }
  };

  export const deleteSubCategory = async (id) => {
    var token = localStorage.getItem("authToken")
    try {
      const response = await fetch(`https://localhost:7284/api/v1/SubCategory/Delete/${id}`, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}` ,
        },
      });

      if(!response.ok){
        throw JSON.stringify(await response.json());
      }

      return await response.json();
    } catch (error) {
      throw error; 
    }
  };
  
  export const deleteProduct = async (id) => {
    var token = localStorage.getItem("authToken")

    try {
      const response = await fetch(`https://localhost:7284/api/v1/Product/Delete/${id}`, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}` ,
        },
      });

      if(!response.ok){
        throw JSON.stringify(await response.json());
      }

      return await response.json();
    } catch (error) {
      throw error; 
    }
  };

  export const deleteBrand = async (id) => {

    var token = localStorage.getItem("authToken")

    try {
      const response = await fetch(`https://localhost:7284/api/v1/Brand/Delete/${id}`, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}` ,
        }
      });

      if(!response.ok){
        throw JSON.stringify(await response.json());
      }

      return await response;
    } catch (error) {
      throw error; 
    }
  };
  
  export const deleteLikedProduct = async (id,userId) => {
    var token = localStorage.getItem("authToken")

    try {
      const response = await fetch(`https://localhost:7284/api/v1/Product/DeleteFromLikedProduct/${id}/${userId}`, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}` ,
        },
       
      });

      if(!response.ok){
        throw JSON.stringify(await response.json());
      }

      return await response.json();
    } catch (error) {
      throw error; 
    }
  };
  
  


  
