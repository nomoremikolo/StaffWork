import React from 'react';
import {EmojiDizzy} from "react-bootstrap-icons";
import {Link} from "react-router-dom";

const PageNotFound = () => {
    return (
        <div>
            <h1 className={'display-5 text-center mt-5'}>Page not found!<EmojiDizzy className={'mb-2'} height={48} width={48}/></h1>
            <p className={'text-center'}><Link to={'/'}>Return to main page</Link></p>
        </div>
    );
};

export default PageNotFound;