import * as React from 'react';
import { observer, inject } from 'mobx-react';
import {Redirect, Link} from 'react-router-dom'; 
import Button from '@material-ui/core/Button'
import SaveIcon from '@material-ui/icons/Save'
import ArrowBackOutlinedIcon from '@material-ui/icons/ArrowBackOutlined';


class CreateView extends React.Component{



    render() {

        document.title = "Create"


        const redirect = this.props.CreateStore.redirect; 
        if(redirect){
            return <Redirect to="/Products"/>
        }


        const makeError = this.props.CreateStore.status; 
        if(makeError === "error"){
            
            setTimeout(() => {
                this.props.history.push('/')
            }, 2000)
            console.log("timer is working...")
            return <h1>An Error Occurred</h1>
            
        }
        

        return (

            <div>
                <p/>
                
                    <form onSubmit={this.props.CreateStore.createProduct}>
                        
                    <div className="form-group">
                        <input ref={this.props.CreateStore.setName} form="Name" id="Name" type="text" placeholder="Name"/>
                    </div>
                    <div className="form-group">
                        <input ref={this.props.CreateStore.setModel} form="Model" id="Model" type="text" placeholder="Model"/>
                    </div>
                    <div className="form-group">    
                        <input ref={this.props.CreateStore.setQuantity}form="Quantity" id="Quantity" type="number" placeholder="Quantity"/>
                    </div>
                    <div className="form-group">
                        <input ref={this.props.CreateStore.setProductCategoryId}form="ProductCategoryId" id="ProductCategoryId" type="number" placeholder="ProductCategory"/>
                    </div>
                        <Button 
                        startIcon = {<SaveIcon />} 
                        type="submit" 
                        variant ="contained" 
                        color ="secondary"
                        size = "small"
                        >Save</Button>
                    </form>
                    <p/>    
                    <Link to = "/products"><Button startIcon = {<ArrowBackOutlinedIcon/>} variant ="contained" color ="primary" size = "small" shadowSize = {2}>Back</Button></Link>


            </div>

             )
        }

        

    
}

export default inject ("CreateStore")(observer(CreateView)); 