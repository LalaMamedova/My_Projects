import { makeAutoObservable,} from "mobx";

export default class BreadCrumbsPath{

    constructor(){
        this._paths = [{
                        "name": '',
                        "url":'' 
                    }];

        makeAutoObservable(this);
      
    }

  
    get Paths(){
        return this._paths;
    }



    setPaths(path){
        this._paths= path;
    }
    
    pushToPaths(path){
        this._paths.push(path);
    }
}