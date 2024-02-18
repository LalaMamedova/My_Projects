import { makeAutoObservable } from "mobx";

export default class User {
    private _quizes: {};
    private _id: string | undefined;
    private _email: string | undefined;
    private _userName: string | undefined;

    constructor() {
        this._userName = undefined;
        this._email = undefined;
        this._id = undefined;
        this._quizes = {};
        makeAutoObservable(this);
    }

    public setData(data: any) {
        
        if (data.userName) {
            this.setUserName(data.userName);
        }
        if (data.email) {
           this.setEmail(data.email)
        }
        if (data.quizes) {
            this.setQuizes(data.quizes);
        }
        if (data.id) {
            this.setId(data.id);
        }

    }


    public get quizes(): {} {
        return this._quizes;
    }

    public setQuizes(value: {}) {
        this._quizes = value;
    }

    public get id():  string | undefined {
        return this._id;
    }
    public setId(value: string | undefined) {
        this._id = value;
    }


    public  get email(): string | undefined {
        
        return this._email;
    }

    public setEmail(value: string | undefined) {
        this._email = value;
    }

    public get userName(): string | undefined {
        return this._userName;
    }

    public setUserName(value: string | undefined) {
        this._userName = value;
    }
}
