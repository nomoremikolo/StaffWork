import React from 'react';
import {HeartSpinner} from "react-spinners-kit";
import ReactDOM from "react-dom";

const Loading = () => {
    return ReactDOM.createPortal(
        <div className={'min-vh-100 min-vw-100 position-fixed top-50 start-50 translate-middle bg-dark bg-opacity-50 '}>
            <div className={'position-absolute top-50 start-50 translate-middle'}>
                <HeartSpinner size={100} color="#0dcaf0" loading={true}/>
            </div>
        </div>,
        document.getElementById('portal') as HTMLElement
    );
};

export default Loading;