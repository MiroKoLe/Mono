import * as React from 'react';
import { observer, inject } from 'mobx-react';
import Button from '@material-ui/core/Button'; 
import ArrowBackOutlinedIcon from '@material-ui/icons/ArrowBackOutlined';
import {Link} from 'react-router-dom'; 
import SaveIcon from '@material-ui/icons/Save'


 class EditView extends React.Component {

    componentDidMount(){
        this.props.EditStore.getProductAsync(this.props.match.params.Id); 
        console.log(this.props)
    } 


    render() {

        return (
        
            <div>
                <form onSubmit = {this.props.EditStore.updateProductAsync} ref = "form">
            
                
                    <div className="form-group">
                        <input onChange={this.props.EditStore.setNameUpdate} value ={this.props.EditStore.editData.Name } id="Name" type="text" />
                    </div>
                    <div className="form-group">
                        <input onChange={this.props.EditStore.setModelUpdate} value ={this.props.EditStore.editData.Model }  id="Model" type="text" />
                    </div>
                    <div className="form-group">    
                        <input onChange={this.props.EditStore.setQuantityUpdate} value ={this.props.EditStore.editData.Quantity } id="Quantity" type="number" />
                    </div>
                    <div className="form-group">
                        <input onChange={this.props.EditStore.setProductCategoryIdUpdate} value ={this.props.EditStore.editData.ProductCategoryId} id="ProductCategoryId" type="number" />
                    </div>
                    <Button 
                        startIcon = {<SaveIcon />} 
                        type="submit" 
                        variant ="contained" 
                        color ="secondary"
                        size = "small"
                        >Edit</Button>
                        <p/>
                        <Link to = {'/products'}><Button startIcon = {<ArrowBackOutlinedIcon/>} size = "small" variant = "contained" color = "primary" href="#contained-buttons">Back</Button></Link>

                        </form>

            </div>
            
           
        )
    }
}

export default inject("EditStore")(observer(EditView));
