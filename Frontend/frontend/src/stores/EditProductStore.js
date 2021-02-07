import { makeAutoObservable, runInAction } from 'mobx';
import ProductService from '../common/ProductService'

 class EditStore{
    
    editData = {}; 

    constructor(){

        this.productService = new ProductService(); 
        makeAutoObservable(this)
    }

  
    updateProductAsync = async () => {
        try{
            await this.productService.put(this.editData, this.editData.Id)           

        } catch (error) {
            console.error("Error", error)
            
            };

        
            
       }

       setNameUpdate = event => {
           this.editData.Name = event.target.value
       }
       setModelUpdate = event => {
        this.editData.Model = event.target.value
       }
       setQuantityUpdate = event => {
        this.editData.Quantity = event.target.value
       }
       setProductCategoryIdUpdate = event => {
        this.editData.ProductCategoryId = event.target.value
       }

        getProductAsync = async (Id) => {
        const getData = await this.productService.getProductById(Id);
        runInAction(() => {
            this.editData = getData; 
        })

        
    }; 

    editProduct = (e) =>{ 
        e.preventDefault(); 
        this.updateProductAsync({   
            Name: this.editData.Name, 
            Model: this.editData.Model, 
            Quantity: this.editData.Quantity, 
            ProductCategoryId: this.editData.ProductCategoryId
        }); 



    }
 
}

export default new EditStore(); 
