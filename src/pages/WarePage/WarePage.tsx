import React, {useEffect} from 'react';
import {useParams} from "react-router-dom";
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {add_to_cart, add_to_favorite, fetch_ware_by_id} from "../../redux/action_creators/ware_action_creator";
import {ButtonGroup, Carousel} from "react-bootstrap";

const WarePage = () => {
    const id = useParams()
    const dispatch = useAppDispatch()
    const {ware} = useAppSelector(state => state.wareReducer)

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
    useEffect(() => {
        console.log(ware)
    }, [ware])
    return (
        <>
            <div className={'container-fluid bg-dark bg-opacity-10 py-5'}>
                <div className={'container'}>
                    <div className="row">
                        <div className="col-12 col-lg-6">
                            <Carousel variant={'dark'}>
                                {ware?.images?.split(" ").map(item => (
                                    <Carousel.Item>
                                        <img
                                            className="d-block w-50 text-center mx-auto"
                                            src={new Image().src = item ?? ""}
                                            alt="First slide"
                                        />
                                    </Carousel.Item>
                                ))}
                            </Carousel>
                        </div>
                        <div className="col">
                            <h1 className={'mt-5 text-center text-md'}>{ware?.name}</h1>
                            <hr className={'d-md-none'}/>
                            <h5 className={'text-center text-md'}>CATEGORY: {ware?.categoryId} BRAND: {ware?.brandId}</h5>
                            <hr className={'d-md-none'}/>
                            {ware?.countInStorage! > 0 ? <h4 className={'text-center text-md'}>{ware?.isDiscount ?
                                <p className={'text-danger'}>{ware?.price} грн. <del
                                    className={'text-muted'}>{ware?.oldPrice} грн</del></p> :
                                <p>{ware?.price} грн</p>}</h4> : <h4 className={'text-decoration-underline text-center my-4'}>Ended</h4>}
                            <hr className={'d-md-none'}/>
                            <form className={'bg-dark bg-opacity-25 px-4 pt-3 pb-5'}>
                                <p className={'text-center text-md'}>Розмір</p>
                                <div className={'text-center text-md'}>
                                    <select className={'form-select form-select-sm'}>
                                        <option value=""></option>
                                        {ware?.sizes.split(" ").map(item => (
                                            <option value="">{item}</option>
                                        ))}
                                    </select>
                                </div>
                            </form>
                            <div className={'text-center mt-4'}>
                                <ButtonGroup className={''}>
                                    <button disabled={ware?.countInStorage! > 0 ? false : true}
                                            onClick={() => dispatch(add_to_cart(ware!.id))}
                                            className={'btn btn-success py-4 py-lg-4 px-5 mb-4 mb-md-0 px-lg'}>Add to cart
                                    </button>
                                    <button onClick={() => dispatch(add_to_favorite(ware!.id))}
                                            className={'btn btn-primary py-4 py-lg-4 px-5 mb-4 mb-md-0 px-lg'}>Add to
                                        favorite
                                    </button>
                                </ButtonGroup>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div className={'container mt-5 mb-5'}>
                <h4 className={'text-center mb-5'}>Description</h4>
                <pre className={'fs-5 desc'}>
                    {ware?.description}
                </pre>
            </div>
        </>
    );
};

export default WarePage;