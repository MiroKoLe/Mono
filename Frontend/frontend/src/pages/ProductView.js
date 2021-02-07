import * as React from 'react';
import { observer, inject } from 'mobx-react';
import {Link} from 'react-router-dom'; 
import ProductService from '../common/ProductService'
import Button from '@material-ui/core/Button'; 
import DeleteIcon from '@material-ui/icons/Delete'; 
import EditOutlinedIcon from '@material-ui/icons/EditOutlined';
import ArrowBackOutlinedIcon from '@material-ui/icons/ArrowBackOutlined';

 class ProductView extends React.Component {

    constructor(){
        super(); 
        this.productService = new ProductService(); 
    }

    componentDidMount(){
        document.title = "Products"
        this.props.ProductStore.getProductsAsync(); 
    }

    updateAfterDelete(){
        this.props.ProductStore.getProductsAsync(); 
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
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.ProductStore.productData.map (product =>(
                            <tr key = {product.Id}>
                                <td>{product.Name}</td>
                                <td>{product.Model}</td>
                                <td>{product.Quantity}</td>
                                <p/>
                                <Link to={`/products/edit/${product.Id}`}><Button startIcon = {<EditOutlinedIcon/>} size = "small" variant = "contained" color = "primary">Edit</Button></Link> 
                                <Button startIcon = {<DeleteIcon/>} size = "small" variant ="contained" color = "secondary" type="button" className="btn btn-danger" onClick = {() => {if(window.confirm('Are you sure you wish to delete this item?'))this.props.ProductStore.deleteProductAsync(product.Id)}}>Delete</Button>
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
export default inject("ProductStore")(observer(ProductView)); 
