export const productPost = async (product) => {
    var token = localStorage.getItem("authToken")

    try {
      const response = await fetch('https://localhost:7284/api/v1/Product', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}` ,
        },
        body: JSON.stringify(product),
      });

      if(!response.ok){
        throw JSON.stringify(await response.json());
      }

      return await response.json();
    } catch (error) {
      throw error; 
    }
  };

  export const subCategoryPost = async (category) => {
    var token = localStorage.getItem("authToken")

    try {
      const response = await fetch('https://localhost:7284/api/v1/SubCategory', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}` ,

        },
        body: JSON.stringify(category),
      });

      if(!response.ok){
        throw JSON.stringify(await response.json());
      }

      return await response.json();
    } catch (error) {
      throw error; 
    }
  };

export const categoryPost = async (category) => {
  var token = localStorage.getItem("authToken")

    try {
      const response = await fetch('https://localhost:7284/api/v1/Category', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}` ,

        },
        body: JSON.stringify(category),
      });

      if(!response.ok){
        throw JSON.stringify(await response.json());
      }

      return await response.json();
      
    } catch (error) {
      throw error; 
    }
  };

  export const reviewPost = async (review) => {
    var token = localStorage.getItem("authToken")
    try {
      const response = await fetch('https://localhost:7284/api/v1/Product/AddReview', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}` ,
        },
        body: JSON.stringify(review),
      });

      if(!response.ok){
        throw JSON.stringify(await response.json());
      }

      return await response;

    } catch (error) {
      throw error; 
    }
  };

  export const likedProductPost = async (product) => {
    var token = localStorage.getItem("authToken")

    try {
      const response = await fetch('https://localhost:7284/api/v1/Product/AddLikedProduct', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}` ,

        },
        body: JSON.stringify(product),
      });

      if(!response.ok){
        throw JSON.stringify(await response.json());
      }

      return await response.json();
    } catch (error) {
      throw error; 
    }
  };

  export const purchaseProductsPost = async (purchasedProducts,userId) => {
    var token = localStorage.getItem("authToken")

    try {
      const response = await fetch(`https://localhost:7284/api/v1/PurchaseProduct/${userId}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}` ,
        },
        body: JSON.stringify(purchasedProducts),
      });

      if(!response.ok){
        throw JSON.stringify(await response.json());
      }

      return await response.json();
    } catch (error) {
      throw error; 
    }
  };

  export const brandPost = async (brand) => {
    var token = localStorage.getItem("authToken")

    try {
      const response = await fetch(`https://localhost:7284/api/v1/Brand`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}` ,
        },
        body: JSON.stringify(brand),
      });

      if(!response.ok){
        throw JSON.stringify(await response.json());
      }

      return await response.json();
    } catch (error) {
      throw error; 
    }
  };