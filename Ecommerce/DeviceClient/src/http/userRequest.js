export const loginPost = async (user) => {
    try {
      const response = await fetch('https://localhost:7004/api/User/Login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(user),
      });

      if(!response.ok){
        throw JSON.stringify(await response);
      }
      return await response.json();
      
    } catch (error) {
      throw error; 
    }
  };
  
  export const registrationPost = async (newUser,role="User") => {
    
    try {
      const response = await fetch(`https://localhost:7004/api/User/Register/${role}`, {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(newUser),
      });

      if(!response.ok){
        throw JSON.stringify(await response.json());
      }
      
      return response.json();

    } catch (error) {      
      throw (error);
    }
  };
  
  export const logoutPost = async (userId) => {
    try {
      const response = await fetch(`https://localhost:7004/api/User/Logout/${userId}`, {
        method: 'POST',      
      });

      if(!response.ok){
        throw JSON.stringify(await response);
      }
      return await response.json();
      
    } catch (error) {
      throw error; 
    }
  };
  
  export const getUserAllPurchasedProducts = async (userId) => {
    var token = localStorage.getItem("authToken")

    try {
      const response = await fetch(`https://localhost:7284/api/v1/PurchaseProduct/GetAll/${userId}`, {
        method: 'GET',   
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer  ${token}` 
        },   
      });

      if(!response.ok){
        throw JSON.stringify(await response);
      }
      return await response.json();
      
    } catch (error) {
      throw error; 
    }
  };
  