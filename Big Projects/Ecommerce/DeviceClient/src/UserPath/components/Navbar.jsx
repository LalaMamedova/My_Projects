import "../css/navbar.css"
import "../css/dropdown.css"
import { useState,useRef, useEffect, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { BASKET, LIKEDPRODUCTS, MAIN_PAGE} from '../utilits/constPath';
import { getAllCategory } from '../../http/deviceRequest/deviceGetRequest';
import { Context } from '../..';
import { Row } from 'react-bootstrap';
import { observer } from "mobx-react-lite";
import CatalogItems from './modals/CatalogItems';
import SigninDropDown from './modals/SignInDropDown';
import {  useCookies } from "react-cookie";
import { logoutPost } from "../../http/userRequest";

const Navbar = observer(({user})=>{
    
    const {userProduct} = useContext(Context)
    const [catalogIsOpen, setCatalogIsOpen] = useState(false)
    const dropdownRef = useRef(null);
    const [cookies, setCookie, removeCookie] = useCookies();

    const navigate = useNavigate();

    useEffect(() => {
        try {
            getAllCategory().then(data=>userProduct.setCategory(data))
        } catch (error) {}
            document.addEventListener('mousedown', handleClickOutside);
        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
    }, []);

    const handleClickOutside = (event) => {
        if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
            setCatalogIsOpen(false);
        }
    };
    const handleBasketClick=()=>{
        navigate(BASKET)
    }

    async function signOutHandler(){
        try {
            await logoutPost(user.userInfo.id)
            .then(isLogin=> 
                {
                    user.setIsAuth(isLogin);
                    removeCookie("user");
                });
        } catch (error) {
            user.setIsAuth(false);
            removeCookie("user");
        }
       
       
    }

    return(
        <header >
               <h1 style={{cursor:"pointer"}} onClick={()=>navigate(MAIN_PAGE)}>Devices</h1>

                 <nav className="center-side" ref={dropdownRef}>
                    <button className='nav-btn' id='catalog-btn' onClick={()=>setCatalogIsOpen(!catalogIsOpen)}>
                        Catalog <i className={`fa ${catalogIsOpen ? 'fa-chevron-up' : 'fa-chevron-down'}`} 
                        aria-hidden="true"></i>
                        {
                            catalogIsOpen &&
                            <Row className="catalog-menu">
                            {
                              userProduct.category  && 
                                userProduct.category.map(category=>(
                                    <CatalogItems setCatalogIsOpen = {setCatalogIsOpen} 
                                    subcategory={category.subCategories} category = {category} />
                                ))}
            
                            </Row>
                        }
                    </button>
                </nav>

                <nav className='right-side'>
                    <button onClick={()=>navigate(LIKEDPRODUCTS)} style={{width:"60px"}} className="nav-btn">
                    <i class="fa fa-heart" aria-hidden="true"></i></button>

                    <button onClick={handleBasketClick} style={{width:"60px"}} className="nav-btn">
                    <i class="fa fa-shopping-basket" aria-hidden="true"></i></button>

                    {
                     user.isAuth===false  
                     ?<SigninDropDown/>
                     :<button className="nav-btn" 
                      onClick={signOutHandler}><i class="fa fa-sign-out" aria-hidden="true"></i> Sign out
                      </button>
                    }
             
                </nav>
                
        </header>
    )
})

export default Navbar;