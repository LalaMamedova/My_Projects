import User from "@/class/User";
import { SIGN_IN } from "@/utilit/paths";
import { ReactNode } from "react";
import { Navigate, useLocation } from "react-router-dom";

export const RequireAuth = ({
  user,
  children,
}: {
  user: User;
  children: ReactNode;
}) => {
  const location = useLocation();
  if (user === null) {
    return <Navigate to={SIGN_IN} state={{ from: location }}></Navigate>;
  } else {
    return children;
  }
};
