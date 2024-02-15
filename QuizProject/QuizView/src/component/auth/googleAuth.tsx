import User from "@/class/User";
import { useLoginHooks } from "@/hooks/loginHooks";
import { googleSigninAsync } from "@/http/userRequests";
import { gapi } from "gapi-script";
import { useEffect } from "react";
import GoogleLogin, { GoogleLogout } from "react-google-login";

const GoogleAuth =  ()=>{
    const clientId="803934699147-922f3qvihvq77k9ukenrnkcu0cv4evor.apps.googleusercontent.com";

    useEffect(()=>{
        gapi.load("client:auth2",()=>{
          gapi.auth2.init({clientId:clientId})
        })
      },[])
   
      const GoogleSignin = ({ user }: { user: User }) => {
        const responseGoogleSuccess = (response: any) => {
            localStorage.setItem("token", response.tokenId);
            googleSigninAsync(user);
        }
        
        const responseGoogleFailure = (response: object) => {
            console.log('Неудачная аутентификация:', response);
        }

        return (
          <GoogleLogin
            clientId={clientId}
            buttonText="Google Login"
            onSuccess={responseGoogleSuccess}
            onFailure={responseGoogleFailure}
            cookiePolicy={'single_host_origin'}
          />
        );
      }
      


      const GoogleSignout = ()  => {

       const logOutAsync =  useLoginHooks();
  
        return (
          <GoogleLogout
           clientId={clientId}
            buttonText="Google Logout"
            onLogoutSuccess={logOutAsync}
          />
        );
      }
    
   

    return {GoogleSignin, GoogleSignout};
    
}


export default GoogleAuth;