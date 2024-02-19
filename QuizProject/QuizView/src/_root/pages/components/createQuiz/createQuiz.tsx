import { observer } from "mobx-react-lite";
import { useState } from "react";
import { FirsStep } from "./firstStep";
import "../../../../css/create.css";
import { SecondStep } from "./secondStep";
import { ThirdStep } from "./thirdStep";

export const CreateQuiz = observer(
  ({ userId }: { userId: string | undefined }) => {
    const [stepIndex, setStepIndex] = useState(1);
    const [quizModelJson,setQuizModelJson] = useState({
      id: {},
      title: "",
      titleImg: "",
      quizDescription: "",
      tags: [],
      quizQuestions: [
        {
          id: 0,
          question: "Вопрос",
          answer: "",
          options: ["Нуу", "аааа", "ууууу"],
          rightAnswers: [],
          optionFormat: "OneRightAnswer",
        },
        {
          id: 1,
          question: "Вопрос2",
          answer: "",
          options: ["Нуу", "аааа", "ууууу"],
          rightAnswers: [],
          optionFormat: "OneRightAnswer",
        },
      ],
      quizResults: [
        {
          resultDescription: "",
          resultTitle: "",
          condiditonValueFrom:0,
          condiditonValueTo:0,
        },
      ],
      userId: userId,
      type: "Learning",
      updateTime: "2024-02-15T19:37:42.801Z",
    });

    const createQuizAction = async () => {
      // quizModelJson.quizQuestions = quizQuestions;
      // quizModelJson.quizResults = quizResults;
      quizModelJson.userId = userId;
    };

    return (
      <div className="create container d-block">
        <>
          {stepIndex === 0 && (
            <FirsStep
              quizModelJson={quizModelJson}
              setQuizModelJson ={setQuizModelJson}
            />
          )}
          {stepIndex === 1 && (
            <SecondStep
            quizModelJson={quizModelJson}
            setQuizModelJson={setQuizModelJson}
            />
          )}
          {stepIndex === 2 && (
            <ThirdStep  
            quizModelJson={quizModelJson}
            setQuizModelJson={setQuizModelJson}/>
          )}

          <div className="prev-next-div d-flex mt-4 ">
            {stepIndex > 0 && (
              <button onClick={() => setStepIndex(stepIndex - 1)}>Prev</button>
            )}
            {stepIndex < 2 ? (
              <button onClick={() => setStepIndex(stepIndex + 1)}>Next</button>
            ) : (
              <button onClick={createQuizAction} type="submit">
                Create
              </button>
            )}
          </div>
        </>
      </div>
    );
  }
);
