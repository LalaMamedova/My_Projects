import { useContext } from "react";
import { Outlet, useNavigate } from "react-router-dom";
import { Context } from "./main";
import User from "./class/User";
import { useLoginHook } from "./hooks/loginHooks";
import { SIGN_IN, SIGN_UP } from "./utilit/paths";

export const Layout = () => {
  const navigate = useNavigate();
  const { user } = useContext(Context) as { user: User };
  const { logOutAsync } = useLoginHook();
  return (
    <>
      <header>
        <h2 onClick={() => navigate("/")}>Beon</h2>
        <nav className="nav-btns">
          {user === null ? (
            <>
              <button onClick={() => navigate(SIGN_IN)}>Login</button>
              <button onClick={() => navigate(SIGN_UP)}>Signup</button>
            </>
          ) : (
            <>
              <button>Profile</button>
              <button onClick={logOutAsync}>Log out</button>
            </>
          )}
        </nav>
      </header>

      <main className="d-flex">
        <Outlet></Outlet>
      </main>

      <footer>2024</footer>
    </>
  );
};
