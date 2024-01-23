import { useEffect, useRef, useState } from "react";
import { SIGNIN, SIGNUP } from "../../utilits/constPath";
import { useNavigate } from "react-router-dom";

const SigninDropDown=()=>{
    const [open, setOpen] = useState(false);
    const navigate = useNavigate();
    const dropdownRef = useRef(null);

    const handleClickOutside = (event) => {
        if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
            setOpen(false);
        }
    };

    useEffect(() => {
        document.addEventListener('mousedown', handleClickOutside);
        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
    }, []);

    return(
        <div className="dropdown" ref={dropdownRef}>
                        <button className='dropdown-btn' onClick={() => setOpen(!open)}>
                            <i className="fa fa-user" aria-hidden="true"></i> Account
                            <i className={`fa ${open ? 'fa-chevron-up' : 'fa-chevron-down'}`} aria-hidden="true"></i>
                        </button>
                        {open ? (
                            <ul className="menu">
                                <li className="menu-item">
                                    <button  onClick={() => navigate(SIGNIN) } >
                                        <i className="fa fa-sign-in" aria-hidden="true"></i> Sign in
                                    </button>
                                </li>
                                <li className="menu-item">
                                    <button onClick={() => navigate(SIGNUP) }>
                                        <i className="fa fa-user-plus" aria-hidden="true"></i>  Sign up
                                    </button>
                                </li>
                            </ul>
                        ) : null}
            </div>
    )
}

export default SigninDropDown;