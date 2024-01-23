export const getAllCategory = async () => {
    try {
        var response = await fetch(`https://localhost:7284/api/v1/Category`);
        let data = await response.json();
        return data;
    } catch (error) {
        return response;
    }
};
export const getCategoryByName = async (name) => {
    try {
        var response = await fetch(`https://localhost:7284/api/v1/Category/Get/${name}`);
        let data = await response.json();
        return data;
    } catch (error) {
        return response;
    }
};

export const getProductsByCategory = async (categoryId,page) => {
    try {
        var response = await fetch(`https://localhost:7284/api/v1/Product/GetCategory/${categoryId}/${page}`);
        let data = await response.json();
        return data;
    } catch (error) {
        return response;
    }
};

export const getProductsBySubCategory = async (subcategoryId,page) => {
    try {
        var response = await fetch(`https://localhost:7284/api/v1/Product/GetSubCategory/${subcategoryId}/${page}`);
        let data = await response.json();
        return data;
    } catch (error) {
        return response;

    }
};


export const getProductsByPage = async (page,itemCount) => {
    try {
        var response = await fetch(`https://localhost:7284/api/v1/Product/GetByPage/${page}/${itemCount}`);
        let data = await response.json();
        return data;
    } catch (error) {
        return response;

    }
};

export const getProductRecomendation= async (categoryId,productId,viewedProducts) => {
    try {
        var response = await fetch(`https://localhost:7284/api/v1/Product/GetRecomendation/${categoryId}/${productId}`,{
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(viewedProducts),
        });
        let data = await response.json();
        return data;
        
    } catch (error) {
        return response;
    }
};

export const getAllBrands = async () => {
  try {
      var response = await fetch(`https://localhost:7284/api/v1/Brand`);
      let data = await response.json();
      return data;
  } catch (error) {
    return response;

  }
};

export const getProductById = async (id) => {
    try {
        var response = await fetch(`https://localhost:7284/api/v1/Product/GetProductById/${id}`);
        let data = await response.json();
        return data;
    } catch (error) {
        return response;

    }
};
export const getProductsById = async (id) => {
    try {
        var response = await fetch(`https://localhost:7284/api/v1/Product/GetProductsById/${id}`);
        let data = await response.json();
        return data;
    } catch (error) {
        return response;

    }
};

export const getCharsValue = async (id) => {
    try {
        var response = await fetch(`https://localhost:7284/api/v1/ProductСharacteristic/Get/${id}`);
        let data = await response.json();
        return data;
    } catch (error) {
        return response;

    }
};
export const getCharsByPrice = async (categoryId) => {
    try {
        var response = await fetch(`https://localhost:7284/api/v1/SubCategory/GetByPrice/${categoryId}`);
        let data = await response.json();
        return data;
    } catch (error) {
        return response;

    }
};

export const getСategoryById = async (id) => {

  try {
      var response = await fetch(`https://localhost:7284/api/v1/Category/Get/${id}/`);
      let data = await response.json();
      return data;
  } catch (error) {
    return response;
  }
};


export const getSubСategoryById = async (id) => {
  try {
      var response = await fetch(`https://localhost:7284/api/v1/SubCategory/Get/${id}`);
      let data = await response.json();
      return data;
  } catch (error) {
    return response;
  }
};

export const getNewestProducts = async () => {
    try {
        var response = await fetch(`https://localhost:7284/api/v1/Product/GetNewestProducts`);
        let data = await response.json();
        return data;
    } catch (error) {
      return response;
    }
  };
  
  export const getUserProducts = async (userId) => {
    try {
        var response = await fetch(`https://localhost:7284/api/v1/Product/GetAllLikedProducts/${userId}`);
        let data = await response.json();
        return data;
    } catch (error) {
      return response;
    }
  };
  
  
  
  
  

