export function changeDefaultElements(isDarkMode,modebtnId){

    if(isDarkMode === true){
        document.body.style.background = '#091324';
        document.querySelector(modebtnId).querySelector('i').className = 'fa fa-moon';
        $('.main-header-div' ).css({'filter':'brightness(80%)'});
        $('#loader-container').css({'background':'#0a0f2e'});
        $('h1,h2,h3,h4,h5,h6,p,label,span').css({'color':'white'});
        changeClassNameId('.main-button','main-button-dark',false)
        changeClassNameId('.music-button','music-button-dark',false)
        changeClassNameId('#loader-bar','loader-bar-pink',true)
        changeClassNameId('#film-roll','film-roll-pink',true)
        $('i,#gradient-i')
        .css({
            'background-clip': 'text','-webkit-background-clip': 
            'text','background-clip': 'text','color':'transparent',
            'background-image': 'linear-gradient(180deg, #667fff, #ff87cb)'});
        changeArrBackground('#tech-card,.tech-card','#2B3865');
            
      
    }else{
        document.body.style.background = "linear-gradient(285deg, #34eeff, #ff269d)";
        document.querySelector(modebtnId).querySelector('i').className = 'fa fa-sun';
        $('.main-header-div' ).css({'filter':'brightness(100%)'});
        $('#loader-container').css({'background':'linear-gradient(285deg, #34eeff, #ff269d)'});
        $('h1,h2,h3,h4,h5,h6,p,label,span').css({'color':'black'});
        $('i').css({'color':'#261447'});
        changeClassNameId('.main-button-dark','main-button',false)
        changeClassNameId('.music-button-dark','music-button',false)
        changeArrBackground('#tech-card,.tech-card','linear-gradient(135deg, #1e38ff, #ff78c7)');
        changeClassNameId('#loader-bar-pink','loader-bar',true)
        changeClassNameId('#film-roll-pink','film-roll',true)

    }
    changeArrColor('#tech-fact','lightblue');
}
export async function changeLoadScreen(isDarkMode){
    if(isDarkMode){
        $('#loader-container').css({'background':'#0a0f2e'})
        changeClassNameId('#loader-bar','loader-bar-pink',true)
        changeClassNameId('#film-roll','film-roll-pink',true)
    }else{
        $('#loader-container').css({'background':'linear-gradient(285deg, #34eeff, #ff269d)'})
        changeClassNameId('#loader-bar-pink','loader-bar',true)
        changeClassNameId('#film-roll-pink','film-roll',true)
    }
}

export function changeClassNameId(className,classNewName,classOrId){
    let changeId = document.querySelectorAll(className);
    changeId.forEach(element=>{
        classOrId === true? element.id = classNewName : element.className=classNewName;
    });
}


export function changeArrBackground(className,color){
    document.querySelectorAll(className).forEach(element=>{
        element.style.background = color;
    });
}

export function changeArrColor(className,color){
    document.querySelectorAll(className).forEach(element=>{
        element.style.color = color;
    });
}

export function btnClick(isDarkMode){
    if(isDarkMode === false) {isDarkMode = true;} else{isDarkMode = false;}
        localStorage.setItem('mode', isDarkMode);
        return isDarkMode;
}
