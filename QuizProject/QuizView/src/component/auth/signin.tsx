import '../../css/login.css'
import User from '@/class/User';
import { Context } from '@/main';
import { useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { singinAsync } from '@/http/userRequests';
import { SIGN_UP } from '@/utilit/paths';
import GoogleAuth from './googleAuth';
import { observer } from 'mobx-react-lite';


export const Signin = observer(() => {   
    const navigation = useNavigate();
    const { GoogleSignin, GoogleSignout } = GoogleAuth();
    const { user } = useContext(Context) as { user: User }; 

    const loginUser = {
        "email": "",
        "password": ""
    };
    
    const loginAsync = async () => {
        const emailRegex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i;
        const isValidEmail = emailRegex.test(loginUser.email);

        if (isValidEmail) {
            await singinAsync(loginUser, user);
        } else {
            alert(`Email: ${loginUser.email} is not valid`);
        }
    };

    return (
        <div id='login' className='login-div'>
            <form>
                <input type='text' required placeholder='Email' 
                    onChange={(e) => { loginUser.email = e.target.value; }}>
                </input>
                <input type='password' required placeholder='Password'
                    onChange={(e) => { loginUser.password = e.target.value; }}>
                </input>
            </form>
            <div>
                {
                user && user.email === undefined && (
                    <>
                        <button onClick={loginAsync}>Login</button>
                        <button onClick={() => navigation(SIGN_UP)}>Don't have account?</button>
                    </>
                )}
            </div>
            {user && user.email === undefined ? <GoogleSignin user={user}/> : <GoogleSignout/>}
        </div>
    );
});
