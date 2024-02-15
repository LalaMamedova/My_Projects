import User from "@/class/User"
import { CREATE_QUIZ } from "@/utilit/paths";
import { useNavigate } from "react-router-dom"

export const Index = ({ user }: { user: User })=>{
    
    const navigate = useNavigate();

    return(
        <div>
            {
                user !== null && (<button onClick={()=>navigate(CREATE_QUIZ)}>Create new quiz</button>) 
            }
        </div>
        
    )
}