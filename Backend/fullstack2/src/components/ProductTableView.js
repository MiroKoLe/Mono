import * as React from 'react';
import { observer, inject } from 'mobx-react';
import {Link} from 'react-router-dom'; 
import ProductService from './ProductService';
import Button from '@material-ui/core/Button'; 
import DeleteIcon from '@material-ui/icons/Delete'; 
import EditOutlinedIcon from '@material-ui/icons/EditOutlined';
import ArrowBackOutlinedIcon from '@material-ui/icons/ArrowBackOutlined';




class ProductTableView extends React.Component {


    constructor(){
        super(); 
        this.productService = new ProductService(); 
    }
   

    componentDidMount(){
        document.title = "Product Table"
        this.props.ProductTableStore.getProductsAsync();        
    }

    updateAfterDelete(){
        this.props.ProductTableStore.getProductsAsync(); 
    }


       render() {
           return (
               <div>
                
                   <table>
                       <thead>
                           <tr>
                               <th>Name</th>
                               <th>Model</th>
                               <th>Quantity</th>  
                               <th textAlign = "right">Action</th> 
                           </tr>
                       </thead>
                       <tbody>
                           {this.props.ProductTableStore.productData.map (product =>(
                               <tr key = {product.ID}>
                                   <td>{product.Name}</td>
                                   <td>{product.Model}</td>
                                   <td>{product.Quantity}</td>
                                   <p/>
                                   <Link to={`/products/edit/${product.ID}`}><Button startIcon = {<EditOutlinedIcon/>} size = "small" variant = "contained" color = "primary">Edit</Button></Link> 
                                   <Button startIcon = {<DeleteIcon/>} size = "small" variant ="contained" color = "secondary" type="button" className="btn btn-danger" onClick = {() => {if(window.confirm('Are you sure you wish to delete this item?'))this.props.ProductTableStore.deleteProductAsync(product.ID)}}>Delete</Button>
                                   {this.updateAfterDelete()}

                               </tr>
                           ))}
                           
                       </tbody>
                       <Link to = {'/'}><Button startIcon = {<ArrowBackOutlinedIcon/>} size = "small" variant = "contained" color = "primary" href="#contained-buttons">Back</Button></Link>

                       
                   </table>
                   
               </div>
           )
       }
   }
   export default inject("ProductTableStore")(observer(ProductTableView)); 