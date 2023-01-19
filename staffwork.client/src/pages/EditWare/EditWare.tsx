import React, {useEffect, useState} from 'react';
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {useForm} from "react-hook-form";
import {
    create_ware,
    fetch_all_brands,
    fetch_all_categories,
    fetch_ware_by_id
} from "../../redux/action_creators/ware_action_creator";
import {Link, useParams} from "react-router-dom";
import {IGetByIdWare, IWare} from "../../types/ware";
import EditForm from "./EditForm";

const EditWare = () => {
    const dispatch = useAppDispatch()
    const {id} = useParams()
    const getByIdWare = useAppSelector(state => state.wareReducer.getByIdWare)

    useEffect(() => {
        dispatch(fetch_all_categories())
        dispatch(fetch_all_brands())
        dispatch(fetch_ware_by_id(Number(id!)))
    }, [])

    document.title = "Edit ware"
    return (
        <div className={'container-fluid'}>
            <div className={'col-md-5 col-lg-4 m-auto'}>
                <div className={'my-5 border border-1 py-3'}>
                    <div className={'w-100 border-bottom border-1 py-3'}>
                        <h5 className="text-center">Wear | Ware editing</h5>
                        {/*<p className={"text-danger text-center"}>{server_errors ? server_errors : ""}</p>*/}
                    </div>
                    {getByIdWare != null ? <EditForm getByIdWare={getByIdWare}/> : <></>}
                </div>
            </div>
        </div>
    );
};

export default EditWare;