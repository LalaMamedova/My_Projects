const CharDescription=(prop)=>{
    return(
        <div className="char-description-div">
            <h4>{prop.description}</h4>
            <span  onClick={()=>prop.close(null)}>&times;</span>
        </div>
    )
}

export default CharDescription