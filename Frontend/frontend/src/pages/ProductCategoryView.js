import React, { Component } from 'react'
import ProductCategoryService from '../common/ProductCategoryService'
import { observer, inject } from 'mobx-react';
import {Link} from 'react-router-dom'; 
import Button from '@material-ui/core/Button'; 
import DeleteIcon from '@material-ui/icons/Delete'; 
import EditOutlinedIcon from '@material-ui/icons/EditOutlined';
import ArrowBackOutlinedIcon from '@material-ui/icons/ArrowBackOutlined';

export class ProductCategoryView extends Component {

    constructor(){
        super()
        this.productCategoryService = new ProductCategoryService();
        
    }

    componentDidMount(){
        document.title = "ProductCategory List"
        this.props.ProductCategoryStore.getCategoryAsync();        
    }

    render() {
        return (
            <div>
                <table>
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Name</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.ProductCategoryStore.categoryData.map(category =>(
                            <tr key = {category.ProductId}>
                            <td>{category.Name}</td>
                            <p/>
                            <Link to={`/productcategory/edit/${category.ProductId}`}><Button startIcon = {<EditOutlinedIcon/>} size = "small" variant = "contained" color = "primary">Edit</Button></Link> 
                                   <Button startIcon = {<DeleteIcon/>} size = "small" variant ="contained" color = "secondary" type="button" className="btn btn-danger" onClick = {() => {if(window.confirm('Are you sure you wish to delete this item?'))this.props.ProductCategoryStore.deleteCategoryAsync(category.Id)}}>Delete</Button>
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
   export default inject("ProductCategoryStore")(observer(ProductCategoryView)); 