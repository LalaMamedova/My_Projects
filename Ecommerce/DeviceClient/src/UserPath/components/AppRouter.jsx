import {Route,Routes,Navigate} from "react-router-dom";
import {authRoute,publicRoute } from '../utilits/route';
import { useContext, useEffect } from "react";
import { adminRoute } from "../../AdminPath/utilits/route";
import { Context } from "../..";
import Navbar from "./Navbar";
import BreadcrumbsProvider  from "../../componets/BreadcrumbsContext";
import {observer } from "mobx-react-lite";
import {SIGNIN } from "../utilits/constPath";
import "../css/index.css"
import { useCookies } from "react-cookie";


const AppRouter = observer(() => { 
  const { user } = useContext(Context);
  const [cookies] = useCookies();

  useEffect(() => {
  const userFromCookies = cookies["user"];

   if(userFromCookies !== undefined){
    user.setUserInfo(userFromCookies);
    user.setIsAuth(true);

    if(user.userInfo.role === "Admin"){
      user.setIsAdmin(true);
    }
   }
}, []);


return (
        <div className="App" >
          { user.isAdmin===false? (
            <>
            <Navbar user ={user}/>
              
            <main>
              <Routes>
                {publicRoute.map(({ path, element }) => (
                  <Route key={path} path={path} element={element} />
                ))}
                
                {user.isAuth ? (
                  authRoute.map(({ path, element }) => (
                    <Route key={path} path={path} element={element} />
                  ))
                ) 
                :(
                  <Route path="/*" element={<Navigate to={SIGNIN} />} />
                )}

                <Route path="/*" element={<Navigate to="/" />} />
              </Routes>
            </main>
          </>
          ):(
              <main>
              <BreadcrumbsProvider/>
                <Routes >
                  {user.isAdmin && adminRoute.map(({ path, element }) => (
                    <Route key={path} path={path} element={element} />
                    ))}
                  <Route path="/*" element={<Navigate to="/" />} />
                </Routes>
              </main>
            )}
        </div>
      );
})
export default AppRouter;