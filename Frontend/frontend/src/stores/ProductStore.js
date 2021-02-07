import {runInAction, makeAutoObservable} from 'mobx'; 
import ProductService from '../common/ProductService'


 class ProductStore {

    productData = []; 

    constructor(){
        this.productService = new ProductService()
        makeAutoObservable(this); 
    }
    status = "initial"; 

    getProductsAsync = async() => {
        try{
            const data = await this.productService.get(); 
            runInAction(() => {
                this.productData = data; 
            })
        } catch(error){
            runInAction(() => {
                this.status = "error"; 
            })
        }
    }
    deleteProductAsync = async(Id) => {
        await this.ProductService.deleteItem(Id)
    }



}

export default new ProductStore(); 
