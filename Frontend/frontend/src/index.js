import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter} from 'react-router-dom';
import {Provider} from 'mobx-react';
import './index.css';
import App from './App'
import reportWebVitals from './reportWebVitals';
import ProductStore from './stores/ProductStore'
import EditProductStore from './stores/EditProductStore';
import CreateProductStore from './stores/CreateProductStore';
import ProductCategoryStore from './stores/ProductCategoryStore';

const productApp = document.getElementById('productApp')
const Root = (<Provider 
  ProductStore = {ProductStore}
  EditProductStore = {EditProductStore}
  CreateProductStore = {CreateProductStore}
  ProductCategoryStore = {ProductCategoryStore}>
<App />
</Provider>
  )
ReactDOM.render(
  <BrowserRouter>
  {Root}
  </BrowserRouter>, productApp
  
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
