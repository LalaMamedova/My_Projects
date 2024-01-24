import { changeDefaultElements,changeClassNameId,btnClick } from "./changeMode.js";
import { post } from "./apiMethods.js";
let isDarkMode = true;
const modebtn = $('#mode-btn');
const email = $('#email-input');
const username = $('#username-input');
const password = $('#password-input');
const confirm = $('#confirm-input');
const submit = $('.submit-sign-up');

window.onload = function() {
    isDarkMode = localStorage.getItem('mode') === 'true'? true:false;
    changeMode(isDarkMode);
};

function changeMode(isDarkMode){
    changeDefaultElements(isDarkMode,'#mode-btn');
    if(isDarkMode === true){
        changeClassNameId('.main-div','main-div-dark',false);
    }else{
        changeClassNameId('.main-div-dark','main-div',false);
    }
}

modebtn.on('click',function(){
    isDarkMode = btnClick(isDarkMode);
    changeMode(isDarkMode);
});
submit.on('click', async function(){
    if(email.val() != '' && password.val()!='' && confirm.val()!='' && username.val()!=''){
        if(password.val() === confirm.val()){
            const sendUser = {
                email:email.val(),
                username:username.val(),
                password: password.val(),
            };

            const result = await post('users', sendUser);
            registration(result);

        }else{
            $('.error-message').text('Passwords is the not the same').css({'display':'flex'});
        }
        
    }else{
        $('.error-message').text('Fill all the field, please').css({'display':'flex'});
    }
});



function registration(result){
    if (result.success){
        let getUser = result.data;
        sessionStorage.setItem('user',getUser.email);

        setTimeout(() => {
            location.href = "/Html/SignIn.html";;}, 1800);
            signUp('You have successfully registered!')

    }else if(result.status === 400){
        signUp("You'r email is alredy is registred. Please,try another email");
    }
    else{
        signUp('Something went wrong, try again!');
    }
}

function signUp(yourMessage){
    let btnClassName = 'main-button';
    if(localStorage.getItem('mode') === 'true'){
        btnClassName+='-dark';
    }
    document.querySelector('main').innerHTML = '';
    document.querySelector('main').innerHTML += `
    <div class="auto-res">${yourMessage}
        <button id="reload-btn" lass=${btnClassName}>Retry</button>
    </div>`;

    $("#reload-btn").on('click',function(){
        console.log('click');
        location.reload();
    });
}


