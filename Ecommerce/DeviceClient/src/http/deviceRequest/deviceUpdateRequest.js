export const categoryUpdate = async (category) => {
    var token = localStorage.getItem("authToken")

    try {
      const response = await fetch(`https://localhost:7284/api/v1/Category/Update`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer  ${token}` 
        },
        body: JSON.stringify(category),
      });

      if(!response.ok){
        return await response.json();
      }

      return await response.json();
    } catch (error) {
      throw error; 
    }
  };

  export const subCategoryUpdate = async (subCategory) => {
    var token = localStorage.getItem("authToken")

    try {
      const response = await fetch(`https://localhost:7284/api/v1/SubCategory/Update`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer  ${token}` 
        },
        body: JSON.stringify(subCategory),
      });

      if(!response.ok){
        return await response.json();
      }

      return await response.json();
    } catch (error) {
      throw error; 
    }
  };
  export const brandUpdate = async (brand) => {
    var token = localStorage.getItem("authToken")

    try {
      const response = await fetch(`https://localhost:7284/api/v1/Brand/Update`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer  ${token}` 
        },
        body: JSON.stringify(brand),
      });

      if(!response.ok){
        return await response;
      }

      return await response.json();
    } catch (error) {
      throw error; 
    }
  };
