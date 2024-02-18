import '../../css/login.css'
import User from '@/class/User';
import { Link} from 'react-router-dom';
import { SIGN_UP } from '@/utilit/paths';
import { observer } from 'mobx-react-lite';
import { useLoginHook } from '@/hooks/loginHooks';
import GoogleAuth from '@/component/auth/googleAuth';


export const Signin = observer(({ user }: { user: User }) => {   
    const { GoogleSignin, GoogleSignout } = GoogleAuth();
    const {loginAsync} = useLoginHook();

    const loginUser = {
        "email": "",
        "password": ""
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
               (
                    user===null &&
                    <>
                        <button onClick={()=>loginAsync("reqular",loginUser)}>Login</button>
                        <button ><Link to={SIGN_UP}>Don't have account?</Link></button>
                    </>
                )}
            </div>
            {user === null  ? <GoogleSignin/> : <GoogleSignout/>}
        </div>
    );
});
