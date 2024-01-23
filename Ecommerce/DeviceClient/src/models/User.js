import {  makeAutoObservable } from 'mobx';

export default class User {
  
  constructor() {
    this._isAuth = false;
    this._isAdmin = false;
    this._userInfo = {};
    this._basket = [];
    this._purchaseProduct = {};
    makeAutoObservable(this);
   }

  setIsAuth(bool) {
    this._isAuth = bool;
  }
  setPurchaseProduct(product) {
    this._purchaseProduct = product;
  }
  setBasket(basket) {
    this._basket = basket;
  }
  pushBasket(product){
    this._basket.push(product);
  }
  setIsAdmin(bool) {
    this._isAdmin = bool;
  }

  setUserInfo(user) {
    this._userInfo = user;
  }

  setUserInfoProducts(products){
    this._userInfo.products = products;
  }
  get isAuth() {
    return this._isAuth;
  }
  
  get purchaseProduct(){
    return this._purchaseProduct;
  }

  get basket(){
    return this._basket;
  }
  get isAdmin() {
    return this._isAdmin;
  }

  get userInfo() {
    return this._userInfo;
  }
}
