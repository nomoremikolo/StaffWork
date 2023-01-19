import React, {FC, useEffect} from 'react';
import {Modal} from "react-bootstrap";
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {Heart, Heartbreak, HeartFill} from "react-bootstrap-icons";
import ReactDOM from 'react-dom'
import {
    fetch_all_wares_with_favorites,
    fetch_discount_wares_authorized,
    fetch_favorite_wares,
    remove_from_favorite
} from "../../redux/action_creators/ware_action_creator";
import {Link} from "react-router-dom";
import {ImagesEndpoint} from "../../global_variables";

interface IFavoriteWares {
    show: boolean,
    closeHandler: () => void,
}

const FavoriteWaresModal: FC<IFavoriteWares> = ({show, closeHandler}) => {
    const dispatch = useAppDispatch()
    const {isAuthorized} = useAppSelector(state => state.authorizationReducer)
    const {favoriteWares} = useAppSelector(state => state.wareReducer)
    useEffect(() => {
        dispatch(fetch_favorite_wares())
    }, [])
    useEffect(() => {
        dispatch(fetch_favorite_wares())
    }, [show])
    if (!show) return null
    return ReactDOM.createPortal(
        <Modal className={'modal-md'} show={show} onHide={closeHandler}>
            <Modal.Header closeButton>
                <Modal.Title>Favorite wares</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {!isAuthorized ?
                    <h3 className={'text-center my-4'}>Ви не авторизовані <Heartbreak className={'mb-1'}/></h3> :
                    <>
                        {favoriteWares.length > 0 ?
                            favoriteWares.map(item => (
                                    <>
                                        <div className="card mx-auto mb-3">
                                            <button className="btn position-absolute bottom-0 end-0" onClick={() => dispatch(remove_from_favorite(item.wareId!,() => {
                                                dispatch(fetch_favorite_wares())
                                                dispatch(fetch_discount_wares_authorized())
                                            }))}><HeartFill height={20} width={20}/></button>
                                            <img style={{"width": "100%",height: 350, "margin": "auto", objectFit: 'cover'}}
                                                 src={`${ImagesEndpoint}/Get/${item.thumbnail}`}
                                                 className="card-img-top pt-2" alt="..."/>
                                            <div className="card-body">
                                                <h5 className="card-title">{item.name}</h5>
                                                <p className="card-text">{item.sizes}</p>
                                                <p className={`card-text ${item.isDiscount ? 'text-danger' : ''}`}>{item.price} грн.</p>
                                                {item.isDiscount ? <p className="card-text">
                                                    <del>{item.oldPrice} грн.</del>
                                                </p> : <></>
                                                }
                                                <Link onClick={closeHandler} to={`/Wares/${item.wareId!}`}
                                                      className="btn btn-primary">More</Link>
                                            </div>
                                        </div>
                                    </>
                                )
                            )
                            : <p>Oops, it's empty</p>}
                    </>
                }

            </Modal.Body>
        </Modal>,
        document.getElementById('portal') as HTMLElement
    );
};

export default FavoriteWaresModal;