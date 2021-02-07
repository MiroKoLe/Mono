import { makeAutoObservable, runInAction} from 'mobx'; 
import ProductService from '../common/ProductService';


 
class CreateProductStore{
    constructor(){
        this.productService = new ProductService(); 
        this.redirect = false;
        this.status = "initial";

        makeAutoObservable(this)
    }

    makeRedirect = () => {
        this.redirect = true; 
    }

    setName = element => {
        this.nameInput = element;
      };
    setModel = element => {
        this.modelInput = element; 
    };
    setQuantity = element =>{
        this.quantityInput = element; 
    } 
    setProductCategoryId = element =>{
        this.productCategoryIdInput = element
    }

    createProduct = (e) =>{ 
 
        e.preventDefault(); 
        this.createProductAsync({         
            Name: this.nameInput.value,
            Model: this.modelInput.value, 
            Quantity: this.quantityInput.value, 
            ProductCategoryId: this.productCategoryIdInput.value
        }); 

    
    
        this.nameInput.value = ""; 
        this.modelInput.value = ""; 
        this.quantityInput.value = ""; 
        this.productCategoryIdInput.value = "";

    }
    
           
    createProductAsync = async (model) =>{
    try{

     const response = await this.productService.post(model); 

     if(response.status === 200){
        this.makeRedirect();
        this.response = false;
     } else {
         runInAction(() => {
             this.status = "error";
         })
     }
    }
    catch (error) {
        console.error("Error", error)
        
        };
    }
     

}


export default new CreateProductStore(); 