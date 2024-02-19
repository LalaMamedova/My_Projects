import { getAllAsync, postAsync } from "./requestTemplates";

export const getAllQuizAsync = async () => {
  return await getAllAsync("Quiz");
};

export const postQuizAsync = async (quiz: object) => {
  var response = await postAsync("Quiz/create", quiz);
  return response;
};
