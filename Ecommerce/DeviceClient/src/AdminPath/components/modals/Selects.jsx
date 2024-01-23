import { observer } from "mobx-react-lite";
import "../../css/addproduct.css"

const Selects = observer(({list,setItem,item,fildName})=>{
    
    function changeSelectOption(value){
        setItem(prevItem => ({
            ...prevItem, 
            [fildName]: value,
        }));
    }
    return(
        <div className="select-div">
            <select value={item.categoryId} 
            onChange={(e) => changeSelectOption(e.target.value)}>
            {list && list.map(category => (
                <option key={category.id} value={category.id}>{category.name}</option>
            ))}
        </select>
      </div>
                 
    )
})
export default Selects;