import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter} from 'react-router-dom';
import {Provider} from 'mobx-react';
import './index.css';
import App from './components/App';
import * as serviceWorker from './serviceWorker';
import ProductTableStore from './components/ProductTableStore'; 
import CreateStore from './components/CreateStore';
import EditStore from './components/EditStore';


const productApp = document.getElementById('productApp')
const Root = (<Provider 
  ProductTableStore = {ProductTableStore}
  CreateStore = {CreateStore}
  EditStore = {EditStore}>
<App />
</Provider>
  )
ReactDOM.render(
  <BrowserRouter>
  {Root}
  </BrowserRouter>, productApp
  
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
