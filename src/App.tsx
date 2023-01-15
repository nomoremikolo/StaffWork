import React, {useEffect, useRef} from 'react';
import {Route, Routes} from "react-router-dom";
import Welcome from "./pages/Welcome/Welcome";
import PageNotFound from "./pages/PageNotFound/PageNotFound";
import Header from "./pages/Layout/Header";
import {useAppDispatch, useAppSelector} from "./hooks/redux";
import Cabinet from "./pages/Cabinet/Cabinet";
import LoginPage from "./pages/SignIn/Login";
import RegistrationPage from "./pages/SignIn/Registration";
import {refresh_token} from "./redux/action_creators/authorization_action_creator";
import Discounts from "./pages/Discounts/Discounts";
import Novelty from "./pages/Novelty/Novelty";
import WarePage from "./pages/WarePage/WarePage";
import NewWare from "./pages/NewWare/NewWare";
import NewCategory from "./pages/NewCategory/NewCategory";
import NewBrand from "./pages/NewBrand/NewBrand";

function App() {
    const dispatch = useAppDispatch()
    const {isAuthorized, user} = useAppSelector(state => state.authorizationReducer)
    const scrlBtn = useRef<HTMLDivElement>(null)

    useEffect(() => {
        dispatch(refresh_token())
        window.addEventListener('scroll', () => {
            const scrollY = window.scrollY || document.documentElement.scrollTop;
            scrollY > 400 ? showScrlHandler() : hideScrlHandler()
        });
        scrlBtn.current!.addEventListener('click', () => {
            window.scrollTo({
                top: 0,
                left: 0,
                behavior: 'smooth'
            });
        })
    }, [ ])

    const showScrlHandler = () => {
        scrlBtn.current!.classList.remove("btn-up_hide")
    }
    const hideScrlHandler = () => {
        scrlBtn.current!.classList.add("btn-up_hide")
    }
    return (
        <>
            <Header/>
            <div ref={scrlBtn} className="btn-up btn-up_hide"></div>
            <Routes>
                <Route path={"/"} element={<Welcome/>}/>
                <Route path={"/Cabinet"} element={isAuthorized ? <Cabinet/> : <LoginPage/>}/>
                <Route path={"/Discounts"} element={<Discounts/>}/>
                <Route path={"/Wares/:id"} element={<WarePage/>}/>
                <Route path={"/Novelty"} element={<Novelty/>}/>
                <Route path={"/SignIn"} element={isAuthorized ? <Cabinet/> : <RegistrationPage/>}/>
                <Route path={"*"} element={<PageNotFound/>}/>
                {!isAuthorized ? <></> : user?.permissions.includes(2) ?
                <>
                    <Route path={"/NewWare"} element={<NewWare/>}/>
                </> : <></>}
                {!isAuthorized ? <></> : user?.permissions.includes(2) ?
                <>
                    <Route path={"/NewCategory"} element={<NewCategory/>}/>
                </> : <></>}
                {!isAuthorized ? <></> : user?.permissions.includes(2) ?
                <>
                    <Route path={"/NewBrand"} element={<NewBrand/>}/>
                </> : <></>}

            </Routes>
        </>
    );
}

export default App;
