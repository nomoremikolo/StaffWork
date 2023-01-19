import React, {useEffect, useState} from 'react';
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {
    fetch_all_categories,
    fetch_all_wares,
    fetch_discount_wares, fetch_discount_wares_authorized, fetch_favorite_wares,
    fetch_novelty_wares, fetch_novelty_wares_authorized
} from "../../redux/action_creators/ware_action_creator";
import {CaretDown, CaretUp} from "react-bootstrap-icons";
import WareMockup from "../../components/WareMockup";
import Loading from "../Layout/Loading";

const Novelty = () => {
    const dispatch = useAppDispatch()
    const {error,isLoading,noveltyWares} = useAppSelector(state => state.wareReducer)
    const {isAuthorized} = useAppSelector(state => state.authorizationReducer)
    const [sortField, setSortField] = useState({value: "Name", isReverse: false})
    const [categoryId, setCategoryId] = useState<number | null>(null)
    const categories = useAppSelector(state => state.wareReducer.allCategories.categories)
    const [countOfRecords, setCountOfRecords] = useState(12)

    useEffect(() => {
        fetch_all()
    }, [sortField, categoryId,  isAuthorized, countOfRecords])
    useEffect(() => {
        dispatch(fetch_all_categories())
    }, [])
    const fetch_all = () => {
        isAuthorized ? dispatch(fetch_novelty_wares_authorized({
            sortBy: {value: sortField.value, isReverse: sortField.isReverse},
            categoryId: categoryId === null ? null : categoryId,
            countOfRecords: countOfRecords
        })) : dispatch(fetch_novelty_wares({
            sortBy: {value: sortField.value, isReverse: sortField.isReverse},
            categoryId: categoryId === null ? null : categoryId,
            countOfRecords: countOfRecords
        }))
    }
    const changeSortHandler = (field: string) => {
        if (field === "PriceUp")
            setSortField({value: "Price", isReverse: false})

        if (field === "PriceDown")
            setSortField({value: "Price", isReverse: true})
    }

    document.title = "Novelty"
    return (
        <div className="container d-block">
            <div className="d-block">
                <br/>
                <br/>
                <div className={'text-center row mt-3'}>
                    <div className="col">
                        <p>Sort {sortField.isReverse ? <CaretDown/> : <CaretUp/>}</p>
                        <select onChange={e => changeSortHandler(e.currentTarget.value)}
                                className={'form-select'}>
                            <option value="PriceUp">Cheaper</option>
                            <option value="PriceDown">More expensive</option>
                        </select>
                    </div>
                    <div className="col">
                        <p>Category</p>
                        <div className="input-group">
                            <select onChange={(e) => setCategoryId(Number(e.currentTarget.value))} className={'form-select py-0'}>
                                {categories?.map(item => (
                                    <option value={item.id}>{item.name}</option>
                                ))}
                            </select>
                            <span onClick={(e:any) => {setCategoryId(null);e.currentTarget.previousElementSibling.value = ""}} className="input-group-text hover">CLEAR</span>
                        </div>
                    </div>
                    <form className="d-flex input-group-sm mt-3" role="search">
                        <input className="form-control py-0" type="search" placeholder="Search by name"/>
                        <button onClick={(e: any) => {
                            dispatch(isAuthorized ? fetch_novelty_wares_authorized({keyWords: e.currentTarget.previousElementSibling!.value}) : fetch_novelty_wares({keyWords: e.currentTarget.previousElementSibling!.value}));
                            e.preventDefault()
                        }} className="btn btn-outline-success" type="submit">Search
                        </button>
                        <button onClick={(e:any) => {fetch_all();e.preventDefault();e.currentTarget.previousElementSibling!.previousElementSibling!.value = ""}} className={'btn btn-outline-primary'}>Reset</button>
                    </form>
                </div>
            </div>
            <div className="text-center text-xxl-start row">
                {noveltyWares?.map(item =>
                    <WareMockup key={item.id} item={item}/>
                )}
            </div>
            {noveltyWares.length >= countOfRecords ? <button onClick={e => setCountOfRecords(countOfRecords+12)} className={'btn btn-outline-secondary position-absolute start-50 mt-5 mb-5 py-1'}>More</button> : <></>}
        </div>
    );
};

export default Novelty;