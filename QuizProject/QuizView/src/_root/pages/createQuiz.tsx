import User from "@/class/User"
import { postQuizAsync } from "@/http/quizRequests"
import { observer } from "mobx-react-lite";
import { useState } from "react"

export const CreateQuiz = observer(({ user }: { user: User })=>{
    
    let [tagsCount,setTagsCount] = useState(1);
    let tags = [""];

    const quiz = {
      "id": {},
      "title": "",
      "titleImg": "",
      "quizDescription": "",
      "tags": [
        "string"
      ],
      "quizQuestions": [
        {
          "question": "",
          "answer": "",
          "options": [
            ""
          ],
          "rightAnswers": [
            ""
          ]
        }
      ],
      "quizResults": [
        {
          "resultDescription": "",
          "resultTitle": "",
          "condition": "Lower",
          "conditionValue": 0
        }
      ],
      "userId": user.id,
      "type": "Learning",
      "updateTime": "2024-02-15T19:37:42.801Z"
    }
    const handleTagChange = (index:number, value:string) => {
      tags[index] = value;
      console.log(tags)

     };
     const handleAddTag = () => {
      const newTags = [...tags, ""]; 
      console.log(newTags)
      tags = (newTags);
      setTagsCount(tagsCount + 1); 
    };
    
    return(
        <div className="d-block"> 
        <div>
              <input type="text" onChange={(e)=>quiz.title = e.target.value} 
              placeholder="Quiz title"/>

              <input type="text" onChange={(e)=>quiz.quizDescription = e.target.value} 
            placeholder="quizDescription"/>
        </div>
           

        <input type="file"   placeholder="titleImg"/>

          {
              Array.from({ length: tagsCount }, (_, index) => (
                <div className={`tags-${index}`} >
                  <input type="text"  placeholder={`Tag`} onChange={(e)=>handleTagChange(index,e.target.value)} />
                  <span>Remove</span>
                </div>
              ))
          }

          <button onClick={handleAddTag}>Add tag</button>
           <div>
              <input type="text" onChange={(e)=>quiz.title = e.target.value} 
              placeholder="Quiz title"/>
            </div> 
            <button type="submit" onClick={async()=>await postQuizAsync(quiz) }>Create</button>
        </div>
    )
});
