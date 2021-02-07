import React from 'react'; 
import {Link} from 'react-router-dom'; 
import '../layouts/App.css';
import { GitHub, Linkedin} from 'react-feather';

const FrontPage =() => {




    return(

        <nav>
            <h3 style = {{color:'whitesmoke'}}>Product Management Portal</h3>
            <ul className="nav-links">
                
                <Link to="/Products">
                <li style = {{color:'whitesmoke'}}>Products</li>
                </Link>
                <Link to="/ProductCategory">
                <li style = {{color:'whitesmoke'}}>Categories</li>
                </Link>
                <Link to="/Products/Create">
                <li style = {{color:'whitesmoke'}}>Create Product</li>
                </Link>
                <Link to="/ProductCategory/Create">
                <li style = {{color:'whitesmoke'}}>Create ProductCategory</li>
                </Link>
                <a href = "https://github.com/MiroKoLe" target = "_blank">
                    <GitHub style = {{color:'whitesmoke'}} size = "30" className = "social-links"/>
                </a>
                <a href = "https://www.linkedin.com/in/miroslav-kolev-04aa0193/" target = "_blank">
                    <Linkedin style = {{color:'whitesmoke'}} size = "30" className = "social-links"/>
                </a>
            </ul>
        </nav>
    )
    
}
export default FrontPage; 