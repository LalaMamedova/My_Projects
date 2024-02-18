import { observer } from "mobx-react-lite";
import { useState } from "react";
import "../../../../css/create.css"

export const FirsStep = observer(({newQuiz,tags, setTags} : 
{newQuiz:any, tags:string[],  setTags:React.Dispatch<React.SetStateAction<string[]>>}) => {


  const [title, setTitle] = useState(newQuiz.title);
  const [description, setDescription] = useState(newQuiz.quizDescription);

  const handleAddTag = (e: React.KeyboardEvent<HTMLInputElement>) => {
    const target = e.target as HTMLInputElement; 
    const value = target.value;
    if (e.key === 'Enter' && value.trim() !== '') {
      setTags([...tags, value.trim()]);
      target.value = ''; 
    }
  };
  

  const removeTag = (indexToRemove:number) => {
    setTags(tags.filter((_, index) => index !== indexToRemove));
  };

  const handleTitleChange = (e: { target: { value: any; }; }) => {
    setTitle(e.target.value);
    newQuiz.title = e.target.value; 
  };
  
  const handleDescriptionChange = (e: { target: { value: any; }; }) => {
    setDescription(e.target.value); 
    newQuiz.quizDescription = e.target.value; 
  };

    return (
        <>
         <div>
    <input
      type="text"
      value={title}
      onChange={handleTitleChange}
      placeholder="Quiz title"
    />
    <input
      type="text"
      value={description}
      onChange={handleDescriptionChange}
      placeholder="Description"
    />
  </div>

        <input type="file" placeholder="titleImg" />

        <div className="d-block">
        <p className="text-center mt-4">Tags</p>
          {
          tags.map((tag, index) => (
              <div className="m-0">
              <li key={index}>
                <div className="col">
                  <h4>{tag}</h4>
                </div>
                <div className="col">
                  <span  onClick={() => removeTag(index)}>{" Remove"}</span>
                  <span  onClick={() => removeTag(index)}>{" Edit"}</span>
                </div>
              </li>
            </div>
          ))}

        <input
          type="text"
          placeholder="Enter a tag and press Enter"
          onKeyDown={handleAddTag}/>
        
        <select  defaultValue={newQuiz.type = 'Learning'} 
            onChange={(e)=>newQuiz.type = e.target.value}>
            <option value="Learning">Learning</option>
            <option value="ForFun">For fun</option>
            <option value="Psychological">Psychological</option>
        </select>
      </div>
    </>
    )

   
});





