import React, { createContext } from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import User from './class/User.tsx';
export const Context = createContext({});

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
   <Context.Provider value={
    {
      user: new User()
    }}> 
      <App />
    </Context.Provider>
  </React.StrictMode>
)
