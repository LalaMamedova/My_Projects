import { post,put } from "../apiMethods.js";
import {changeDefaultElements,btnClick,changeArrBackground } from "../changeMode.js";
import { changeBtns } from "../templates.js";

const inputName = document.querySelector("#input-name");
const inputDescription = document.querySelector("#input-description");
const inputDate = document.querySelector("#input-date");
const inputType = document.querySelector('#input-type');
var inputFact = document.querySelectorAll("#input-fact");
var inputImg = document.querySelectorAll('#input-img');

const techId = document.querySelector('#tech-id');
const modebtn = document.querySelector('#mode-btn');
const submit = document.querySelector('#confirm-btn');

let isDarkMode = localStorage.getItem('mode') === 'true'? true:false;
let techInfo = '';

function defautValue(){techId.textContent = generateUID();}; defautValue();


techId.addEventListener('click',()=>{techId.textContent = generateUID();});
inputType.addEventListener('click',()=>{
    inputValue(inputType,document.querySelector('#tech-type'),"Computer",inputType.value);});
inputName.addEventListener("input", ()=> {
    inputValue(inputName,document.querySelector('#tech-name'),"Старая техника",inputName.value);});
inputDate.addEventListener("input", ()=> {
    inputValue(inputDate,document.querySelector('#tech-date'),"1700",inputDate.value);});
inputDescription.addEventListener("input",()=> {
    inputValue(inputDescription,document.querySelector('#tech-description'),"Очень при-очень длинное описание старой техники",inputDescription.value);});
inputFact[0].addEventListener('input',()=>{
    inputValue(inputFact,document.querySelector('#tech-fact'),"Some Interesting fact",inputFact.value);});


function inputValue(input,liElement,defaultText, value,){
    if (input.value.length == 0) {
        liElement.textContent = defaultText;
    }else{liElement.textContent = value;}
}


window.onload = function() {
    changeMode(isDarkMode);
    if(sessionStorage.getItem('editData')){
        fromEditData(checkEditFile());
    }
    changeBtns();
};

function checkEditFile(){
    const tempData = sessionStorage.getItem('editData');
    techInfo = JSON.parse(tempData);
    return techInfo;
}

function fromEditData(techInfo){ 
    techId.textContent = techInfo.id;
    inputName.value = techInfo.name;
    inputDate.value = techInfo.year;
    inputDescription.value = techInfo.description;
    inputType.value = techInfo.type;
    
    tempDivTextContent('#tech-name',inputName.value);
    tempDivTextContent('#tech-date',inputDate.value);
    tempDivTextContent('#tech-description',inputDescription.value);
    tempDivTextContent('#tech-type',inputType.value);
    tempDivTextContent('#tech-fact',techInfo.interestingfacts[0]);
    
    rewriteDiv('.add-img', `<h1>Tech Images</h1>`);
    for(let i = 0; i< techInfo.images.length; i++){
        document.querySelector('.add-img').innerHTML +=
        `<div id="add-img-box" class="add-box" >
        <input type="text" id="input-img" value="${techInfo.images[i]}" placeholder="Введите URL">
        </div>`;
        
        document.querySelectorAll('.tech-img')[i].src = techInfo.images[i];
    }
    
    
    rewriteDiv('.add-fact-div', `<h1>Interesting Facts</h1>`);
    for(let i = 0; i< techInfo.interestingfacts.length; i++){
        document.querySelector('.add-fact-div').innerHTML +=
        `<div id="fact-div"  class="add-box">
            <input type="text" value="${techInfo.interestingfacts[i]}" id="input-fact">
        </div>`;
    }


    let techChar = document.querySelector('.all-tech-char-div');
    techChar.innerHTML ='';
    for(let i = 0; i< techInfo.charname.length; i++){
        techChar.innerHTML +=
        `<div class="characteristic-input">
        <input type="text" value="${techInfo.charname[i]}" id="input-tech-char-name" placeholder="Название характеристики:">
            <input type="text" value="${techInfo.charvalue[i]}" id="input-tech-char-value" placeholder="Значение характеристики:">   
        </div>`;
    }

}
document.querySelector('.add-img').addEventListener('click',(event)=>{
    const target = event.target;
    if (target.id === 'input-img'){
        document.querySelectorAll('#input-img').forEach((imgInput,index) => {
            imgInput.addEventListener('input', (event) => {
                const inputImgValue = event.target.value;
                updateImageAtIndex(inputImgValue, index);
            });
        });
    }
});
document.querySelector('.add-fact-div').addEventListener('click',(event)=>{
    const target = event.target;

    if(event.target.id ==='input-fact'){
        const input = target.closest('#input-fact'); 
        input.addEventListener('input',()=>{
            inputValue(input,document.querySelector('#tech-fact'),"Some Interesting fact",input.value);
        });
    }
});

function updateImageAtIndex(newImageUrl, index) {
    
    const defaultImgPath = 'https://avatars.dzeninfra.ru/get-zen_doc/3137181/pub_622c93eaa228967ff2d727e7_622c94198ae1c12db1140183/scale_1200';
    const allCaruselImg = document.querySelectorAll(".tech-img");
    if (allCaruselImg[index]) {
        if (allCaruselImg[index].src != newImageUrl) {

            if(newImageUrl != ""){
                allCaruselImg[index].src = newImageUrl;

             }else{
                allCaruselImg[index].src = defaultImgPath;
            }
            allCaruselImg[index].alt = 'GFG';
        } 
    }
}
function rewriteDiv(divClass,divH1){
    let mainDiv = document.querySelector(`${divClass}`);
    mainDiv.innerHTML = '';
    mainDiv.innerHTML += divH1;
}
modebtn.addEventListener('click',()=>{
    isDarkMode = btnClick(isDarkMode);
    changeMode(isDarkMode);
});

function changeMode(isDarkMode){
    changeDefaultElements(isDarkMode,'#mode-btn');
    if(isDarkMode === true){
        changeArrBackground('input,select,textarea','lightblue');
    }else{
        changeArrBackground('input,select,textarea','linear-gradient(100deg, #f2d9ff,#ff89df)');
    }
}


submit.addEventListener('click', () => {
    let retroTech = {
        id :generateUID(),
        name: "",
        year: "",
        description: "",
        type: "",
        images: [],
        interestingfacts: [],
        charname: [],
        charvalue: [],
    };


    const allcharName = document.querySelectorAll('#input-tech-char-name');
    const allcharValue = document.querySelectorAll('#input-tech-char-value');
    inputFact = document.querySelectorAll("#input-fact");
    inputImg = document.querySelectorAll('#input-img');

    const allInputs = [inputName, inputDate,inputDescription, inputType,...inputImg, ...inputFact,...allcharName,...allcharValue,];
    let isEmpty = false;

    for(let i= 0,inputCount = 1; i< allInputs.length; i++,inputCount++)
    {
        let inputField = allInputs[i].id;
        if(!isNullOrEmpty(allInputs[i])){
            if(inputField.includes('fact') || inputField.includes('img') || inputField.includes('char-name') ||inputField.includes('char-value')) {
                let prevField = inputField;
                while(prevField === inputField){
                    
                    retroTech[Object.keys(retroTech)[inputCount]].push(allInputs[i].value);
                    i++;
                    
                    if(i < allInputs.length)
                    {
                         inputField = allInputs[i].id;
                    }
                    else{break; }
                }
                i--;
            } else{
                retroTech[Object.keys(retroTech)[inputCount]] = allInputs[i].value;
            }
        } else{
            isEmpty = true;
        }
    }

    if(isEmpty === false && sessionStorage.getItem('editData') != null){
        retroTech.id = techInfo.id;
        put('technologies',retroTech.id,retroTech) .then(result => {
            if(result){
                apiStateChange('green','Tech is successfully changed');
            }else{
                apiStateChange('red','Something went wrong');
            }
        })
        
    }
    else if(isEmpty === false && sessionStorage.getItem('editData') === null){
        post('technologies',retroTech).then(result=>{
            if(result.success){
                apiStateChange('green','Tech is successfully saved');
            }else{
                apiStateChange('red','Something went wrong');
            }
        });

    }else{
        apiStateChange('red','Fill the empty place please');
    }
  
});

function apiStateChange(color,text){
    let apiState = document.querySelector('.api-state')
    apiState.style.color = color;
    apiState.textContent = text;
}
function isNullOrEmpty(input) {
    return !input || input.value.trim() === '';
}
function generateUID() {
    var firstPart = (Math.random() * 46656) | 0;
    var secondPart = (Math.random() * 46656) | 0;
    return firstPart + secondPart;
}

function tempDivTextContent(textContentId,value){
    document.querySelector(textContentId).textContent = value;
}