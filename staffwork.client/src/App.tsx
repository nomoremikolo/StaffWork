import React, {useEffect, useRef} from 'react';
import {Route, Routes} from "react-router-dom";
import Welcome from "./pages/Welcome/Welcome";
import PageNotFound from "./pages/PageNotFound/PageNotFound";
import Header from "./pages/Layout/Header";
import {useAppDispatch, useAppSelector} from "./hooks/redux";
import Cabinet from "./pages/Account/Cabinet";
import LoginPage from "./pages/SignIn/Login";
import RegistrationPage from "./pages/SignIn/Registration";
import {refresh_token} from "./redux/action_creators/authorization_action_creator";
import Discounts from "./pages/Discounts/Discounts";
import Novelty from "./pages/Novelty/Novelty";
import WarePage from "./pages/WarePage/WarePage";
import NewWare from "./pages/NewWare/NewWare";
import NewCategory from "./pages/NewCategory/NewCategory";
import NewBrand from "./pages/NewBrand/NewBrand";
import EditWare from "./pages/EditWare/EditWare";
import CabinetUserEdit from "./pages/Account/CabinetUserEdit";
import ScrollBtn from "./components/ScrollBtn";
import AllWares from "./pages/AllWares/AllWares";

function App() {
    const dispatch = useAppDispatch()
    const {isAuthorized, user} = useAppSelector(state => state.authorizationReducer)


    useEffect(() => {
        dispatch(refresh_token())
    }, [ ])

    return (
        <>
            <Header/>
            <ScrollBtn/>
            <Routes>
                <Route path={"/"} element={<Welcome/>}/>
                <Route path={"/Account"} element={isAuthorized ? <Cabinet/> : <LoginPage/>}/>
                <Route path={"/Discounts"} element={<Discounts/>}/>
                <Route path={"/All"} element={<AllWares/>}/>
                <Route path={"/Category/:category"} element={<AllWares/>}/>
                <Route path={"/Wares/:id"} element={<WarePage/>}/>
                <Route path={"/Novelty"} element={<Novelty/>}/>
                <Route path={"/SignIn"} element={isAuthorized ? <Cabinet/> : <RegistrationPage/>}/>
                <Route path={"*"} element={<PageNotFound/>}/>
                {!isAuthorized ? <></> : user?.permissions.includes(2) ?
                <>
                    <Route path={"/NewWare"} element={<NewWare/>}/>
                </> : <></>}
                {!isAuthorized ? <></> : user?.permissions.includes(1) ?
                <>
                    <Route path={"/EditWare/:id"} element={<EditWare/>}/>
                </> : <></>}
                {!isAuthorized ? <></> : user?.permissions.includes(1) ?
                <>
                    <Route path={"/NewCategory"} element={<NewCategory/>}/>
                </> : <></>}
                {!isAuthorized ? <></> : user?.permissions.includes(1) ?
                <>
                    <Route path={"/NewBrand"} element={<NewBrand/>}/>
                </> : <></>}
                {!isAuthorized ? <></> : user?.permissions.includes(3) ?
                <>
                    <Route path={"/EditUser/:id"} element={<CabinetUserEdit/>}/>
                </> : <></>}

            </Routes>
        </>
    );
}

export default App;
