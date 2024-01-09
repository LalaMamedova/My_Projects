import { Navigate } from "react-router-dom";
import MainPage from "../page/main";
import { MAIN_ADMIN_PAGE } from "./constPath";


export const adminRoute = [
    {
        path: '/',
        element: <Navigate to={MAIN_ADMIN_PAGE} replace />,
    },
    {
        path: MAIN_ADMIN_PAGE,
        element: <MainPage/>
    },
    {
        path: MAIN_ADMIN_PAGE +"/:path",
        element: <MainPage/>
    },
    {
        path: MAIN_ADMIN_PAGE + "/:path/path",
        element: <MainPage/>
    },
   
]

