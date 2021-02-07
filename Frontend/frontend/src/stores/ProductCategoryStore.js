import {runInAction, makeAutoObservable } from 'mobx'; 
import ProductCategoryService from '../common/ProductCategoryService'


class ProductCategoryStore {

    categoryData = []; 

    constructor(){
        this.productCategoryService = new ProductCategoryService()
        makeAutoObservable(this)
    }
    status = "initial"; 

    deleteCategoryAsync = async(Id) => {
        await this.productCategoryService.deleteItem(Id)
    }

    getCategoryAsync = async() => {
        try{
            const data = await this.productCategoryService.get(); 
            runInAction(() => {
                this.categoryData = data; 

            })
        }catch {
            runInAction(() => {
                this.status = "error"; 
            })
        }
    };

    
  
}

export default new ProductCategoryStore(); 
