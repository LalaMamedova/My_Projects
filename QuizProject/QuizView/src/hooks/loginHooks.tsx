import User from "@/class/User";
import { singOut } from "@/http/userRequests";
import { Context } from "@/main";
import { useContext } from "react";

export const useLoginHooks = ()=>{
    const { user } = useContext(Context) as { user: User }; 

    const logOutAsync = async()=>{

        await singOut( user.email as string)
        .then(()=>
        {
            user.clear();
            localStorage.removeItem("token");
        });
    }
    
    return logOutAsync;
}