const baseUrl = "https://localhost:7201/api/"


export const getAllAsync = async(controllerUrl:string)=>{
    const response = await fetch(`${baseUrl}${controllerUrl}`);
    let data = (await response).json();
    return data;
}
export const getWithToken = async(controllerUrl:string)=>{
  const token = localStorage.getItem("token");

  const response = await fetch(`${baseUrl}${controllerUrl}`,{
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` ,
    },
  });

  let data = (await response).json();
  return data;
}


export const getByParam = async (controllerUrl: string, param: string | number) => {
    const response = await fetch(`${baseUrl}${controllerUrl}${param}`);
    const data = await response.json();
    return data;
};

export const postAsync = async (controllerUrl:string, bodyObject:object) => {
    const token = localStorage.getItem("token");

    try {
        const response = await fetch(`${baseUrl}${controllerUrl}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}` ,
        },
        body: JSON.stringify(bodyObject),
      });

      if(!response.ok){
        throw await response.json();
      }

      return await response.json();
    } catch (error) {
      throw error; 
    }
  };

  export const deleteByIdAsync = async (controllerUrl:string,  id: string | number) => {

    try {
        const response = await fetch(`${baseUrl}${controllerUrl}${id}`, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json',
        },
      });

      if(!response.ok){
        throw JSON.stringify(await response.json());
      }

      return await response;
    } catch (error) {
      throw error; 
    }
  };

  export const updateAsync = async (controllerUrl:string, bodyObject:object) => {

    try {
        const response = await fetch(`${baseUrl}${controllerUrl}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(bodyObject),
      });

      if(!response.ok){
        return await response.json();
      }

      return await response.json();
    } catch (error) {
      throw error; 
    }
  };