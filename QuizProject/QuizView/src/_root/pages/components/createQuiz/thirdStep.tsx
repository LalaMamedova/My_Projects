import { observer } from "mobx-react-lite";
import "../../../../css/create.css";
import { useContext, useState } from "react";
import { QuizRightAnswers } from "./quizRightAnswers";
import { Context } from "@/main";
import { useQuizModelHooks} from "@/hooks/quizModelHooks";

export const ThirdStep = observer(
  ({
    quizModelJson,
    setQuizModelJson,
  }: {
    quizModelJson: any;
    setQuizModelJson: React.Dispatch<React.SetStateAction<any>>;
  }) => {
    const [description, setDescription] = useState("");
    const [title, setTitle] = useState("");
    const [valueFrom, setValueFrom] = useState(0);
    const [valueTo, setValueTo] = useState(1);
    const { types } = useContext(Context) as { types: string[] }; 
    const divDisplay = quizModelJson.type === "ForFun" ?"d-block" : "d-flex"
    const {updateQuizQuestions} = useQuizModelHooks();


    

    const handleAddResult = () => {
      if (title.trim().length > 0) {
        setQuizModelJson({
          ...quizModelJson,
          quizResults: [
            ...quizModelJson.quizResults,
            {
              resultDescription: description,
              resultTitle: title,
              condiditonValueFrom: valueFrom,
              condiditonValueTo: valueTo,
            },
          ],
        });
        setDescription("");
        setTitle("");
      }
    };

    const handleChangeOption = (e: { target: { value: any } },question:any) => {
      question.optionFormat = e.target.value;
      if (question.optionFormat === types[0]) {
        question.rightAnswers = [];
      }
      updateQuizQuestions({
        setQuizModelJson,
        quizModelJson,
        question,
      });
    };

    
    return (
      <div className="d-block ">
        {quizModelJson.quizResults.map(
          (
            result: { resultTitle: string; resultDescription: string },
            index: number
          ) => (
            <div key={index} className="d-flex">
              <h4>{`${result.resultTitle} ${result.resultDescription}`}</h4>
            </div>
          )
        )}

        <div className={`text-center ${divDisplay} mt-4`}>
          {quizModelJson.type !== "ForFun" && (
            <div className="d-flex">
              <fieldset>
                <legend>Right answers : From</legend>
                <input
                  type="number"
                  required
                  value={valueFrom}
                  onChange={(e) => setValueFrom(parseInt(e.target.value))}
                />
              </fieldset>

              <fieldset>
                <legend>Right answers : To</legend>
                <input
                  type="number"
                  required
                  value={valueTo}
                  onChange={(e) => setValueTo(parseInt(e.target.value))}
                />
              </fieldset>
            </div>
          )}
      
          
          {quizModelJson.type === "ForFun" && (
            <div className="d-block text-center mb-5">
              <h3>Choice checked answers and result to it</h3>
              {quizModelJson.quizQuestions.map((question: any, questionIndex: number) => (
                <div key={questionIndex}>
                  <h4>{question.question}</h4>
                  <select onChange={(e)=>handleChangeOption(e,question)} className="mr-4 ml-4">
                  <option value="OneRightAnswer">One right answer</option>
                  <option value="ManyRightAnswer">Many right answer</option>
                </select>
                  {question.options.map((option: any, index: number) => (
                    <>
                    <QuizRightAnswers
                     quizModelJson={quizModelJson}
                     setQuizModelJson={setQuizModelJson}
                     question={question}
                     option={option}
                     index={index}
                     />
                     </>
                  ))}
                </div>
              ))}
            </div>
          )}


<div className="d-flex">

          <fieldset>
            <legend>Title</legend>
            <input
              type="text"
              required
              value={title}
              placeholder="Result title"
              onChange={(e) => setTitle(e.target.value)}
              />
          </fieldset>

          <fieldset>
            <legend>Description</legend>
            <textarea
              style={{ marginLeft: "50px" }}
              required
              value={description}
              placeholder="Result description"
              onChange={(e) => setDescription(e.target.value)}
              />
          </fieldset>
        </div>
              </div>
        <div className="text-center mt-5">
          <button onClick={handleAddResult}>Add</button>
        </div>
      </div>
    );
  }
);
