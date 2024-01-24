import {changeDefaultElements,btnClick } from "./changeMode.js";
import { changeBtns } from "./templates.js";
import {getByEmail, put } from "./apiMethods.js";

const modebtn = document.querySelector('#mode-btn');
let isDarkMode = localStorage.getItem('mode') === 'true'? true:false;
let techInfo = '';
var userRes = '';

window.onload = async function() {
    if(sessionStorage.getItem('infoData')){

        if(localStorage.getItem('user')!=null){

            let user = JSON.parse(localStorage.getItem('user'));
            userRes = await getByEmail('users',user.email);    
        }
        await changeTemplate(getFullInfoTech());
    }
    await changeMode(isDarkMode);
    changeBtns();
};

modebtn.addEventListener('click',()=>{
    isDarkMode = btnClick(isDarkMode);
    changeMode(isDarkMode);
});

async function changeMode(isDarkMode){
    changeDefaultElements(isDarkMode,'#mode-btn')

    if(isDarkMode === true){
        $('.section').css({'background':'#182945'});
    }else{
        $('.section').css({'background':'linear-gradient(135deg, #8a98ff, #ff78c7)'});
    }
}


function getFullInfoTech(){
    const tempData = sessionStorage.getItem('infoData');
    techInfo = JSON.parse(tempData);
    return techInfo;
}
async function changeTemplate(techInfo){
    let likeBtnType = await addLikeBtn();
    let imgDiv = makeImgDiv(techInfo);
    let charDiv = makeCharLi(techInfo);
    let factDiv = makeFactDiv(techInfo);

    const link = `http://127.0.0.1:5500/Html/InfoPage.html?${techInfo.name}`;
    const msg = encodeURIComponent(`Look at this ${techInfo.name}`);

    document.querySelector('.main-div').innerHTML = 
    `<div class="all-img-div">
                <div id="GFG" class="carousel slide" data-ride="carousel">
                    <div class="carousel-inner">
                        <div class="carousel-item active">
                            <img class="tech-img" src="${techInfo.images[0]}"  alt="${techInfo.name}">
                            <p>1</p>
                        </div>

                        ${imgDiv}

                    </div>
                    <a class="carousel-control-prev" href="#GFG"
                        data-slide="prev">
                        <span class="carousel-control-prev-icon"></span>
                    </a>
                    <a class="carousel-control-next" href="#GFG"
                        data-slide="next">
                        <span class="carousel-control-next-icon" ></span>
                    </a>
                </div>
            </div>

            <div class="mini-info-div">  
                <div class="interactiv-btn-div" >
                   ${likeBtnType};
                    <button id='share-btn' class="interactiv-btn"><i class="fa fa-share-alt" style="font-size:24px"></i></button>
                        <div class="share-btns-container">
                            <a target='_blank' href="https://www.facebook.com/share.php?u=${link}" id="gradient-i" class="fa fa-facebook-square" ></a>
                            <a target='_blank' href="https://www.twitter.com/share?&url=${link}&text=${msg}" id="gradient-i" class="fa fa-twitter-square"></i></a>
                            <a target='_blank' href="http://vk.com/share.php?&url=${link}&title=${msg}&image=${techInfo.images[0]}" id="gradient-i" class="fa fa-vk"></a>
                        </div>
                    </div>  
                <div>
                    <section class='section' id="name-section">
                        <label for="tech-name">Name:</label>
                        <span id="tech-name">${techInfo.name}</span>
                    </section>
                    
                    <section  class='section' id="type-section">
                        <label for="tech-type">Type:</label>
                        <span id="tech-type">${techInfo.type}</span>
                    </section>
                    
                    <section class='section' id="date-section">
                        <label for="tech-date">Release date:</label>
                        <span id="tech-date">${techInfo.year}</span>
                    </section>
                </div>
            </div>

            <div class="other-info-div">
                <section class='section' id="description-section">
                    <label for="tech-description">Description:</label>
                    <span id="tech-description">${techInfo.description}</span>
                </section>
            </div>

            <div class="fact-and-char-div">
                <section class='section' id="char-section">
                    <label for="tech-char-value">Technical characteristics :</label>
                    ${charDiv}
                </section>

                <section class='section' id="fact-section">
                    <label for="tech-fact">Interesting facts:</label>
                    ${factDiv}
                </section>
            </div>
        `

        await likeBtnClick();
        await shareBtnClick();
}

async function shareBtnClick(){
    let shareBtn = document.querySelector('#share-btn')
    shareBtn.addEventListener('click', async function(){
        var shareBtnContainer = document.querySelector(".share-btns-container");

        if (shareBtnContainer.getAttribute('style') === 'display: none;' || shareBtnContainer.getAttribute('style') === null){
            shareBtnContainer.style.display = 'flex';

        }else{
            shareBtnContainer.style.display ='none';
        }


    });
}
async function likeBtnClick(){

    $('#like-btn').on('click', async function(){
        if(userRes != '')
        {
            const techId = techInfo.id;
            if(!userRes.likedTechnology.includes(techId)){
                userRes.likedTechnology.push(techId);
                put('users',userRes.id,userRes);
                localStorage.setItem('user', JSON.stringify(userRes));
                document.querySelector('#like-btn').querySelector('i').className= ('fa fa-heart fa-beat');
            }else{
                let index =  userRes.likedTechnology.indexOf(techId);
                userRes.likedTechnology.splice(index,1);
                put('users',userRes.id,userRes);
                localStorage.setItem('user', JSON.stringify(userRes));
                document.querySelector('#like-btn').querySelector('i').className= ('fa fa-heart-o fa-beat');
            }

        }else{
            alert('Sign in')
        };  
        
    });
}


async function addLikeBtn(){
    if (userRes != '') {
        if (userRes.likedTechnology.includes(techInfo.id)) {
            return `<button id='like-btn' class="interactiv-btn"><i class="fa fa-heart fa-beat" style="font-size:24px" aria-hidden="true"></i> </button>`;
        }
    }
    return `<button id='like-btn' class="interactiv-btn"><i class="fa fa-heart-o fa-beat" style="font-size:24px" aria-hidden="true"></i> </button>`

}
function makeImgDiv(json){
    let div = '';
    if(json.images.length>1){
        for(let i=1;i<json.images.length;i++){
            
            div +=`<div class="carousel-item">
            <img class="tech-img" src="${json.images[i]}"  alt="${json.name}">
            <p>${i+1}</p>
            </div>`
        }
    }
    return div;
    
}
function makeCharLi(json){
    let div = '';
    for(let i=0; i<json.charname.length ; i++){
        div +=` <li> 
                    <span id="tech-char-name">${json.charname[i]}:</span>
                    <span id="tech-char-value">${json.charvalue[i]}</span>
                </li>`
    }
    return div;
}

function makeFactDiv(json){
    let div = '';
    for(let i=0; i<json.interestingfacts.length ;i++){

        div +=`<li> 
            <span id="tech-fact">${i+1}. ${json.interestingfacts[i]}</span>
        </li>`
    }
    return div;

}
