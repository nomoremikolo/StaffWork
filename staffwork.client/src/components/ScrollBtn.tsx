import React, {useEffect, useRef} from 'react';
import {refresh_token} from "../redux/action_creators/authorization_action_creator";
import ReactDOM from "react-dom";

const ScrollBtn = () => {
    const scrlBtn = useRef<HTMLDivElement>(null)
    useEffect(() => {
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
    return ReactDOM.createPortal (
        <>
            <div ref={scrlBtn} className="btn-up btn-up_hide"></div>
        </>,
        document.getElementById('portal') as HTMLElement
    );
};

export default ScrollBtn;