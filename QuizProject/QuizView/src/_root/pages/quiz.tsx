import { CREATE_QUIZ } from "@/utilit/paths"
import { Link, Outlet, useLocation } from "react-router-dom"

export const Quiz =()=>{
    const location = useLocation();
    return(
        <div>
            {!location.pathname.includes(CREATE_QUIZ) && 
                <Link 
                    to={`${CREATE_QUIZ}`} >Create new quiz
                </Link>
            }
            <Outlet />
        </div>
    )
}