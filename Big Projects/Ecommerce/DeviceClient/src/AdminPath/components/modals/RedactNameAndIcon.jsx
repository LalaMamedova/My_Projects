import {useState } from "react"
import { observer } from "mobx-react-lite";
import IconSelect from "./IconChoice";
import "../../css/categories.css"

const RedactItem = observer(({setItem,item})=>{
    const [newImg,setNewImg] = useState(null);

     function changeItemName(newName){
        if(newName!=''){
            setItem(prevItem => ({
                ...prevItem, 
                "name": newName,
            }));
        }
    }

    function changeIcon(){
        if(newImg!==null){
            setItem(prevItem => ({
                ...prevItem, 
                "icon": newImg,
            }));
        }
    }changeIcon();
  


    return(
        <div className="redact-modal">
        <span onClick={()=>setItem(null)}>X</span>
            <input onChange={(e)=>changeItemName(e.target.value)} 
             placeholder="Name" value={item.name}></input>
            <IconSelect setIcon = {setNewImg} Icon={newImg} inputId={"updateIcon"} 
                                  className ={"d-flex mt-2"} />
            
        </div>
    )
})
export default RedactItem;