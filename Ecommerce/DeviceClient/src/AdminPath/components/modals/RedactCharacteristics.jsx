import { useState } from "react";
import { Col, Row } from "react-bootstrap";
import { subCategoryCharacteristicsJson } from "../../staticJsonFiles/deviceStaticJson";
import { observer } from "mobx-react-lite";

const RedactCharacteristics = observer(({editItem,updateCharacteristic,
                               setUpdateCharacteristic})=>{
                                
    const [editChar,setEditChar]  = useState(null);

    function removeCharHandler(index){

      setUpdateCharacteristic(prevState => {
        const newState = [...prevState];
        newState.splice(index, 1);
        return newState;
      });
        

    }
      function addChars(){
        const newChar = subCategoryCharacteristicsJson(editItem.id);
        
        editItem.characteristics.length > 0
        ?setUpdateCharacteristic(updateCharacteristic => [...updateCharacteristic, newChar])
        :updateCharacteristic.push(newChar);
        
      }
      
      function reWriteChar(index, value, fieldName) {
        setUpdateCharacteristic(prevItem => {
          const updatedArray = [...prevItem];

          updatedArray[index] = {
            ...updatedArray[index],          
            [fieldName]: value,
          };
          return updatedArray;
        });
      }
      
      function editCharHandler(index) {
        setEditChar(editChar === null || editChar !== index ? index : null);
      }
      
      
    return(

        <div className="d-block mt-5" >
        <h3>Characteristics</h3>{

         Array.from({ length: updateCharacteristic.length }, (_, index) => (
          <div key={`char-${index}`} className={`char-${index}`}>
            <div className=" mt-5 d-flex">
              {editChar !== index ? (
                <Row className="d-flex">   
                   <Col >
                   <h5 style={{color: updateCharacteristic[index].name === ''? "red": "white"}}>
                        {updateCharacteristic[index].name !== '' ?
                        updateCharacteristic[index].name
                        :"Name is empty!"}
                      </h5>
                  </Col>

                  <Col>
                      <h5 style={{color: updateCharacteristic[index].description === ''? "red": "white"}}>
                        {updateCharacteristic[index].description !== '' ?
                        updateCharacteristic[index].description
                        :"Descrption is empty!"}
                      </h5>
                  </Col> 
                </Row>
              ) 
              :(
                  <Col>
                    <input
                      type="text"
                      value={updateCharacteristic[index].name}
                      onChange={(e)=> reWriteChar(index,e.target.value, "name")}/>
          
                    <input
                      type="text"
                      value={updateCharacteristic[index].description}
                      onChange={(e)=> reWriteChar(index, e.target.value, "description")}
                      />
                      <button
                      onClick={() => {
                        if (index < editItem.characteristics.length) {
                          reWriteChar(index, editItem.characteristics[index].description, "description");
                          reWriteChar(index, editItem.characteristics[index].name, "name");
                        } else {
                          reWriteChar(index, " ", "description");
                          reWriteChar(index, " ", "name");
                        }
                      
                      }}
                      className="edit-btn">
                      Rewrite</button>

                  </Col>
              )}
            <Row  className="col-2">
                <Col>
                    <button onClick={()=> 
                      editCharHandler(index)} 
                      className="edit-btn">Edit</button>
                  
                      <button onClick={()=>
                      removeCharHandler(index)}
                      className="remove-btn">Remove</button>

                  </Col>
            </Row>
            </div>
            <div style={{ width: "100%" }} className="hr" />
          </div>
        ))}
      
        <button className="add-btn" onClick={addChars}>Add characteristics </button>
      </div>
    )
})

export default RedactCharacteristics;