import React, {useEffect, useState} from 'react';
import {useParams} from "react-router-dom";
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {fetch_all_permissions, fetch_user_by_id} from "../../redux/action_creators/users_action_creator";
import {useForm} from "react-hook-form";
import Select from "react-select";
import CabinetUserEditForm from "./CabinetUserEditForm";

const CabinetUserEdit = () => {
    const {id} = useParams()
    const {user, isLoading} = useAppSelector(state => state.UsersReducer.getUserById)
    const dispatch = useAppDispatch()

    useEffect(() => {
        dispatch(fetch_all_permissions())
    }, [ ])
    useEffect(() => {
        dispatch(fetch_user_by_id(Number(id)))
    }, [id])
    return (
        <div>
            {isLoading ? <></> :
                <div className={'container-fluid'}>
                    {user != null ? <CabinetUserEditForm user={user}/> : <></>}
                </div>
            }
        </div>
    );
};

export default CabinetUserEdit;