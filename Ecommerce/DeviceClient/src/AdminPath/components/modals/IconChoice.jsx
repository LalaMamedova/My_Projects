import { observer } from "mobx-react-lite";
import useProductsFunctions from "../../functions/productsFunctions";

const IconSelect = observer(({setIcon,Icon,inputId,className}) => {

  const {resizeAndAddToProductImgs} = useProductsFunctions(setIcon);

  function choiceIconHandler(e) {
      const fileInput = e.target.files[0];
        
      if (fileInput) {
          resizeAndAddToProductImgs(fileInput);
      }
    }
  
    return (
      <div className={className}>
        {
            Icon &&
            <div className="choice-img-div ">
            <img src={Icon} />
            <button onClick={()=>setIcon(null)}>x</button>
            </div>
         }
        <label htmlFor={inputId} className="upload-img">Upload Icon </label>
        <input 
          id={inputId}
          type="file"
          onChange={(event) => choiceIconHandler(event)}
          style={{ display: "none" }}
          accept="image/png, image/gif, image/jpeg, image/svg" />
         
      </div>
      
    )
  });
export default IconSelect;
  