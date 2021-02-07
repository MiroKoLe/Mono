import * as React from 'react'; 
import {Route, Switch} from 'react-router-dom';
import FrontPage from '../pages/FrontPageView'
import ProductView from '../pages/ProductView'; 
import CreateProductView from '../pages/CreateProductView'; 
import EditProductView from '../pages/EditProductView'
import ProductCategoryView from '../pages/ProductCategoryView'

export default class Routes extends React.Component{
    render() {
        return (
            <div>
                <Switch>
                <Route path="/" exact component = {FrontPage}/>
                <Route path="/Products" exact component = {ProductView}/>
                <Route path="/ProductCategory" exact component = {ProductCategoryView}/>
                <Route path ="/Products/Create" exact component = {CreateProductView}/>
                <Route path ="/Products/Edit/:Id" exact component = {EditProductView}/>
                </Switch>
                
            </div>
        )
    }
}


