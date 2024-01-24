var css = document.querySelectorAll('link');
var url = window.location.href.split("/Html");
const sideBar = document.getElementById("sideBar");
const menuBtn = document.getElementById("menu-btn");
url = url[0];

function changeCss(){
    
    if (window.innerWidth > 1200 && css[0].href !== `${url}/Scss/Main.css`) {
        css[0].href = `${url}/Scss/Main.css`;
        menuBtn.style.display = 'none';
        sideBar.style.display = 'block';
        sideBar.className = 'side-bar-div';
        allDivMargin(10);
        
    }else if (window.innerWidth < 1200 && css[0].href !== `${url}/Scss/Main-Mobile.css`) {
        css[0].href = `${url}/Scss/Main-Mobile.css`;
        sideBar.style.display = 'none';       
        menuBtn.style.display = 'flex';
    
    }
}

window.addEventListener('resize', changeCss);
window.addEventListener('load', changeCss);

setTimeout(() => {
    menuBtn.addEventListener("click", function() {
        allTechDiv = document.querySelectorAll('.tech-card');
    
        if (!sideBar.className.includes('active')) {
            sideBar.className += '-active';
            sideBar.style.display = 'block'; 
            allDivMargin(30);
        } 
        else {
    
            sideBar.className = sideBar.className.replace('-active','');
            setTimeout(() => { sideBar.style.display = 'none';}, 250);
            allDivMargin(10);
        }
    });
    
}, 100);



function allDivMargin(marginPersent){
    let allTechDiv = document.querySelectorAll('.tech-card');
    allTechDiv.forEach(cards => {
        cards.style.marginLeft= `${marginPersent}%`;
    });
}

