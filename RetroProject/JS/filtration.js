import { cardTemp } from "./templates.js";
import { changeArrBackground } from "./changeMode.js";
var mainCardDiv= document.querySelector("#tech-list");
const rangeInput = $("#yearRange");
let filtredDivs = []; 

rangeInput.attr("max", new Date().getFullYear());
$("#yearRange").on("input", function() {
    $("#yearChoice").text(rangeInput.val());
});

function filtred(inputId, cardInnerElement) {
    const userSearch = document.getElementById(inputId).value.toLowerCase();

    for(let i = 0; i< filtredDivs.length; i++){
        const techValue = filtredDivs[i].querySelector(cardInnerElement).textContent.toLowerCase();
        if(!techValue.includes(userSearch)){
            filtredDivs.splice(i,1) ;
            i--;

        }
    }

}

function sortByName() {filtred('search-name', 'h2');}
function sortByDescription() {filtred('description-search', 'p');}
function sortByType() {filtred('tech-type-box', 'h4');}

function sortByYear() {
    const userSearch = document.getElementById('yearChoice').textContent;

    console.log(userSearch);
    for(let i = 0; i< filtredDivs.length; i++){
        const techValue = filtredDivs[i].querySelector('h6').textContent;

        if(techValue<userSearch){
            filtredDivs.splice(i,1) ;
            i--;
        }
    }
 
}

$('#filter-btn').on('click', function () {
    if(document.querySelector('#sideBar').className = 'side-bar-div-active'){
        document.querySelector('#sideBar').className = ('side-bar-div');
    }
    let cardDiv = document.querySelectorAll(".tech-card");
    filtredDivs = Array.from(cardDiv); 
    mainCardDiv.innerHTML = '';

    sortByYear();
    sortByName();
    sortByDescription();
    sortByType();


    if (filtredDivs.length === 0) {
        mainCardDiv.innerHTML +=
            `${noFoundTemp()}`;
        return;
    }else{
            filtredDivs.forEach(card => {
            mainCardDiv.innerHTML +=
                `<div id="tech-card" class="tech-card">
                    ${card.innerHTML}
                </div>`;
        });

        let isDarkMode = localStorage.getItem('mode') === 'true'? true:false;
        isDarkMode === true? changeArrBackground('#tech-card','#2B3865'):changeArrBackground('#tech-card','linear-gradient(135deg, #1e38ff, #ff78c7)');
       
    }
});

function noFoundTemp(){
    let btnClassName = 'main-button';
    if(localStorage.getItem('mode') === 'true'){
        btnClassName+='-dark';
    }
    return `<div class='glitch' style="z-index:0; justify-content: center;display:flex; position:relative; margin:20%;">
            <h1 style="font-weight:bolder; color:cyan; font-size: 120px;">Sory,but there are no result</h1>
            <button id='reload-btn' class=${btnClassName} >Retry</button>
        </div>`
    $('#reload-btn').on('click',function(){
        location.reload();

    });
}

