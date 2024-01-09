const factsContainer = document.querySelector(".add-fact-div");
const addFactsButton = document.getElementById("add-fact-button");
const addImgsButton = document.getElementById("add-img-char-button");
const imgContainer = document.querySelector(".add-img");
var allImgInputs = imgContainer.querySelectorAll('#input-img');
import {changeArrBackground } from "../changeMode.js";

addFactsButton.addEventListener("click", function() {
    let newFactDiv = document.createElement("div");
    newFactDiv.className = "add-box";
    newFactDiv.style.marginTop = '15px';

    let previusInput = document.querySelectorAll('#input-fact');
    let lastfact = previusInput[previusInput.length-1];

    let input = document.createElement("input");
    input.type = "text";
    input.id = "input-fact";

    newFactDiv.appendChild(input);
    
    if(lastfact.value!='' && factsContainer.childElementCount < 7){
        factsContainer.appendChild(newFactDiv);
        changeInputsBackground();
    }else if (lastfact.value === '') {
            alert('Введите факт');
    }else if (factsContainer.childElementCount >= 6){
            alert('Слишком много фактов');
    }
  
});


const techContainer = document.querySelector(".all-tech-char-div");
const addTechButton = document.getElementById("add-tech-char-button");

addTechButton.addEventListener("click", ()=> {
    
    let prevTechName = document.querySelectorAll('#input-tech-char-name');
    let prevTechValue =  document.querySelectorAll('#input-tech-char-value');
    let prevTechInputName = prevTechName[prevTechName.length-1];
    let prevTechInputValue = prevTechValue[prevTechValue.length-1];

    let newTechDiv = document.createElement("div");
    newTechDiv.className = "characteristic-input";
    newTechDiv.style.marginTop = '15px';

    let inputName = document.createElement("input");
    inputName.type = "text";
    inputName.id = "input-tech-char-name";
    inputName.placeholder = 'Название характеристики'

    let inputValue = document.createElement("input");
    inputValue.type = "text";
    inputValue.id = "input-tech-char-value";
    inputValue.placeholder = 'Значение характеристики'

    newTechDiv.appendChild(inputName);
    newTechDiv.appendChild(inputValue);

    if ( prevTechInputName.value !== '' && prevTechInputValue.value !== '') {        
        techContainer.appendChild(newTechDiv);
        changeInputsBackground();
        
    }else if (prevTechInputValue.value === '') {
        alert('Введите характеристику');
    }
});


const imgLimit = 3;

addImgsButton.addEventListener("click", () => {
    let imgs = document.querySelectorAll('#input-img');
    let prevImg = imgs[imgs.length-1];

    let newImgDiv = document.createElement("div");
    newImgDiv.id = "add-img-box";
    newImgDiv.className = "add-box";
    newImgDiv.style.marginTop = '15px';

    let input = document.createElement("input");
    input.type = "text";
    input.id = "input-img";

    newImgDiv.appendChild(input);

    if (imgContainer.childElementCount <=imgLimit && prevImg.value !== '') {
        imgContainer.appendChild(newImgDiv);
        changeInputsBackground();
    } else if (prevImg.value === '') {
        alert('Введите URL');
    } else if(imgContainer.childElementCount > imgLimit){
        alert(`Лимит фотографий: ${imgLimit}`)
    }
});

function changeInputsBackground(){
    let isDarkMode = localStorage.getItem('mode') === 'true'? true:false;

    if(isDarkMode === true){
        changeArrBackground('input,select,textarea','lightblue');
    }else{
        changeArrBackground('input,select,textarea','linear-gradient(100deg, #f2d9ff,#ff89df)');

    }
}