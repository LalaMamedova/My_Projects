import { singupAsync } from '@/http/userRequests'
import '../../css/login.css'
import { useNavigate } from 'react-router-dom';
import { SIGN_IN } from '@/utilit/paths';
import { observer } from 'mobx-react-lite';
  
export const Signup = observer(()=>{
    const navigation = useNavigate();

    const signupUser = {
        "email":'elnaramam6@gmail.com',
        "password":'1234567',
        "confirmPassword":'1234567',
        "userName":'lala',
    }

    const singup = async ()=>{

        if(signupUser.confirmPassword == signupUser.password){
            try {
                const emailRegex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i;
                const isValidEmail = emailRegex.test(signupUser.email);

                if(isValidEmail){
                    await singupAsync(signupUser);
                    navigation(SIGN_IN);
                }else{
                    alert(`Email: ${signupUser.email} is not valid`)
                }

            } catch (error) {
                alert(JSON.stringify( error))
            }
           
        }else{
            alert("Password and confirm password not the same!")
        }
    }
    return(
        <div className="login-div">
            <form>
                <input type='text' placeholder='Email' 
                    onChange={(e)=>{signupUser.email = e.target.value}}>
                </input>

                <input type='text' placeholder='Username'
                    onChange={(e)=>{signupUser.userName = e.target.value}}>
                </input>

                <input type='password' placeholder='Password'
                    onChange={(e)=>{signupUser.password = e.target.value}}>
                </input>

                <input type='password' placeholder='Confirm'
                    onChange={(e)=>{signupUser.confirmPassword = e.target.value}}>
                </input>
            </form>
            <button onClick={singup}>Sign up</button>
           
            <button onClick={()=>navigation(SIGN_IN)}>Alredy have account?</button>

        </div>
    )
});

