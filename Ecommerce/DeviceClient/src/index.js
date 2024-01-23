import React, { createContext } from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import Product from './models/UserProduct';
import User from './models/User';
import AdminProduct from './models/AdminProduct';
import BreadCrumbsPath from './models/BreadCrumbsPath';


export const Context = createContext(null);
const root = ReactDOM.createRoot(document.getElementById('root'));

root.render(
    <Context.Provider  value={{
        user: new User(),
        userProduct:new Product(),
        adminProduct:new AdminProduct(),
        breadPath:new BreadCrumbsPath(),
    }}> 
        <App />
    </Context.Provider>
);

