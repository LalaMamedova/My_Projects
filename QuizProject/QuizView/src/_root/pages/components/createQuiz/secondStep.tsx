import "../../../../css/create.css";
import { observer } from "mobx-react-lite";
import { SecondStepItem } from "./secondStepItem";

export const SecondStep = observer(
  ({
    quizModelJson,
    setQuizModelJson,
  }: {
    quizModelJson: any;
    setQuizModelJson: React.Dispatch<React.SetStateAction<any>>;
  }) => {
    const handleAddQuestion = (e: React.KeyboardEvent<HTMLInputElement>) => {
      const target = e.target as HTMLInputElement;
      const value = target.value;

      if (e.key === "Enter" && value.trim() !== "") {
        const lastQuestion = quizModelJson.quizQuestions.length;
        setQuizModelJson(({
          ...quizModelJson,
          quizQuestions: [
            ...quizModelJson.quizQuestions,
            {
              id:  lastQuestion > 0
              ? quizModelJson.quizQuestions[lastQuestion - 1].id + 1
              : 0,
              question: value.trim(),
              answer: "",
              options: [],
              rightAnswers: [],
              optionFormat: "OneRightAnswer",
            }
          ]
        }));
        target.value = "";
      }
    };

    return (
      <>
        <div className="d-flex text-center mb-4 mt-2">
          <h4 className="col-8">Question</h4>
          <h4 className="col-8">Option</h4>
          <h4 className="col-4">New Option</h4>
        </div>
        <div className="d-block col p-2 text-center">
          {quizModelJson.quizQuestions.map((question: any, index: number) => (
            <SecondStepItem
              question={question}
              quizModelJson={quizModelJson}
              setQuizModelJson={setQuizModelJson}
              index={index}
            />
          ))}
          <input
            type="text"
            style={{ position: "relative", left: "55%" }}
            required
            placeholder="Enter a question and press Enter"
            onKeyDown={(e) => handleAddQuestion(e)}
          />
        </div>
      </>
    );
  }
);
