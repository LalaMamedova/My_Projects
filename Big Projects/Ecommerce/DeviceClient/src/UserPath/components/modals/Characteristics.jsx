import { useState } from "react";
import { Col, Row } from "react-bootstrap";

const Characteristics = (props)=>{

    const [showChars, setShowChars] = useState(2);
    const [allCharsIsOpen,setAllCharsIsOpen] = useState(false);
   

    function showAllCharsHandler(){
        if(allCharsIsOpen==false){
            setShowChars(props.characteristic.length)
            setAllCharsIsOpen(true)
        }else{
            setShowChars(2)
            setAllCharsIsOpen(false);
        }
   
    }
    return(
        <Col className="characteristics-div">
        <Row  >
            <Col >
            <div style={{ borderBottom: "1px solid white" }} className="d-flex">
            <h4  key={ props.brand.id}> Brand</h4>
            </div>
            {
                props.characteristic
                .slice(0, showChars)
                .map(char => (
                    <div style={{ borderBottom: "1px solid white" }} className="d-flex">

                        <h4  key={char.id}>
                            {char.characteristic.name}
                        </h4>
                        {
                            char.characteristic.description!=='' &&
                            <span onClick={()=>props.setShowCharDescription(char.characteristic.description)}>
                                {"  ?"}
                            </span>
                        }
         
                    </div>
                    ))
                }
            </Col>
            <Col className="mt-3" >
                <h4 style={{cursor:"pointer",color:"#DF81FF"}}> { props.brand.name}</h4>
                {
                    props.characteristic
                    .slice(0, showChars)
                    .map(char=>(
                        <h4>{char.value}</h4>
                    ))
                 }
            </Col>
        </Row>


            <span className="show-char" onClick={showAllCharsHandler}>
                {
                    allCharsIsOpen ===  true ? 'Hide ': 'Show all '
                }
                 <i className={`fa ${allCharsIsOpen ? 'fa-chevron-up' : 'fa-chevron-down'}`} aria-hidden="true"></i>
            </span>

        </Col>
    )
}

export default Characteristics; 