import React, {useEffect, useState} from 'react';
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {fetch_discount_wares, fetch_discount_wares_authorized} from "../../redux/action_creators/ware_action_creator";
import {CaretDown, CaretUp} from "react-bootstrap-icons";
import WareMockup from "../../components/WareMockup";

const Discounts = () => {
    const dispatch = useAppDispatch()
    const {error,isLoading,discountWares} = useAppSelector(state => state.wareReducer)
    const {isAuthorized} = useAppSelector(state => state.authorizationReducer)
    const [sortField, setSortField] = useState({value: "Name", isReverse: false})
    useEffect(() => {
        // console.log(isAuthorized)
        if(!isAuthorized){
            dispatch(fetch_discount_wares({sortBy: {value: "price", isReverse: false}}))
        }else{
            dispatch(fetch_discount_wares_authorized({sortBy: {value: "price", isReverse: false}}))
        }

    }, [ ])
    useEffect(() => {
        if(!isAuthorized){
            dispatch(fetch_discount_wares({sortBy: {value: sortField.value, isReverse: sortField.isReverse}}))
        }else{
            dispatch(fetch_discount_wares_authorized({sortBy: {value: sortField.value, isReverse: sortField.isReverse}}))
        }
    }, [sortField])
    const changeSortHandler = (field: string) => {
        setSortField({value: field, isReverse: field === sortField.value ? !sortField.isReverse : false})
    }
    return (
        <div className="container d-block mb-5">
            <div className="d-block">
                <br/>
                <br/>
                <div className={"text-center"}>
                    <p>Sort by {sortField.isReverse ? <CaretUp/> : <CaretDown/>}</p>
                    <button style={{backgroundColor: sortField.value === "Name" ? "red" : ""}} onClick={() => changeSortHandler('Name')} className={"btn btn-primary"}>Name</button>
                    <button style={{backgroundColor: sortField.value === "Price" ? "red" : ""}} onClick={() => changeSortHandler("Price")} className={"btn btn-primary"}>Price</button>
                    <button style={{backgroundColor: sortField.value === "Sizes" ? "red" : ""}} onClick={() => changeSortHandler("Sizes")} className={"btn btn-primary"}>Sizes</button>
                    <button style={{backgroundColor: sortField.value === "OldPrice" ? "red" : ""}} onClick={() => changeSortHandler("OldPrice")} className={"btn btn-primary"}>OldPrice</button>
                </div>
            </div>
            <div className="text-center text-xxl-start row">
                {discountWares?.map(item =>
                    <WareMockup key={item.id} item={item}/>
                )}
            </div>
        </div>
    );
};

export default Discounts;