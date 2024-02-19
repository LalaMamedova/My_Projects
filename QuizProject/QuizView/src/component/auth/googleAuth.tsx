import { useLoginHook } from "@/hooks/loginHooks";
import { gapi } from "gapi-script";
import { useEffect } from "react";
import GoogleLogin, { GoogleLogout } from "react-google-login";

const GoogleAuth = () => {
  const clientId =
    "803934699147-922f3qvihvq77k9ukenrnkcu0cv4evor.apps.googleusercontent.com";

  useEffect(() => {
    gapi.load("client:auth2", () => {
      gapi.auth2.init({ clientId: clientId });
    });
  }, []);

  const GoogleSignin = () => {
    const { loginAsync } = useLoginHook();

    const responseGoogleSuccess = (response: any) => {
      loginAsync("google", response);
    };

    const responseGoogleFailure = (response: object) => {
      console.log("Неудачная аутентификация:", response);
    };

    return (
      <GoogleLogin
        clientId={clientId}
        buttonText="Google Login"
        onSuccess={responseGoogleSuccess}
        onFailure={responseGoogleFailure}
        cookiePolicy={"single_host_origin"}
      />
    );
  };

  const GoogleSignout = () => {
    const { logOutAsync } = useLoginHook();

    return (
      <GoogleLogout
        clientId={clientId}
        buttonText="Google Logout"
        onLogoutSuccess={logOutAsync}
      />
    );
  };

  return { GoogleSignin, GoogleSignout };
};

export default GoogleAuth;
