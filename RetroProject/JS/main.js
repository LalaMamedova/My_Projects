import { cardTemp,changeBtns } from "./templates.js";
import { get,remove } from "./apiMethods.js";
import { changeLoadScreen, changeDefaultElements,btnClick,changeArrBackground } from "./changeMode.js";
let isDarkMode = localStorage.getItem('mode') === 'true'? true:false;

document.addEventListener("DOMContentLoaded", function() {
    
    const modebtn = $('#mode-btn');
    const addTempBtn = document.querySelector('#add-temp');

    var techList = document.querySelector('#tech-list');
    var cardDiv = '';
    var AllCards = [];

    window.onload = async function () {
      
        await changeLoadScreen(isDarkMode);
        await loadPage(1500);
        sessionStorage.clear();
        changeBtns();
    };
    
    async function loadPage(loadAnimationDelay) {
        
        setTimeout(() => {$('#loader-container').css({'display': 'none'})},loadAnimationDelay);

        try {
            AllCards = await get('technologies');
        } catch (error) {
            console.log(error);
        }

        if(AllCards.length>0){
            AllCards.forEach(data => {
                techList.innerHTML +=`${cardTemp(data.id,data.images[0],data.name,data.year,data.type,data.description, data.interestingfacts[0])} `
            });
            cardDiv = document.querySelectorAll(".tech-card,#tech-card");
        }else{
            techList.innerHTML+=
            `<div class='no-res-div'>
                <h1>There are no technology.If you know some, please <a href ='AddRedactPage.html'>add</a></h1>
            </div>`;

        }
        
        await changeMode(isDarkMode);
    }

    modebtn.on("click", async function(){
        isDarkMode = btnClick(isDarkMode);
        await changeMode(isDarkMode);
    });


    async function changeMode(isDarkMode){

        changeDefaultElements(isDarkMode,"#mode-btn");

        if(isDarkMode === true){
            $('.side-bar-div,.side-bar-div-active').css({'background':'#2B3865'});
        }else{
            $('.side-bar-div,.side-bar-div-active').css({'background':'linear-gradient(85deg, #ffc273, #b222ff)'});
        }
    }

    
    addTempBtn.addEventListener('click',()=>{
        AllCards.forEach(data => {
            var card = cardTemp(data.id, data.images[0], data.name, data.year, data.type, data.description, data.interestingfacts[0]);
            techList.innerHTML += `${card}`;
        });
    });

    techList.addEventListener('click', (event) => {
        const target = event.target;
    
        if ( event.target.tagName === 'BUTTON') {
            const card = target.closest('.tech-card'); 

            if (card) {
                const techId = card.querySelector("h5").textContent;            
                AllCards.forEach(techs => {
                    if (techs.id == techId) {
                        if(target.value === 'info'){
                            sessionStorage.setItem('infoData', JSON.stringify(techs));
                            window.location.href = `/Html/InfoPage.html?tech=${techs.name}`;

                        }else if(target.value === 'delete'){
                            
                            remove("technologies",techId);
                            setTimeout(() => {location.reload();}, 120);
                        }
                        else if(target.value === 'edit'){
                            sessionStorage.setItem('editData', JSON.stringify(techs));
                            window.location.href = `/Html/AddRedactPage.html?tech=${techs.name}`;
                            
                        }
                    }
                
                });
                
            }
        }
    });

    $('#add-tech').on('click',function(){
        window.location.href = `/Html/AddRedactPage.html`;
    });


   

});