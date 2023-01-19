import React, {useEffect, useState} from 'react';
import {
    CaretDown,
    CaretUp, EmojiExpressionless,
    Google, Heart, Heartbreak,
    HeartFill,
    Mailbox,
    Person,
    PersonBadgeFill,
    PlusCircle
} from "react-bootstrap-icons";
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {sign_out} from "../../redux/action_creators/authorization_action_creator";
import {Button, ButtonGroup} from "react-bootstrap";
import {
    delete_ware,
    fetch_all_wares,
    fetch_discount_wares,
    fetch_discount_wares_authorized, fetch_favorite_wares, get_orders, remove_from_favorite
} from "../../redux/action_creators/ware_action_creator";
import {Link} from "react-router-dom";
import CabinetAccount from "./CabinetAccount";
import CabinetFavorite from "./CabinetFavorite";
import CabinetWares from "./CabinetWares";
import SignOutModal from "./SignOutModal";
import CabinetOrders from "./CabinetOrders";
import Loading from "../Layout/Loading";
import CabinetUsers from "./CabinetUsers";
import CabinetCategories from "./CabinetCategories";

const Cabinet = () => {
    const user = useAppSelector(state => state.authorizationReducer.user)
    const allWares = useAppSelector(state => state.wareReducer.allWares.wares)
    const waresLoading = useAppSelector(state => state.wareReducer.allWares.isLoading)
    const isLoading = useAppSelector(state => state.wareReducer)
    const favoriteWares = useAppSelector(state => state.wareReducer.favoriteWares)
    const [showSignOutModal, setShowSignOutModal] = useState(false)

    const dispatch = useAppDispatch()
    const [currentPage, setCurrentPage] = useState('account')
    const signOutHandler = () => {
        setShowSignOutModal(true)
    }

    useEffect(() => {
        if (currentPage === 'wares')
            dispatch(fetch_all_wares())
        // if (currentPage === 'favorites')
        //     dispatch(fetch_favorite_wares())
        if (currentPage === 'orders')
            dispatch(get_orders())
    }, [currentPage])
    document.title = "Account"
    return (
        <>
            <div className={"container"}>
                <h2 className={"text-center display-6 mt-5"}>Personal Account</h2>
                <p onClick={signOutHandler} style={{cursor: 'pointer'}}
                   className={"text-decoration-underline text-center"}>Sign Out</p>
                <SignOutModal show={showSignOutModal} closeHandler={() => {setShowSignOutModal(false)}}/>
            </div>
            <div className={"container"}>
                <div className={'text-center'}>
                    <div className={'row'}>
                        <Button onClick={() => setCurrentPage('account')}
                                className={`hover-white col ${currentPage === 'account' ? 'opacity-75' : 'opacity-100'}`}
                                variant="primary">Account</Button>
                        {user?.permissions?.includes(1) ? <>
                            <Button onClick={() => setCurrentPage('wares')}
                                    className={`hover-white col ${currentPage === 'wares' ? 'opacity-75' : 'opacity-100'}`}
                                    variant="primary">Wares</Button>
                        </> : <></>}
                        {user?.permissions?.includes(1) ? <>
                            <Button onClick={() => setCurrentPage('orders')}
                                    className={`hover-white col ${currentPage === 'orders' ? 'opacity-75' : 'opacity-100'}`}
                                    variant="primary">Orders</Button>
                        </> : <></>}
                        {user?.permissions?.includes(1) ? <>
                            <Button onClick={() => setCurrentPage('categories')}
                                    className={`hover-white col ${currentPage === 'categories' ? 'opacity-75' : 'opacity-100'}`}
                                    variant="primary">Categories</Button>
                        </> : <></>}
                        {user?.permissions?.includes(2) ? <>
                            <Button onClick={() => setCurrentPage('users')}
                                    className={`hover-white col ${currentPage === 'users' ? 'opacity-75' : 'opacity-100'}`}
                                    variant="primary">Users</Button>
                        </> : <></>}
                    </div>
                </div>
            </div>
            <div className={`container ${currentPage === 'account' ? 'd-block' : 'd-none'}`}>
                <CabinetAccount user={user}/>
            </div>
            <div className={`container-lg ${currentPage === 'wares' ? 'd-block' : 'd-none'}`}>
                <CabinetWares allWares={allWares}/>
            </div>
            <div className={`container-fluid ${currentPage === 'orders' ? 'd-block' : 'd-none'}`}>
                <CabinetOrders/>
            </div>
            <div className={`container-fluid ${currentPage === 'users' ? 'd-block' : 'd-none'}`}>
                <CabinetUsers/>
            </div>
            <div className={`container-fluid ${currentPage === 'categories' ? 'd-block' : 'd-none'}`}>
                <CabinetCategories/>
            </div>
        </>
    );
};

export default Cabinet;