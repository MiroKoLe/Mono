import {runInAction, makeAutoObservable } from 'mobx'; 
import ProductService from './ProductService'



  class ProductTableStore {


    productData = [];  


    constructor(){
        this.productService = new ProductService();
        makeAutoObservable(this)
       }
       status = "initial"



       deleteProductAsync = async (id) => {
           await this.productService.deleteItem(id)
       }

       
    getProductsAsync = async () => {
        try{
                
            const data = await this.productService.get(); 
            runInAction(() =>{
            this.productData = data; 
        })
    }catch (error){
        runInAction(() => {
            this.status = "error"; 
        })

    }
    }; 
    
    
}


export default new ProductTableStore(); 
