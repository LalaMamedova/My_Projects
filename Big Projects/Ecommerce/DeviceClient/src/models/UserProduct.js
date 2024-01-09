import { makeAutoObservable,} from "mobx";

export default class Product{

    constructor(){
        this._category =[];
        this._product = [];
        this._subcategory = [];
        this._brand = [];
        this._selectedProduct = {};
        this._selectedCategory = {};
        this._recomendationProducts= [];
        this._totalSum = 1;
        this._currentPage = 1;
        this._totalPages = 1;
        this._totalItems = 1;
        makeAutoObservable(this);
    }
    setCategory(category){
        this._category = category;
    }
    
    setRecomendationProducts(products){
        this._recomendationProducts = products;
    }

    setTotalSum(sum){
        this._totalSum = sum;
    }

    setCurrentPage(page){
        this._currentPage = page;
    }
    
    setTotalItems(item){
        this._totalItems = item;
    }

    setTotalPages(page){
        this._totalPage = page;
    }

    setProduct(product){
        this._product = product;
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
    setSelectedCategory(category){
        this._selectedCategory = category;
    }
   

    get currentPage(){
        return this._currentPage;
    }
    get recomendationProducts(){
        return this._recomendationProducts;
    }
    get totalPages(){
        return this._totalPages;
    }
    get totalItems(){
        return this._totalItems;
    }
    get totalSum(){
        return this._totalSum;
    }
    get category(){
        return this._category;
    }
    get subcategory(){
        return this._subcategory;
    }
    get product(){
        return this._product;
    }
    get selectedProduct(){
        return this._selectedProduct;
    }
   
    get selectedCategory(){
        return this._selectedCategory;
    }
}