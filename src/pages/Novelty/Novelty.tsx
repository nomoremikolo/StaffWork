import React, {useEffect, useState} from 'react';
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {fetch_discount_wares, fetch_novelty_wares} from "../../redux/action_creators/ware_action_creator";
import {CaretDown, CaretUp} from "react-bootstrap-icons";
import WareMockup from "../../components/WareMockup";

const Novelty = () => {
    const dispatch = useAppDispatch()
    const {error,isLoading,noveltyWares} = useAppSelector(state => state.wareReducer)
    const [sortField, setSortField] = useState({value: "Name", isReverse: false})
    useEffect(() => {
        dispatch(fetch_novelty_wares({sortBy: {value: "price", isReverse: false}}))
    }, [ ])
    useEffect(() => {
        dispatch(fetch_novelty_wares({sortBy: {value: sortField.value, isReverse: sortField.isReverse}}))
    }, [sortField])
    const changeSortHandler = (field: string) => {
        setSortField({value: field, isReverse: field === sortField.value ? !sortField.isReverse : false})
    }
    return (
        <div className="container d-block">
            <div className="d-block">
                <br/>
                <br/>
                <div className={"text-center"}>
                    <p>Sort by {sortField.isReverse ? <CaretDown/> : <CaretUp/>}</p>
                    <button style={{backgroundColor: sortField.value === "Name" ? "red" : ""}} onClick={() => changeSortHandler('Name')} className={"btn btn-primary"}>Name</button>
                    <button style={{backgroundColor: sortField.value === "Price" ? "red" : ""}} onClick={() => changeSortHandler("Price")} className={"btn btn-primary"}>Price</button>
                    <button style={{backgroundColor: sortField.value === "Sizes" ? "red" : ""}} onClick={() => changeSortHandler("Sizes")} className={"btn btn-primary"}>Sizes</button>
                </div>
            </div>
            <div className="text-center text-xxl-start row">
                {noveltyWares?.map(item =>
                    <WareMockup key={item.id} item={item}/>
                )}
            </div>
        </div>
    );
};

export default Novelty;