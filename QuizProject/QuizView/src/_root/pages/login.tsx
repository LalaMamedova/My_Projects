import { Outlet,Navigate } from "react-router-dom";

export const Login = ()=>{
    const isAuth = false;

    return(
        <>
        {isAuth ? (
            <Navigate to="/"/>
        ) :(

            <div className="d-flex">
                <Outlet/>
            </div>
            )
        }
        </>
    )
}

