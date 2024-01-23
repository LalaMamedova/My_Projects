import { observer } from "mobx-react-lite";
import IconSelect from "./modals/IconChoice";
import React from "react";
import { useContext, useEffect, useState } from "react";
import { Context } from '../..';
import { categoryPost, subCategoryPost } from "../../http/deviceRequest/devicePostRequest";
import { categoryJson, subCategoryJson } from "../staticJsonFiles/deviceStaticJson";
import { MAIN_ADMIN_PAGE } from "../utilits/constPath";
import { NotificationContainer, NotificationManager } from 'react-notifications';

const AddSubCategory = observer(() => {

  const {adminProduct,breadPath } = useContext(Context);
  const [subСategoryIcon, setSubCategoryIcon] = useState();
  const [categoryIcon, setCategoryIcon] = useState();
  const [isAddedNewCategory, setIsAddedNewCategory] = useState(false);
  let characteristicsArray = [];
  let [charCount, setCharCount] = useState(1);

  useEffect( () => {
   
    breadPath.setPaths([
        { name: 'AdminPanel', url: MAIN_ADMIN_PAGE },
        { name: "Add SubCategory", url: MAIN_ADMIN_PAGE + '/add-subcategory' },
      ]);
}, []);


  function categoryHandler(categoryChoice,addcategory,newcategory ) {
    document.querySelector(".category-choice").style.display = categoryChoice;
    document.querySelector(".add-category-div").style.display = addcategory;
    setIsAddedNewCategory(newcategory)
  }

  function removeCharHandler(index) {
    let parent = document.querySelector(`.chars-div`)
    let child = document.getElementsByClassName(`char-${index}`);
    characteristicsArray = [];
    if (parent.childElementCount > 3) {
      parent.removeChild(child[0])
    }
  }

  const addChars = async () => {
  
    const charsDescriotion = document.querySelectorAll("#char-description");
    const charsName = document.querySelectorAll("#char-name");
  
    for (let i = 0; i < charsName.length; i++) {
      const name = charsName[i].value;
      const description = charsDescriotion[i].value;
      
      if(name!==''){

        const characteristic = {
          id:0,
          name: name,
          description: description,
          subCategoryId:0,
        };
        
        characteristicsArray.push(characteristic);
      }else{
        NotificationManager.error("Please write name to all characteristics",'error');
      }

    }
  
  };
  
 async function addCategoryHandler(){
      await addChars();

      if(isAddedNewCategory){

        const categoryPostJson = categoryJson(0,
          adminProduct.categoryName,categoryIcon,
          adminProduct.subCategoryName,
          subСategoryIcon,0,characteristicsArray);
          
        await categoryPost(categoryPostJson).then(()=>
        NotificationManager.success(`${adminProduct.categoryName} was successfully added`, 'Success'));

      }else{
        const subCategoryJsonFile = subCategoryJson
          (0,adminProduct.subCategoryName,
          adminProduct.selectedCategory,
          subСategoryIcon ,characteristicsArray)
       
          console.log(subCategoryJsonFile)
          await subCategoryPost(subCategoryJsonFile)
          .then(()=>
          NotificationManager.success(`${adminProduct.subCategoryName} was successfully added`, 'Success'));
      }
    } 


  return (
    <div className="center-side-container mt-5">
      <fieldset className="input-in-center  mt-5">
        <input onChange={(e)=>{adminProduct.setSubCategoryName(e.target.value)}} placeholder="Sub Category Name"></input>
      </fieldset>

      <fieldset className="input-in-center">
        <legend>Category</legend>
        <div className="category-choice">
          <select onChange={(e)=>{adminProduct.setSelectedCategory(e.target.value)}}>
            {
              adminProduct.category && adminProduct.category.map(category => (
                <option value={category.id}>{category.name}</option>
              ))
            }
          </select>
          <button className="add-btn" onClick={() => 
            { 
              categoryHandler("none","flex",true) 
            }}>Add new</button>
        </div>

        <div className="add-category-div">
          <div>
            <input onChange={(e)=>{adminProduct.setCategoryName(e.target.value)}} placeholder="Category Name"></input>
            <button className="remove-btn" onClick={() => { categoryHandler("flex","none" ,false)}}>Remove</button>
          </div>
          
          <IconSelect setIcon={setCategoryIcon} Icon={categoryIcon} 
          className={"d-flex"}  inputId="сategoryIconInput"/>
          
        </div>
      </fieldset>

      <fieldset className="input-in-center mt-5">
        <div className="chars-div">
          <legend>Characters</legend>
          {
            Array.from({ length: charCount }, (_, index) => (
              <div className={`char-${index}`} >
                <input type="text" id="char-name"
                placeholder={`Characteristic`} />

                <input className="mt-2" type="text"  id="char-description"
                placeholder={`Description` } />

                <button className="remove-btn" onClick={() => removeCharHandler(index)}>Remove</button>
                <div className="hr" />
              </div>
            ))
          }
          <button className="add-btn" onClick={() => setCharCount(charCount += 1)}>Add</button>
        </div>
      </fieldset>

      <IconSelect setIcon={setSubCategoryIcon} Icon={subСategoryIcon}
       className={"input-in-center d-flex mt-5 ml-5"} inputId="subCategoryIconInput"></IconSelect>

     <div className="mt-1">
       <button className="submit-post-btn" onClick={addCategoryHandler}>Submit</button>
     </div>
     <NotificationContainer></NotificationContainer>
    </div>
  );
})

export default AddSubCategory;
