import React, {useState} from 'react';
import {Link} from "react-router-dom";
import FavoriteWaresModal from "./FavoriteWaresModal";
import CartModal from "./CartModal";

const Header = () => {
    const [showFavorite, setShowFavorite] = useState(false)
    const [showCart, setShowCart] = useState(false)

    return (
        <>
            <nav className="navbar navbar-dark navbar-expand-xl bg-black">
                <div className="container">
                    <Link to={"/"} className={"navbar-brand"}>WEAR</Link>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse"
                            data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false"
                            aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarNav">
                        <ul className="navbar-nav">
                            <li className="nav-item">
                                <Link to={"/Discounts"} className={"nav-link active link-danger"}>Discounts</Link>
                            </li>
                            <li className="nav-item">
                                <Link to={"/Novelty"} className={"nav-link active link-success"}>Novelty</Link>
                            </li>
                            <li className="nav-item">
                                <Link to={"/Wear-basic"} className={"nav-link active"}>Wear Basic</Link>
                            </li>
                            <li className="nav-item">
                                <Link to={"/Wear-tactical"} className={"nav-link active"}>Wear Tactical</Link>
                            </li>
                            <li className="nav-item">
                                <Link to={"/Wear"} className={"nav-link active"}>Wear</Link>
                            </li>
                            <li className="nav-item">
                                <Link to={"/Footwear"} className={"nav-link active"}>Footwear</Link>
                            </li>
                            <li className="nav-item">
                                <Link to={"/Backpacks-and-bags"} className={"nav-link active"}>Backpacks and bags</Link>
                            </li>
                            <li className="nav-item">
                                <Link to={"/Accessories"} className={"nav-link active"}>Accessories</Link>
                            </li>
                        </ul>
                        <div className="position-absolute end-0 me-4">
                            <button onClick={e => setShowCart(true)} className="btn p-2">
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="white"
                                     className="bi bi-cart mb-1" viewBox="0 0 16 16">
                                    <path
                                        d="M0 1.5A.5.5 0 0 1 .5 1H2a.5.5 0 0 1 .485.379L2.89 3H14.5a.5.5 0 0 1 .491.592l-1.5 8A.5.5 0 0 1 13 12H4a.5.5 0 0 1-.491-.408L2.01 3.607 1.61 2H.5a.5.5 0 0 1-.5-.5zM3.102 4l1.313 7h8.17l1.313-7H3.102zM5 12a2 2 0 1 0 0 4 2 2 0 0 0 0-4zm7 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4zm-7 1a1 1 0 1 1 0 2 1 1 0 0 1 0-2zm7 0a1 1 0 1 1 0 2 1 1 0 0 1 0-2z"/>
                                </svg>
                            </button>
                            <button onClick={e => setShowFavorite(true)} className="btn p-2">
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="white"
                                     className="bi bi-heart" viewBox="0 0 16 16">
                                    <path
                                        d="m8 2.748-.717-.737C5.6.281 2.514.878 1.4 3.053c-.523 1.023-.641 2.5.314 4.385.92 1.815 2.834 3.989 6.286 6.357 3.452-2.368 5.365-4.542 6.286-6.357.955-1.886.838-3.362.314-4.385C13.486.878 10.4.28 8.717 2.01L8 2.748zM8 15C-7.333 4.868 3.279-3.04 7.824 1.143c.06.055.119.112.176.171a3.12 3.12 0 0 1 .176-.17C12.72-3.042 23.333 4.867 8 15z"/>
                                </svg>
                            </button>
                            <Link to={"/Cabinet"} className="btn p-2">
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="white"
                                     className="bi bi-person mb-1" viewBox="0 0 16 16">
                                    <path
                                        d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm2-3a2 2 0 1 1-4 0 2 2 0 0 1 4 0zm4 8c0 1-1 1-1 1H3s-1 0-1-1 1-4 6-4 6 3 6 4zm-1-.004c-.001-.246-.154-.986-.832-1.664C11.516 10.68 10.289 10 8 10c-2.29 0-3.516.68-4.168 1.332-.678.678-.83 1.418-.832 1.664h10z"/>
                                </svg>
                            </Link>
                        </div>
                    </div>
                </div>
            </nav>
            <FavoriteWaresModal show={showFavorite} closeHandler={() => setShowFavorite(!showFavorite)} />
            <CartModal show={showCart} closeHandler={() => setShowCart(!showCart)} />
        </>
    );
};

export default Header;