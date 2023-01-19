import React, {useEffect, useState} from 'react';
import {Carousel} from "react-bootstrap";
import {Link} from "react-router-dom";
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {
    fetch_all_wares,
    fetch_discount_wares, fetch_discount_wares_authorized,
    fetch_novelty_wares, fetch_novelty_wares_authorized
} from "../../redux/action_creators/ware_action_creator";
import WareMockup from "../../components/WareMockup";

const Welcome = () => {
    const dispatch = useAppDispatch()
    const {noveltyWares, discountWares} = useAppSelector(state => state.wareReducer)
    const {isLoading} = useAppSelector(state => state.wareReducer)
    const {isAuthorized} = useAppSelector(state => state.authorizationReducer)
    const [countOfDiscountRecords, setCountDiscountOfRecords] = useState(4)
    const [countOfNoveltyRecords, setCountOfNoveltyRecords] = useState(4)
    useEffect(() => {
        dispatch(isAuthorized ? fetch_discount_wares_authorized({countOfRecords: countOfDiscountRecords}) : fetch_discount_wares({countOfRecords: countOfDiscountRecords}))
        dispatch(isAuthorized ? fetch_novelty_wares_authorized({countOfRecords: countOfNoveltyRecords}) : fetch_novelty_wares({countOfRecords: countOfNoveltyRecords}))
    }, [ ])

    useEffect(() => {
        dispatch(isAuthorized ? fetch_discount_wares_authorized({countOfRecords: countOfDiscountRecords}) : fetch_discount_wares({countOfRecords: countOfDiscountRecords}))
        dispatch(isAuthorized ? fetch_novelty_wares_authorized({countOfRecords: countOfNoveltyRecords}) : fetch_novelty_wares({countOfRecords: countOfNoveltyRecords}))
    }, [countOfDiscountRecords, countOfNoveltyRecords])
    document.title = "Welcome"
    return (
        <>
            <div className="container-fluid px-0 ">
                <Carousel>
                    <Carousel.Item>
                        <img
                            className="d-block w-100"
                            src="https://www.staff-clothes.com/_next/image/?url=https%3A%2F%2Fstatic.staff-clothes.com%2Fuploads%2Fmedia%2Fdefault%2F0002%2F70%2F61c8caa3d6044fb2b1ebac8e0d95333a.jpeg&w=1920&q=75"
                            alt="First slide"
                        />
                    </Carousel.Item>
                    <Carousel.Item>
                        <img
                            className="d-block w-100"
                            src="https://www.staff-clothes.com/_next/image/?url=https%3A%2F%2Fstatic.staff-clothes.com%2Fuploads%2Fmedia%2Fdefault%2F0002%2F72%2F42d486c092fb4fa9aaa1b6697c8b172a.jpeg&w=1920&q=75"
                            alt="Second slide"
                        />
                    </Carousel.Item>
                </Carousel>
                <Link to={"/All"}
                      className="btn btn-primary py-2 position-absolute start-50 translate-middle carousel-enter">Go
                    over
                </Link>
            </div>
            <div className="container mt-5">
                <div className="row">
                    <div className="col my-3">
                        <img
                            src="https://www.staff-clothes.com/_next/image/?url=https%3A%2F%2Fstatic.staff-clothes.com%2Fuploads%2Fmedia%2Fdefault%2F0002%2F70%2F408ad6d730404fada9a91a9e49d80f49.jpeg&w=1920&q=75"
                            alt=""/>
                    </div>
                    <div className="col my-3">
                        <img
                            src="https://www.staff-clothes.com/_next/image/?url=https%3A%2F%2Fstatic.staff-clothes.com%2Fuploads%2Fmedia%2Fdefault%2F0002%2F70%2F67895284092c4f14881dfa6e5eb2ef60.jpeg&w=1920&q=75"
                            alt=""/>
                    </div>
                    <div className="col my-3">
                        <img
                            src="https://www.staff-clothes.com/_next/image/?url=https%3A%2F%2Fstatic.staff-clothes.com%2Fuploads%2Fmedia%2Fdefault%2F0002%2F70%2Fe1d4b958f1ca4f0484f76ed20fd3737d.jpeg&w=1920&q=75"
                            alt=""/>
                    </div>
                    <div className="col my-3">
                        <img
                            src="https://www.staff-clothes.com/_next/image/?url=https%3A%2F%2Fstatic.staff-clothes.com%2Fuploads%2Fmedia%2Fdefault%2F0002%2F70%2F37f1284fda264fe1ad23aea1c8a1cc2f.jpeg&w=1920&q=75"
                            alt=""/>
                    </div>
                </div>
            </div>
            <div className="container mt-5">
                <div className="d-block">
                    <h2 className="d-xxl-inline text-center text-xxl-start fw-bold border-3 border-dark border-bottom">DISCOUNTS</h2>
                </div>
                <div className="text-center text-xxl-start row">
                    {discountWares.map(item =>
                        <WareMockup item={item}/>
                    )}
                </div>
                {discountWares.length >= countOfDiscountRecords ? <button onClick={e => setCountDiscountOfRecords(countOfDiscountRecords+12)} className={'btn btn-outline-secondary position-absolute start-50 mt-5 mb-5 py-1'}>More</button> : <></>}
            </div>
            <div className="container mt-5 mb-5 d-block">
                <div className="d-block">
                    <h2 className="d-xxl-inline text-center text-xxl-start fw-bold border-3 border-dark border-bottom">NOVELTIES</h2>
                </div>
                <div className="text-center text-xxl-start row mb-5">
                    {noveltyWares.map((item) =>
                        <WareMockup item={item}/>
                    )}
                </div>
                {noveltyWares.length >= countOfNoveltyRecords ? <button onClick={e => setCountOfNoveltyRecords(countOfNoveltyRecords+12)} className={'btn btn-outline-secondary position-absolute start-50 mb-5 py-1'}>More</button> : <></>}
            </div>
        </>
    );
};

export default Welcome;