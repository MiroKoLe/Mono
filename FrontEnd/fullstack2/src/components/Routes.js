import * as React from 'react'; 
import {Route, Switch} from 'react-router-dom'; 
import ProductTableView from './ProductTableView';
import Home from './Home';  
import CreateView from './CreateView';
import EditView from './EditView';




export default class Routes extends React.Component {
    render(){
        return (
            <div>
                <Switch>
                <Route path="/" exact component = {Home}/>
                <Route path="/Products" exact component = {ProductTableView}/>
                <Route path ="/Products/Create" exact component = {CreateView}/>
                <Route path ="/Products/Edit/:Id" exact component = {EditView}/>
                </Switch>
                
           
             
            </div>
        )
    }
}