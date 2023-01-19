import React, {useEffect} from 'react';
import {Accordion} from "react-bootstrap";
import CabinetOrderItem from "./CabinetOrderItem";
import {fetch_all_users} from "../../redux/action_creators/users_action_creator";
import {Link} from "react-router-dom";
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {delete_category, fetch_all_categories} from "../../redux/action_creators/ware_action_creator";

const CabinetCategories = () => {
    const dispatch = useAppDispatch()
    const {categories, isLoading, errors} = useAppSelector(state => state.wareReducer.allCategories)

    useEffect(() => {
        dispatch(fetch_all_categories())
    }, [ ])
    return (
        <div className={'container mt-5'}>
            <p className={'text-center'}><Link target="_blank" to={"/NewCategory"}>Create new category</Link></p>
            <div className={'table-responsive'}>
                <table className={'table text-center'}>
                    <thead>
                    <tr>
                        <th>
                            Id
                        </th>
                        <th>
                            Name
                        </th>
                        <th>Control</th>
                    </tr>
                    </thead>
                    <tbody>
                    {categories.map(item => (
                        <tr>
                            <td>{item.id}</td>
                            <td>{item.name}</td>
                            <td>
                                <button onClick={e => dispatch(delete_category(item.id, (statusCode) => {
                                    if (statusCode === 200){
                                        dispatch(fetch_all_categories())
                                    }
                                }))} className={'btn btn-danger py-0'}>
                                    Delete
                                </button>
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default CabinetCategories;