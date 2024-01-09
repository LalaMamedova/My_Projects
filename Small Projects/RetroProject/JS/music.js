const audio= document.querySelector(".audio");
const playButton= document.querySelector(".play-music");
const pauseButton= document.querySelector(".stop-music");
const nextButton= document.querySelector(".next-music");
const prevButton= document.querySelector(".prev-music");
let musicState = false;
let index = 0;
const songs = [
    "Messages from the Stars.mp3",
    "Careless Whisper.mp3", 
    "Sweet Dreams.mp3",
    'Stranger Things.mp3',
    "Lady of the 80's.mp3"
]



playButton.addEventListener("click", () => {
    if(musicState === false){
        audio.play(); 
        musicState = true;
    }else{
        audio.pause();
        musicState = false;
    }
});


nextButton.addEventListener("click", () => {
    nextButton.querySelector('i').className +=' fa-spin'
    ;
    if (index < songs.length-1) {
        index++;
    } else {
        index = 0;
    }
    audio.setAttribute("src", "/Assets/Media/" + songs[index]);
    audio.play(); 

    setTimeout(()=>{
        nextButton.querySelector('i').className ='fa-solid fa-forward-step';
    },2000);
});

prevButton.addEventListener("click", () => {
    prevButton.querySelector('i').className +=' fa-spin';

    if (index < songs.length-1 && index!=0) {
        index--;
    }else{
        index = songs.length-1;
    }
    audio.setAttribute("src", "/Assets/Media/" + songs[index]);
    audio.play(); 

    setTimeout(()=>{
        prevButton.querySelector('i').className ='fa-solid fa-backward-step';
    },2000);
    
});

