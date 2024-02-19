import User from "@/class/User";
import { googleSigninAsync, singOut, singinAsync } from "@/http/userRequests";
import { Context } from "@/main";
import { useContext } from "react";
import { useLocation, useNavigate } from "react-router-dom";

export const useLoginHook = () => {
  const { user, setUser } = useContext(Context) as {
    user: User;
    setUser: (value: User | null) => void;
  };
  const navigate = useNavigate();
  const location = useLocation();
  const fromPage = location.state?.from?.pathname || "/";

  const loginAsync = async (loginMethod: string, response: any) => {
    if (loginMethod === "reqular") {
      const emailRegex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i;
      const isValidEmail = emailRegex.test(response.email);

      if (isValidEmail) {
        setUser(new User());
        await singinAsync(response, user);
        navigate(fromPage);
      } else {
        alert(`Email: ${response.email} is not valid`);
      }
    } else if (loginMethod === "google") {
      localStorage.setItem("token", response.tokenId);
      setUser(new User());
      googleSigninAsync(user);
      navigate(fromPage);
    }
  };

  const logOutAsync = async () => {
    await singOut(user.email as string).then(() => {
      setUser(null);
      localStorage.removeItem("user");
      localStorage.removeItem("token");
    });
  };

  return { logOutAsync, loginAsync };
};
