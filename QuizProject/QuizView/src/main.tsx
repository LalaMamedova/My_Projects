import React, { createContext, useState } from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import User from './class/User';

export const Context = createContext({});
const Root = () => {
  const [user, setUser] = useState<User | null>(new User()  || null);
  const userValue = {user,setUser}

  return (
    <React.StrictMode>
      <Context.Provider value={userValue}>
        <App />
      </Context.Provider>
    </React.StrictMode>
  );
};

ReactDOM.render(<Root />, document.getElementById('root'));
