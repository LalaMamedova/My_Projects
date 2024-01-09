import { Navigate } from "react-router-dom";
import { BASKET, LIKEDPRODUCTS, MAIN_PAGE, PRODUCT, PURCHASE, SHOP, SIGNIN, SIGNUP } from "./constPath";
import MainPage from "../page/main";
import Auth from "../page/auth";
import Shop from "../page/shop";
import Product from "../page/productInfo";
import Basket from "../page/basket";
import Purchase from "../page/purchase";
import LikedProducts from "../page/likedProducts";


export const publicRoute = [
    {
        path: '/',
        element: <Navigate to={MAIN_PAGE} replace />,
    },
    {
        path: MAIN_PAGE,
        element: <MainPage/>
    },
    {
        path: SIGNIN,
        element: <Auth/>
    },
    {
        path: SIGNUP,
        element: <Auth/>
    },
    {
        path: SHOP,
        element: <Shop/>
    },
    {
        path: SHOP  + "/:category",
        element: <Shop/>
    },
    {
        path: PRODUCT  + "/:id",
        element: <Product/>
    },
    {
        path: BASKET,
        element: <Basket/>
    },
]

export const authRoute = [
    {
        path: PURCHASE,
        element: <Purchase />,
    },
    {
        path: LIKEDPRODUCTS,
        element: <LikedProducts />,
    },
    {
        path: '/',
        element: <Navigate to={SIGNIN} replace />,
    },
]


