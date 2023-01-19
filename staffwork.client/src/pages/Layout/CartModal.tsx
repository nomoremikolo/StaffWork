import React, {FC, useEffect, useState} from 'react';
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import ReactDOM from "react-dom";
import {Button, ButtonGroup, Modal} from "react-bootstrap";
import {Basket, DashCircle, Heartbreak, PlusCircle, XLg} from "react-bootstrap-icons";
import {
    changeBasketWareCount, clearBasket, confirmOrder,
    fetch_cart_wares,
    removeFromBasket
} from "../../redux/action_creators/ware_action_creator";
import {Link} from "react-router-dom";
import {ImagesEndpoint} from "../../global_variables";

interface ICardModal {
    show: boolean,
    closeHandler: () => void,
}

const CartModal: FC<ICardModal> = ({show, closeHandler}) => {
    const dispatch = useAppDispatch()
    const {isAuthorized} = useAppSelector(state => state.authorizationReducer)
    const {cartWares} = useAppSelector(state => state.wareReducer)
    const [sum, setSum] = useState(0)
    useEffect(() => {
        dispatch(fetch_cart_wares())
        let inter = setInterval(() => {
            if (cartWares.length > 0) dispatch(fetch_cart_wares())
        }, 600)
        return function (){
            clearInterval(inter)
        }
    }, [])
    useEffect(() => {
        dispatch(fetch_cart_wares())
    }, [show])
    useEffect(() => {
        let summ = 0;
        cartWares.forEach((item) => {
            summ += (item.price * item.count);
        })
        setSum(summ)
    }, [cartWares])
    if (!show) return null
    return ReactDOM.createPortal(
        <Modal show={show} onHide={closeHandler}>
            <Modal.Header closeButton>
                <Modal.Title>Cart</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {!isAuthorized ?
                    <h3 className={'text-center my-4'}>Ви не авторизовані <Heartbreak className={'mb-1'}/></h3> : <></>}
                {!isAuthorized ?
                    <h3 className={'text-center my-4'}>Ви не авторизовані <Heartbreak className={'mb-1'}/></h3> :
                    <>
                        {cartWares.length > 0 ?
                            cartWares.map(item => (
                                    <>
                                        <div className="card mx-auto mb-3">
                                            <button onClick={() => {dispatch(removeFromBasket(item.id));dispatch(fetch_cart_wares())}} className={"p-0 btn btn-secondary position-absolute top-0 end-0 mt-3 me-3"}><XLg height={20} width={20}/></button>
                                            <img style={{"width": "100%", height: 350, objectFit: 'cover'}}
                                                 src={`${ImagesEndpoint}/Get/${item.thumbnail}`}
                                                 className="card-img-top" alt="..."/>
                                            <div className="card-body">
                                                <h5 className="card-title">{item.name}</h5>
                                                <p className="card-text">Size:{item.size!}</p>
                                                <p className="card-text">{item.price} грн.</p>
                                                {item.isDiscount ? <p className="card-text">
                                                    <del>{item.oldPrice} грн.</del>
                                                </p> : <></>
                                                }
                                                <Link onClick={closeHandler} to={`/Wares/${item.id!}`}
                                                      className="btn btn-primary">More</Link>
                                                <button onClick={(e: any) => {
                                                    let count = Number(e.currentTarget.nextSibling?.innerHTML)
                                                    if(count < item.countInStorage){
                                                        dispatch(changeBasketWareCount(item.id, Number(e.currentTarget.nextSibling.innerHTML) + 1))
                                                        e.currentTarget.nextSibling.innerHTML = Number(e.currentTarget.nextSibling.innerHTML) + 1
                                                    }
                                                }
                                                } className={'btn btn-secondary mb-1 ms-3 me-3 p-0'}><PlusCircle height={20} width={20}/></button>
                                                <p className={'d-inline'}>{item.count}</p>
                                                <button onClick={(e: any) => {
                                                    let count = Number(e.currentTarget.previousElementSibling?.innerHTML)
                                                    if(count > 1){
                                                        dispatch(changeBasketWareCount(item.id, count - 1));
                                                        e.currentTarget.previousElementSibling.innerHTML = count - 1
                                                    }
                                                }} className={'btn btn-secondary mb-1 ms-3 p-0'}><DashCircle height={20} width={20}/></button>
                                            </div>
                                        </div>
                                    </>
                                )
                            )
                            : <p>Oops, it's empty</p>}
                        <div className="modal-footer">
                            <p className={'my-3 d-inline p-2'}>Order for the amount:{sum} грн.</p>
                            <ButtonGroup aria-label="Basic example">
                                <button disabled={cartWares.length < 1} onClick={() => {
                                    if(cartWares.length > 0)
                                        dispatch(clearBasket(() => dispatch(fetch_cart_wares())))}
                                } type="button" className="btn btn-danger">Clear basket</button>
                                <button disabled={cartWares.length < 1} onClick={() => {
                                    if(cartWares.length > 0)
                                        dispatch(confirmOrder(() => dispatch(fetch_cart_wares())))}
                                } type="button" className="btn btn-primary">Confirm order</button>
                            </ButtonGroup>
                        </div>
                    </>
                }
            </Modal.Body>
        </Modal>,
        document.getElementById('portal') as HTMLElement
    );
};

export default CartModal;