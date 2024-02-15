import User from "@/class/User";
import { useLoginHooks } from "@/hooks/loginHooks";
import { Context } from "@/main";
import { SIGN_IN, SIGN_UP } from "@/utilit/paths";
import { useContext } from "react";
import { useNavigate } from "react-router-dom"

export const Header = ()=>{
    const navigate = useNavigate();
    const { user } = useContext(Context) as { user: User }; 
    const logOutAsync =  useLoginHooks()
  
    
    return(
        <header > 
            <h2 onClick={(()=>navigate("/"))}>Beon</h2>
            <nav  className="nav-btns">
                {
                    user && user.email===undefined  ? 
                    (<>
                        <button onClick={(()=>navigate(SIGN_IN))}>Login</button>
                        <button onClick={(()=>navigate(SIGN_UP))}>Signup</button>
                    </>)
                    :(
                    <>
                        <button>Profile</button>
                        <button onClick={logOutAsync}>Log out</button>
                    </>
                    )
                }
               
            </nav>
        </header>
    )
}