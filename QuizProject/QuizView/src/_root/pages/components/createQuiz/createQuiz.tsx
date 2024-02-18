import { observer } from "mobx-react-lite";
import {  useState } from "react";
import { FirsStep } from "./firstStep";
import "../../../../css/create.css"
import { SecondStep } from "./secondStep";
import { ThirdStep } from "./thirdStep";

export const CreateQuiz = observer(({ userId }: { userId: string | undefined }) => {
  const [stepIndex,setStepIndex] = useState(0);
  const [tags, setTags] = useState<string[]>([]);
  const [quizQuestions, setQuizQuestions] = useState<any[]>([])
  const [quizResults, setQuizResults] = useState<any[]>([])

  const [newQuiz] = useState( {
    "id": {},
    "title": "",
    "titleImg": "",
    "quizDescription": "",
    "tags": [''],
    "quizQuestions":[{
      "id": 0,
      "question": "",
      "answer": "",
      "options": [],
      "rightAnswers": [],
      "optionFormats":'OneRightAnswer'
    }],
    "quizResults": [
        {
            "resultDescription": "",
            "resultTitle": "",
            "condition": "Lower",
            "conditionValue": 0
        }
    ],
    "userId": userId,
    "type": "",
    "updateTime": "2024-02-15T19:37:42.801Z",

})

  const createQuizAction = async () => {
    newQuiz.tags = tags;
    newQuiz.quizQuestions = quizQuestions;
    newQuiz.quizResults = quizResults;
    newQuiz.userId = userId;
  }
  
    return (
        <div className="create container d-block">
                <>
                {
                  stepIndex === 0 && (<FirsStep newQuiz={newQuiz} tags={tags} setTags={setTags} />)
                }
                {
                  stepIndex === 1 && (
                    <SecondStep  
                    newQuiz={newQuiz}
                    quizQuestions={quizQuestions} 
                    setQuizQuestions={setQuizQuestions} />
                  )
                }
                {
                  stepIndex === 2 && (<ThirdStep  quizResults={quizResults} setQuizResults={setQuizResults} />)
                }

                <div className="prev-next-div d-flex mt-4 ">
                  {stepIndex > 0 && (
                    <button onClick={() => setStepIndex(stepIndex - 1)}>Prev</button>
                  )}
                  {
                    stepIndex < 2 ? 
                    <button onClick={() => setStepIndex(stepIndex + 1)}>Next</button>
                   :
                    <button onClick={createQuizAction} type="submit">Create</button>
                  }
                 
                </div>
              </>
        </div>
    )

   
});





