import "../../../../css/create.css"
import { observer } from "mobx-react-lite";

export const ThirdStep = observer(({quizResults, setQuizResults} : 
{quizResults:any[],  setQuizResults: React.Dispatch<React.SetStateAction<any[]>>}) => {
  
    const handleAddQuestion = (e: KeyboardEvent<HTMLInputElement>) => {
        const value = e.target.value;
        if (e.key === 'Enter' && value.trim() !== '') {

          setQuizQuestions(prevQuestions => [
            ...prevQuestions,
            {
              "id": prevQuestions.length > 0 ? prevQuestions[prevQuestions.length - 1].id + 1 : 0,
              "question": value.trim(),
              "answer": "",
              "options": [],
              "rightAnswers": [""],
            }
          ]);

          e.target.value = '';

        }
      };
      

      const handleAddOption = (e: KeyboardEvent<HTMLInputElement>, questionIndex:number) => {
        
        const value = e.target.value;
        if (e.key === 'Enter' && value.trim() !== '') {
            quizQuestions.find(x=>x.id === questionIndex).options.push(value.trim());
            setQuizQuestions([...quizQuestions]); 
            e.target.value = '';
        }
      };
   
      return (
        <>
            
        </>
    );
    

   
});





