import { useContext } from "react";
import { QuizRightAnswers } from "./quizRightAnswers";
import { Context } from "@/main";
import { useQuizModelHooks } from "@/hooks/quizModelHooks";

export const SecondStepItem = ({
  question,
  index,
  quizModelJson,
  setQuizModelJson,
}: {
  question: any;
  quizModelJson: any;
  index: number;
  setQuizModelJson: React.Dispatch<React.SetStateAction<[]>>;
}) => {

  const { types } = useContext(Context) as { types: string[] }; 
  const {updateQuizQuestions} = useQuizModelHooks();


  const handleAddOption = (e: React.KeyboardEvent<HTMLInputElement>) => {
    const target = e.target as HTMLInputElement;
    const value = target.value;

    if (e.key === "Enter" && value.trim() !== "") {
      const checkSameOption = question?.options.find(
        (x: string) => x === value
      );
      
      if (checkSameOption === undefined) {
        question.options.push(value.trim());
        if (question.optionFormat !== types[0]) {
          question.rightAnswers.push("");
        }
        updateQuizQuestions({
          setQuizModelJson,
          quizModelJson,
          question,
        });

        target.value = "";
      } else {
        alert(`${value} alredy is option to this question`);
      }
    }
  };

  const handleChangeOption = (e: { target: { value: any } }) => {
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
    <div key={index} className="d-flex mb-4">
      <div key={index} className="d-flex m-0 mb-4 col-8">
        <div className="col">
          <h5>{`${index + 1}: ${question.question}`}</h5>
          <span className="remove">{" Remove"}</span>
          <span className="edit">{" Edit"}</span>
        </div>
        {quizModelJson.type !== "ForFun" && (
          <select onChange={handleChangeOption} className="mr-4 ml-4">
            <option value="OneRightAnswer">One right answer</option>
            <option value="ManyRightAnswer">Many right answer</option>
            <option value="Scores">Scores for each option</option>
          </select>
        )}
      </div>

      <div className="option-div d-block col-8 ">
        {question.options.length > 0 ? (
          question.options.map((option: string | undefined, index: number) => (
            <div key={index} className="col mb-2">
              <div className="d-flex ">

                {quizModelJson.type !== "ForFun" ?(
                  <QuizRightAnswers
                    index={index}
                    option={option}
                    quizModelJson={quizModelJson}
                    setQuizModelJson={setQuizModelJson}
                    question={question}
                  />
                ):
                  <h5>{`${index + 1}: ${option}`}</h5>
                }
              </div>
              <span className="remove">{" Remove"}</span>
              <span className="edit">{" Edit"}</span>
            </div>
          ))
        ) : (
          <h3 className="text-danger">ADD OPTION</h3>
        )}
      </div>
      <input
        className="ml-4 mr-4"
        type="text"
        required
        placeholder="Press Enter to add"
        onKeyDown={handleAddOption}
      />
    </div>
  );
};
