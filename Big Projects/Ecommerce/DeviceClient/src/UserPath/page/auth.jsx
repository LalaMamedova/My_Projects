import React, { useContext, useEffect, useState } from "react";
import { loginPost, registrationPost } from "../../http/userRequest";
import { MAIN_PAGE, SIGNIN, SIGNUP } from "../utilits/constPath";
import { MAIN_ADMIN_PAGE } from "../../AdminPath/utilits/constPath";
import { Container } from "react-bootstrap";
import { useLocation, useNavigate } from "react-router-dom";
import { observer } from 'mobx-react-lite'
import { Context } from "../..";
import { useCookies } from "react-cookie";
import "../css/auth.css"
import { getUserProducts } from "../../http/deviceRequest/deviceGetRequest";

const Auth = observer(() =>{

    const {user} = useContext(Context)
    const [email,setEmail] = useState();
    const [password,setPassword] = useState();
    const [confirmPassword,setConfirmPassword] = useState();
    const [username,setUsername] = useState();
    const [authState, setAuthState] = useState();
    const [authResponse, setAuthResponse] = useState([]);
    const navigation = useNavigate();
    const location = useLocation();
    const [cookies, setCookies] = useCookies();
    const signin = "Sign in";
    const signup = "Sign up";

    useEffect(() => {
        const pathSegments = location.pathname.split('/');

        if (pathSegments[1] === 'signin') {
            setAuthState(signup);
        } else {
            setAuthState(signin);
        }
    }, [location.pathname]);
  

    function handleAuthState(){

        if(authState===signin){
            navigation(SIGNIN)
            setAuthState(signup)
        }else{
            navigation(SIGNUP)
            setAuthState(signin)
        }
    }


    async function login(){
        const userJson = {
            "email": email,
            "password": password
        };
        
        try {
            const userData = await loginPost(userJson);
            localStorage.setItem("authToken", userData.token.acssesToken);
            setCookies("user",userData);

            user.setIsAuth(true);   
            user.setUserInfo(userData);
                     
            if(userData.role === "Admin") {
                user.setIsAdmin(true);
                navigation(MAIN_ADMIN_PAGE);
            }else{
                await getUserProducts(user.userInfo.id)
                .then(data=>setCookies("userLikedProducts",data));
                navigation(MAIN_PAGE);
            }
           
        } catch (error) {
            setAuthResponse(JSON.parse(error));
        }
    }
    
    async function registration()
    {
        if (password === confirmPassword) {
            const userJson = {
                "email": email,
                "password": password,
                "username": username,
            };
            try {
                var data = await registrationPost(userJson);
                setAuthResponse(data);
                navigation(SIGNIN);
    
            } catch (error) {
                setAuthResponse(JSON.parse(error))
            }
            
        } else {
            alert("Password and confirm password not the same");
        }
    }
    
    async function authHandler() {
        if (authState === signup) {
            await login();
        } else {
            await registration();
        }
    }
    
  
    return (

        <Container >
            <div className="auth-container">
                <form className="auth-form">
                    {
                    authState === signin ?
                        (
                            <>
                                <input onChange={(e)=>setEmail(e.target.value)} type="text" placeholder="Email"  />
                                <input onChange={(e)=>setUsername(e.target.value)} type="text" placeholder="Username" />
                                <input onChange={(e)=>setPassword(e.target.value)} type="password" placeholder="Password" />
                                <input onChange={(e)=>setConfirmPassword(e.target.value)} type="password" placeholder="Confirm Password"  />
                            </>
                        )
                        :
                        (
                            <>
                                <input onChange={(e)=>setEmail(e.target.value)} type="text" placeholder="Email" />
                                <input onChange={(e)=>setPassword(e.target.value)} type="password" placeholder="Password" />
                            </>
                        )
                        
                    }

                
                    <button className="auth-btn mt-5" type="submit"  onClick={(e) => { e.preventDefault(); authHandler(); }}>
                        {
                            authState!== signin ?signin:signup
                        }
                    </button>
                    
                    <nav className="auth-nav">

                        {
                            authState=== signup ?
                            (
                                <div>
                                    <span className="forgot-pass">Forgot password</span>
                                    <span onClick={handleAuthState} className="signup">{authState}</span>
                                </div>
                            ) :
                            (
                            <span onClick={handleAuthState} className="signin">{authState}</span>
                            )
                        }
                    </nav>

                    {
                        authResponse && authResponse.length> 0 ?
                        <div className="error">
                            {
                                authResponse.map(errors=>(
                                    <p>{errors.description}</p>

                                ))
                            }
                        </div>: 
                        authResponse && 
                        <p style={{color: authResponse.description === "Succeed registration" ? "green":"red"}} 
                         className="error" > {authResponse.description}
                        </p>
                    }
                </form>
                
            </div>
        </Container>
    );
})
export default Auth;