import { useNavigate } from "react-router-dom";
import Resizer from 'react-image-file-resizer';

const useProductsFunctions = (setProductImgs) => {
  
   
    function resizeAndAddToProductImgs(file,arrayOrNot = false) {
      Resizer.imageFileResizer(
        file,
        180, 
        180, 
        'JPEG', 
        100, 
        0, 
        uri => {
          arrayOrNot 
          ?setProductImgs(oldArray => [...oldArray, uri])
          :setProductImgs(uri)
        },
        'base64' 
      );
    }
    return { resizeAndAddToProductImgs };
  };
  
  export default useProductsFunctions;