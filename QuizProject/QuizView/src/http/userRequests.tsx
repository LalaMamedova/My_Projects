import User from "@/class/User";
import "./requestTemplates";
import { getWithToken, postAsync } from "./requestTemplates";

export const googleSigninAsync = async (user: User) => {
  await getWithToken(`User/google-auth`).then((data) => {
    localStorage.setItem("user", JSON.stringify(data)), user.setData(data);
  });
};

export const singinAsync = async (signinUser: object, user: User) => {
  await postAsync("User/signin", signinUser).then((data) => {
    localStorage.setItem("user", JSON.stringify(data));
    localStorage.setItem("token", data.userToken.acssesToken);
    user.setData(data);
  });
};

export const singupAsync = async (user: object) => {
  const response = await postAsync("User/signup", user);
  return response.json();
};

export const singOut = async (userId: string) => {
  try {
    const response = await getWithToken(`User/sign-out/${userId}`);
    return response.json();
  } catch (error) {
    console.log(error);
  }
};
