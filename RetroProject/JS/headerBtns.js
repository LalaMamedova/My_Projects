function changeIconInHeadersBtn(){
    if (window.innerWidth > 768) {
        headerIconBtnsInnerHTML('#sign-in-btn','user','Sign in','110px' ,'SignIn')
        headerIconBtnsInnerHTML('#sign-up-btn','user-plus','Sign up','110px','SignUp')
        headerIconBtnsInnerHTML('#cabinet-btn','home','Cabinet','110px','Cabinet')
    
    }else if (window.innerWidth < 768) {
        headerIconBtnsInnerHTML('#sign-in-btn','user','','45px','SignIn')
        headerIconBtnsInnerHTML('#sign-up-btn','user-plus','','45px','SignUp')
        headerIconBtnsInnerHTML('#cabinet-btn','home','','45px','Cabinet')
    }

    let isDarkMode = localStorage.getItem('mode') === 'true'? true:false;

    if(isDarkMode){
    $('i,#gradient-i')
        .css({
            'background-clip': 'text','-webkit-background-clip': 
            'text','background-clip': 'text','color':'transparent',
            'background-image': 'linear-gradient(180deg, #667fff, #ff87cb)'});
    }
    else{
        $('i').css({'color':'#261447'});
    }

}

function headerIconBtnsInnerHTML(btnIcon,icon,text,width,aRef){
    document.querySelector(btnIcon).innerHTML =  
    `<a href="${aRef}.html"><i class="fa fa-${icon} fa-fw w3-margin-right"></i></a>${text}`;
    document.querySelector(btnIcon).style.width = width;
}

window.addEventListener('resize', changeIconInHeadersBtn);
window.addEventListener('load', changeIconInHeadersBtn);
