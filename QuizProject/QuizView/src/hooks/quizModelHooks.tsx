export const useQuizModelHooks = () => {
    
  const updateQuizQuestions = ({
    setQuizModelJson,
    quizModelJson,
    question,
  }: {
      setQuizModelJson:React.Dispatch<React.SetStateAction<any>>;
      quizModelJson: any;
      question:any;
  }) => {

    setQuizModelJson({
      ...quizModelJson,
      quizQuestions: quizModelJson.quizQuestions.map((q: { id: any }) =>
        q.id === question.id ? question : q
      ),
    });
  };

  const placeHolder = ()=>{}

  return {updateQuizQuestions,placeHolder}
};
