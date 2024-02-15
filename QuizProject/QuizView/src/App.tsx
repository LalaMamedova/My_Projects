import './css/App.css'
import './css/index.css'

import { BrowserRouter,  Route, Routes } from 'react-router-dom'
import {Login} from './_root/pages/login'
import { Signin } from './component/auth/signin'
import { Signup } from './component/auth/signup'
import { CREATE_QUIZ, HOME, SIGN_IN, SIGN_UP } from './utilit/paths'
import { Header } from './component/layout/header'
import { useContext, useEffect } from 'react'
import { Context } from './main'
import { Index } from './component/home'
import User from './class/User'
import { QuizMain } from './component/quiz'
import { CreateQuiz } from './_root/pages/createQuiz'

function App() {
  const { user } = useContext(Context) as { user: User }; 
  useEffect(() => {

    const userFromLocalStorageString = localStorage.getItem("user");

    if (userFromLocalStorageString !== null) {
      const userFromLocalStorage = JSON.parse(userFromLocalStorageString);
      user.setData(userFromLocalStorage);
    }
    
  }, []);
  
  return (
    <>
      <BrowserRouter>
        <Header />
        <main>
          <Routes>
            <Route path={HOME} element={<Index user={user}/>}/> 
          
            <Route element={<Login />}>
              <Route path={SIGN_IN} element={<Signin />} />
              <Route path={SIGN_UP} element={<Signup />} />
            </Route>

            <Route element={<QuizMain /> }>
              <Route path={CREATE_QUIZ} element={<CreateQuiz user={user} />} />
            </Route>
            
          </Routes>
        </main>
      </BrowserRouter>
    </>
  )
}

export default App
