import * as React from 'react';

const webApiUrl = "/productapi"; 

class ProductService extends React.Component {
    
    get = async () => {
        const options = {
            method: "GET",
        }
        const request = new Request(webApiUrl, options); 
        const response = await fetch(request); 
        return response.json(); 
    }


    post = async (model) => {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");
        var options = {
            method: "POST", 
            headers, 
            body: JSON.stringify(model)
        }
        const request = new Request(webApiUrl, options); 
        const response = await fetch(request); 
        return response;  
      
    }
    put = async (editData, Id) => {
        const headers = new Headers()
        headers.append("Content-Type", "application/json");
        var options = {
            method: "PUT",
            headers,
            body: JSON.stringify(editData)
        }
        const request = new Request(webApiUrl + "/" + Id, options);
        const response = await fetch(request);
        return response; 
        
    
    }
     

    getProductById = async (Id) => {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");
        const options = {
            method: "GET",
            headers
    }
        const request = new Request(webApiUrl + "/" + Id, options);
        const response = await fetch(request);
        return response.json(); 
}
    
    deleteItem = async (Id) => {
        const headers = new Headers(); 
        headers.append("Content-Type", "application/json"); 
        const options = {
            method: "DELETE",
            headers
        }
        const request = new Request (webApiUrl + "/" + Id, options); 
        const response = await fetch(request)
        return response; 
    }



}
export default ProductService; 