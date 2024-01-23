import { useEffect, useState } from "react";
import { Col } from "react-bootstrap";
import { getCharsValue,getCharsByPrice } from "../../http/deviceRequest/deviceGetRequest";
import { observer } from "mobx-react-lite";
import "../css/filtration.css"
import { toJS } from "mobx";

const ProductFilter = observer(({categoryCharacteristics,categoryId})=>{

    const [categoryCharsValuesAndName, setCategoryCharsValuesAndName] = useState([]);
    const [priceFilter, setPriceFilter] = useState([]);
    
    useEffect(() => {
        const fetchData = async () => {
            const updatedValues = [];

            for (const characteristics of categoryCharacteristics) {
                 await getCharsValue(characteristics.id)
                 .then(values=>updatedValues.push({
                    ...characteristics,
                    values,
                }));
                console.log(toJS(characteristics))
                
            }
            setCategoryCharsValuesAndName(updatedValues);
    
            const priceData = await getCharsByPrice(categoryId);
            setPriceFilter(priceData);
        };
    
        fetchData();
    }, [categoryId, categoryCharacteristics]);
    
    console.log(categoryCharsValuesAndName)
    return(
        <div className="main-filter-div bg-dark text-center">
                 <Col className="mt-2 mb-4">
                 <h3 className="charValuesName">Price</h3> 
                 {
                   <div className="d-block">
                    <input className="price-filter" style={{ marginRight:"10px"}} 
                                                    placeholder={priceFilter.item1}/>
                    <input className="price-filter" placeholder={priceFilter.item2}/>
                    </div>
                 }
                </Col>
            {
                categoryCharsValuesAndName.map(charsValue=>(
                    <Col className="mt-3">
                        <h3 className="charValuesName">{charsValue.name}</h3> 
                        {
                            charsValue.values.map(char=>(
                                <div className="d-block">
                                    <p>{`${char.key} `} <i>({char.value})</i></p>
                                </div>
                            ))
                        }
                    </Col>
                ))
            }
        </div>
    )
})

export default ProductFilter;