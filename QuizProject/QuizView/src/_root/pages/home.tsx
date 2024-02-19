import { CREATE_QUIZ, QUIZ } from "@/utilit/paths";
import { Link } from "react-router-dom";

export const HomeMain = () => {
  return (
    <div>
      <li>
        <Link to={`${QUIZ}`}>To quiz</Link>
      </li>
      <li>
        <Link to={`${QUIZ}/${CREATE_QUIZ}`}>Create new quiz</Link>
      </li>
    </div>
  );
};
