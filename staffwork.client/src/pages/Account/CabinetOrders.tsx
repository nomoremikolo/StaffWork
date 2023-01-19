import React, {useEffect, useState} from 'react';
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {get_orders, update_order} from "../../redux/action_creators/ware_action_creator";
import {Accordion} from "react-bootstrap";
import CabinetOrderItem from "./CabinetOrderItem";
import {CaretDown, CaretUp} from "react-bootstrap-icons";
import {fetch_all_users} from "../../redux/action_creators/users_action_creator";

const CabinetOrders = () => {
    const dispatch = useAppDispatch()
    const orders = useAppSelector(state => state.wareReducer.orders.orders)
    const [filterField, setFilterField] = useState<string | null>(null)
    const [filter, setFilter] = useState<boolean | null>(null)
    const [orderNumber, setOrderNumber] = useState<number | null>(null)
    useEffect(() => {
        dispatch(get_orders())
    }, [ ])

    useEffect(() => {
        if (filterField === "true"){
            setFilter(true)
        }
        if (filterField === "false"){
            setFilter(false)
        }
        if (filterField === ""){
            setFilter(null)

        }

    }, [filterField])

    useEffect(() => {
        fetchOrderHandler()
    }, [filter, orderNumber])

    const fetchOrderHandler = () => {
        dispatch(get_orders(filter, orderNumber))
    }
    return (
        <div className={'mt-4 mx-auto'}>
            <div className={'row mb-2'}>
                <div className="col">
                    <form className="d-flex input-group-sm" role="search">
                        <input className="form-control py-0" type="search" placeholder="Search by name"/>
                        <button onClick={(e: any) => {
                            setOrderNumber(Number(e.currentTarget.previousElementSibling!.value) ?? null);
                            e.preventDefault()
                        }} className="btn btn-outline-success" type="submit">Search
                        </button>
                        <button onClick={(e:any) => {setOrderNumber(null);e.preventDefault();e.currentTarget.previousElementSibling!.previousElementSibling!.value = ""}} className={'btn btn-outline-primary'}>Reset</button>
                    </form>
                </div>
                <div className="col">
                    <div className="">
                        <select onChange={e => setFilterField(e.currentTarget.value)}
                                className={'form-select'}>
                            <option selected value="">All</option>
                            <option value="true">Confirmed</option>
                            <option value="false">Not confirmed</option>
                        </select>
                    </div>
                </div>
            </div>
            <Accordion defaultActiveKey="0">
                {orders.map(item => (
                    <CabinetOrderItem item={item} />
                ))}
            </Accordion>
        </div>
    );
};

export default CabinetOrders;