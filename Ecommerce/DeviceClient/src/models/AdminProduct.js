import { makeAutoObservable } from "mobx";

export default class AdminProduct{

    constructor(){
        this._category =[];
        this._device = [];
        this._subcategory = [];
        this._brand = [];
        this._selectedProduct = {};
        this._selectedCategory ={};
        this._selectedSubCategory ={};
        this._categoryName = '';
        this._subCategoryName = '';
        this._characteristics = [];
        makeAutoObservable(this);
      
        
    }
    
    setChars(chars){
        this._characteristics = chars;
    }
    setCategory(category){
        this._category = category;
    }
    setDevice(device){
        this._device = device;
    }

    setBrand(brand){
        this._brand = brand;
    }
    
    setSubCategory(subcategory){
        this._subcategory = subcategory;
    }

    setSelectedProduct(product){
        this._selectedProduct = product;
    }
    
    setCategoryName(name){
        this._categoryName  = name;
    }
    setSubCategoryName(name){
        this._subCategoryName  = name;
    }
    setSelectedCategory(category){
        this._selectedCategory  = category;
    }
    setSelectedSubCategory(subcategory){
        this._selectedSubCategory  = subcategory;
    }

    get chars(){
        return this._characteristics;
    }
    get categoryName(){
        return this._categoryName;
    }
    get selectedCategory(){
        return this._selectedCategory;
    }
    get selectedSubCategory(){
        return this._selectedSubCategory;
    }
    get subCategoryName(){
        return this._subCategoryName;
    }
    get category(){
        return this._category;
    }
    get subcategory(){
        return this._subcategory;
    }
    get device(){
        return this._device;
    }
    get brand(){
        return this._brand;
    }
    get selectedProduct(){
        return this._selectedProduct;
    }
}