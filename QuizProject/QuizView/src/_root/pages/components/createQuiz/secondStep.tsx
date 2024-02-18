import "../../../../css/create.css"
import { observer } from "mobx-react-lite";

export const SecondStep = observer(({newQuiz,quizQuestions, setQuizQuestions} : 
{newQuiz:any,quizQuestions:any[],  setQuizQuestions: React.Dispatch<React.SetStateAction<any[]>>}) => {
  
    const type = ["OneRightAnswer","ManyRightAnswer","Scores"];

    const handleAddQuestion = (e: React.KeyboardEvent<HTMLInputElement>) => {
        const target = e.target as HTMLInputElement; 
        const value = target.value;

        if (e.key === 'Enter' && value.trim() !== '') {

          setQuizQuestions(prevQuestions => [
            ...prevQuestions,
            {
              "id": prevQuestions.length > 0 ? prevQuestions[prevQuestions.length - 1].id + 1 : 0,
              "question": value.trim(),
              "answer": "",
              "options": [],
              "rightAnswers": [],
              "optionFormats":'OneRightAnswer'
            }
          ]);
          target.value = '';

        }
      };

      const handleAddOption = (e: React.KeyboardEvent<HTMLInputElement>, questionIndex:number) => {
        const target = e.target as HTMLInputElement; 

        const value = target.value;
        if (e.key === 'Enter' && value.trim() !== '') {
            const checkSameOption = quizQuestions.find(x => x.id === questionIndex)?.options.find((x: string) => x === value);
           
            if(checkSameOption===undefined){
                quizQuestions.find(x=>x.id === questionIndex).options.push(value.trim());
                setQuizQuestions([...quizQuestions]); 
                target.value = '';
            }else{
                alert(`${value} alredy is option to this question`)
            }
        }
      };

      const handleRightAnswer = (e:  React.ChangeEvent<HTMLInputElement> ,
         questionIndex: number) => {
        const check = e.target.checked;
        const value = e.target.value;
        const question = quizQuestions.find(x => x.id === questionIndex);

        if(question.optionFormats  === type[0]){
            question.rightAnswers = [];
            question.rightAnswers = value;
        }else if(question.optionFormats === type[1]){
            if (check){
                question.rightAnswers.push(value);
            } else {
                question.rightAnswers = question.rightAnswers.filter((x: string) => x !== value);
            }
        }
    
        setQuizQuestions([...quizQuestions]); 
        console.log(quizQuestions)
    };

    console.log(quizQuestions)
    
   
    return (
        <>
            <div className="d-flex text-center mb-4 mt-2" >
                <h4 className="col-8">Question</h4>
                <h4 className="col-8">Option</h4>
                <h4 className="col-4">New Option</h4>
            </div>
            <div className="d-block col p-2 text-center">
                {quizQuestions.map((question, index) => (
                    <div key={index} className="d-flex mb-4">
                        <div  key={index} className="d-flex m-0 mb-4 col-8" >
                            <div className="col">
                                <h5>{`${index+1}: ${question.question}`}</h5>
                                <span className="remove">{" Remove"}</span>
                                <span className="edit">{" Edit"}</span>
                            </div>

                            <select 
                                onChange={(e) => {question.optionFormats = (e.target.value); console.log(question)}}
                                className="mr-4 ml-4">
                                <option value="OneRightAnswer">One right answer</option>
                                <option value="ManyRightAnswer">Many right answer</option>
                                <option value="Scores">Scores for each option</option>
                            </select>

                        </div>
                            
                        <div className="option-div d-block col-8 " >
                            {question.options.length > 0 ? (
                                question.options.map((option: string | undefined, index:number) => (
                                    <div key={index} className="col mb-2">

                                        <div className="d-flex ">
                                            {
                                               question.optionFormats  === type[0] &&
                                               <input type="radio"     
                                               name="options" 
                                               checked={question.rightAnswers.includes(option)}
                                               className="w-50 h-25" value={option}
                                               onChange={(e)=>handleRightAnswer(e,question.id)}/>
                                           }
                                            {
                                                question.optionFormats  === type[1] &&
                                                <input type="checkbox"    
                                                checked={question.rightAnswers.includes(option)}
                                                className="w-50 h-25" value={option}
                                                onChange={(e)=>handleRightAnswer(e,question.id)}/>
                                            }
                                            <h5>{`${index+1}: ${option}`}</h5>
                                        </div>
                                            <span className="remove">{" Remove"}</span>
                                            <span className="edit">{" Edit"}</span>
                                        </div>
                                ))
                            ) : (
                                <h3 className="text-danger">ADD OPTION</h3>
                            )}
                        </div>
                        <input className="ml-4 mr-4"
                            type="text"
                            required
                            placeholder="Press Enter to add"
                            onKeyDown={(e)=>handleAddOption(e, question.id)} />
                    </div>
                ))}
                <input
                    type="text"
                    style={{position: "relative", left: "55%"}}
                    required
                    placeholder="Enter a question and press Enter"
                    onKeyDown={(e)=>handleAddQuestion(e)}
                />
            </div>
        </>
    );
    

   
});





