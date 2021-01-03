import React from 'react'; 
import {Link} from 'react-router-dom'; 
import './App.css';
import { GitHub, Linkedin, Facebook  } from 'react-feather';

const Home =() => {

    


    return(

        <nav>
            <h3 style = {{color:'whitesmoke'}}>Product Management Portal</h3>
            <ul className="nav-links">
                
                <Link to="/Products">
                <li style = {{color:'whitesmoke'}}>Products</li>
                </Link>
                <Link to="/Products/Create">
                <li style = {{color:'whitesmoke'}}>Create</li>
                </Link>
                <a href = "https://github.com/MiroKoLe" target = "_blank">
                    <GitHub style = {{color:'whitesmoke'}} size = "30" className = "social-links"/>
                </a>
                <a href = "https://www.linkedin.com/in/miroslav-kolev-04aa0193/" target = "_blank">
                    <Linkedin style = {{color:'whitesmoke'}} size = "30" className = "social-links"/>
                </a>
                <a href = "https://www.facebook.com/miroslav.kolev1/" target = "_blank">
                    <Facebook style = {{color:'whitesmoke'}} size = "30" className = "social-links"/>
                </a>
            </ul>
        </nav>
    )
    
}
export default Home; 