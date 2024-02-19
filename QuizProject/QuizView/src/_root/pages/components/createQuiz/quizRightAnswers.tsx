import { useQuizModelHooks } from "@/hooks/quizModelHooks";
import { Context } from "@/main";
import { useContext } from "react";

export const QuizRightAnswers = ({
  quizModelJson,
  setQuizModelJson,
  question,
  option,
  index,
}: {
  quizModelJson: any;
  setQuizModelJson: React.Dispatch<React.SetStateAction<any>>;
  option: string | undefined;
  question: any;
  index: number;
}) => {
  
  const { types } = useContext(Context) as { types: string[] };
  const {updateQuizQuestions} = useQuizModelHooks();
  const handleRightAnswer = (e: React.ChangeEvent<HTMLInputElement>) => {
    const check = e.target.checked;
    const value = e.target.value;

    if (question.optionFormat === types[0]) {
      question.rightAnswers[0] = value;
    } else if (question.optionFormat === types[1]) {
      if (check) {
        question.rightAnswers.push(value);
      } else {
        question.rightAnswers = question.rightAnswers.filter(
          (x: string) => x !== value
        );
      }
    }

    updateQuizQuestions({
      setQuizModelJson,
      quizModelJson,
      question,
    });  };

  const handleScores = (
    e: React.ChangeEvent<HTMLInputElement>,
    optionIndex: number
  ) => {
    question.rightAnswers[optionIndex] = e.target.value;
    updateQuizQuestions({
      setQuizModelJson,
      quizModelJson,
      question,
    });
  };

  return (
    <div className="d-flex mt-2" style={{position:"relative",left:"35px"}}>
      {quizModelJson.type !== "Psychological" && question.optionFormat === types[0] && (
        <input key={index}
          type="radio"
          checked={question.rightAnswers.includes(option)}
          style={{height:"15px", width:"30px"}}
          value={option}
          onChange={handleRightAnswer}
        />
      )}
      
      {question.optionFormat === types[1] &&
        quizModelJson.type !== "Psychological" && (
          <input key={index}
            type="checkbox"
            checked={question.rightAnswers.includes(option)}
            style={{height:"15px", width:"30px"}}
            value={option}
            onChange={handleRightAnswer}
          />
        )}
      {question.optionFormat === types[2] &&  quizModelJson.type !== "ForFun" && (
        <input key={index}
          type="number"
          style={{height:"25px", width:"120px"}}
          onChange={(e) => handleScores(e, index)}
        />
      )}

      <h5>{`${index + 1}: ${option}`}</h5>
    </div>
  );
};
