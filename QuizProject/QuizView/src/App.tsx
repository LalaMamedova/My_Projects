import "./css/App.css";
import "./css/index.css";

import User from "./class/User";
import {
  Route,
  RouterProvider,
  createBrowserRouter,
  createRoutesFromElements,
} from "react-router-dom";
import { Signin } from "./_root/pages/signin";
import { Signup } from "./_root/pages/signup";
import { CREATE_QUIZ, QUIZ, SIGN_IN, SIGN_UP } from "./utilit/paths";
import { useContext, useEffect, useState } from "react";
import { Context } from "./main";
import { CreateQuiz } from "./_root/pages/components/createQuiz/createQuiz";
import { HomeMain } from "./_root/pages/home";
import { Layout } from "./layout";
import { RequireAuth } from "./hooks/requareAuth";
import { Quiz } from "./_root/pages/quiz";
import { ErrorPage } from "./_root/pages/error";
import "bootstrap/dist/css/bootstrap.min.css";

function App() {
  const { user, setUser } = useContext(Context) as {
    user: User;
    setUser: (value: User | null) => void;
  };
  const [userId, setUserId] = useState();

  useEffect(() => {
    const userFromLocalStorageString = localStorage.getItem("user");
    if (userFromLocalStorageString !== null) {
      user.setData(JSON.parse(userFromLocalStorageString));
      setUserId(JSON.parse(userFromLocalStorageString).id);
    } else {
      setUser(null);
    }
  }, []);

  const router = createBrowserRouter(
    createRoutesFromElements(
      <Route path="/" element={<Layout />} errorElement={<ErrorPage />}>
        <Route index element={<HomeMain />} />

        <Route path={SIGN_IN} element={<Signin user={user} />} />
        <Route path={SIGN_UP} element={<Signup />} />

        <Route path={QUIZ} element={<Quiz />}>
          <Route
            path={CREATE_QUIZ}
            element={
              <RequireAuth user={user}>
                <CreateQuiz userId={userId} />
              </RequireAuth>
            }
          />
        </Route>
      </Route>
    )
  );
  return (
    <>
      <RouterProvider router={router}></RouterProvider>
    </>
  );
}

export default App;
