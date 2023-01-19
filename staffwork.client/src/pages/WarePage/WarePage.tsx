import React, {useEffect, useState} from 'react';
import {Link, useParams} from "react-router-dom";
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {add_to_cart, add_to_favorite, fetch_ware_by_id} from "../../redux/action_creators/ware_action_creator";
import {ButtonGroup, Carousel} from "react-bootstrap";
import {PencilSquare} from "react-bootstrap-icons";
import {ImagesEndpoint} from "../../global_variables";
import {NotificationReducer} from "../../redux/reducers/NotificationReducer";

const WarePage = () => {
    const id = useParams()
    const dispatch = useAppDispatch()
    const {getByIdWare} = useAppSelector(state => state.wareReducer)
    const [size, setSize] = useState<string | null>(null)

    useEffect(() => {
        if (id !== undefined) {
            dispatch(fetch_ware_by_id(Number(id.id)))
        }
    }, [])

    useEffect(() => {
        if (id !== undefined) {
            dispatch(fetch_ware_by_id(Number(id.id)))
        }
    }, [id])
    const AddToCartHandler = () => {
        if (size === null || size === ""){
            dispatch(NotificationReducer.actions.SHOW_ERROR_MESSAGE('Error, please select size!'))
            return;
        }
        dispatch(add_to_cart(getByIdWare!.id, size))
    }
    document.title = "Ware"
    return (
        <>
            <div className={'container-fluid bg-dark bg-opacity-10 py-5'}>
                <div className={'container'}>
                    <div className="row">
                        <div className="col-12 col-lg-6">
                            <Carousel variant={'dark'}>
                                {getByIdWare?.images?.split(" ").map(item => (
                                    <Carousel.Item>
                                        <img
                                            style={{objectFit: 'cover'}}
                                            height={500}
                                            className="d-block w-50 text-center mx-auto"
                                            src={`${ImagesEndpoint}/Get/${item}`}
                                            alt="First slide"
                                        />
                                    </Carousel.Item>
                                ))}
                            </Carousel>
                        </div>
                        <div className="col">
                            <h1 className={'mt-5 text-center text-md'}>{getByIdWare?.name}</h1>
                            <hr className={'d-md-none'}/>
                            <h5 className={'text-center text-md'}>CATEGORY: {getByIdWare?.categoryName} | BRAND: {getByIdWare?.brandName}</h5>
                            <hr className={'d-md-none'}/>
                            {getByIdWare?.countInStorage! > 0 ? <h4 className={'text-center text-md'}>{getByIdWare?.isDiscount ?
                                <p className={'text-danger'}>{getByIdWare?.price} грн. <del
                                    className={'text-muted'}>{getByIdWare?.oldPrice} грн</del></p> :
                                <p>{getByIdWare?.price} грн</p>}</h4> : <h4 className={'text-decoration-underline text-center my-4'}>Ended</h4>}
                            <hr className={'d-md-none'}/>
                            <form className={'bg-dark bg-opacity-25 px-4 pt-3 pb-5'}>
                                <p className={'text-center text-md'}>Розмір</p>
                                <div className={'text-center text-md'}>
                                    <select onChange={e => setSize(e.currentTarget.value)} className={'form-select form-select-sm'}>
                                        <option value=""></option>
                                        {getByIdWare?.sizes.split(" ").map(item => (
                                            <option value={item}>{item}</option>
                                        ))}
                                    </select>
                                </div>
                                <div className={'text-center mt-4'}>
                                    <ButtonGroup className={''}>
                                        <button disabled={getByIdWare?.countInStorage! > 0 ? false : true}
                                                onClick={e => {e.preventDefault();AddToCartHandler()}}
                                                className={'btn btn-success py-4 py-lg-4 px-5 mb-4 mb-md-0 px-lg'}>Add to cart
                                        </button>
                                        <button onClick={() => dispatch(add_to_favorite(getByIdWare!.id))}
                                                className={'btn btn-primary py-4 py-lg-4 px-5 mb-4 mb-md-0 px-lg'}>Add to
                                            favorite
                                        </button>
                                    </ButtonGroup>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <div className={'container mt-5 mb-5'}>
                <h4 className={'text-center mb-5'}>Description</h4>
                <pre className={'fs-5 desc'}>
                    {getByIdWare?.description}
                </pre>
            </div>
        </>
    );
};

export default WarePage;