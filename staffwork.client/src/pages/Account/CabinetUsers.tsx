import React, {useEffect} from 'react';
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {fetch_all_users} from "../../redux/action_creators/users_action_creator";
import {Link} from "react-router-dom";
import {fetch_all_wares} from "../../redux/action_creators/ware_action_creator";

const CabinetUsers = () => {
    const dispatch = useAppDispatch()
    const {users, isLoading, errors} = useAppSelector(state => state.UsersReducer.allUsers)

    useEffect(() => {
        dispatch(fetch_all_users())
    }, [ ])
    return (
        <div className={'container-fluid mt-5'}>
            <form className="d-flex input-group-sm mt-3" role="search">
                <input className="form-control py-0" type="search" placeholder="Search by name"/>
                <button onClick={(e: any) => {
                    dispatch(fetch_all_users(e.currentTarget.previousElementSibling!.value ?? null));
                    e.preventDefault()
                }} className="btn btn-outline-success" type="submit">Search
                </button>
                <button onClick={(e:any) => {dispatch(fetch_all_users());e.preventDefault();e.currentTarget.previousElementSibling!.previousElementSibling!.value = ""}} className={'btn btn-outline-primary'}>Reset</button>
            </form>
            <div className={'table-responsive'}>
                <table className={'table'}>
                    <thead>
                        <tr>
                            <th>
                                Nickname
                            </th>
                            <th>
                                Name
                            </th>
                            <th>
                                Surname
                            </th>
                            <th>
                                Email
                            </th>
                            <th>
                                Adress
                            </th>
                            <th>
                                Role
                            </th>
                            <th>
                                Permissions
                            </th>
                            <th>
                                Control
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        {users.map(item => (
                            <tr>
                                <td>{item.username}</td>
                                <td>{item.name}</td>
                                <td>{item.surname}</td>
                                <td>{item.email}</td>
                                <td>{item.adress}</td>
                                <td>{item.role}</td>
                                <td>{item.permissions.join(" ")}</td>
                                <td>
                                    <Link to={`/EditUser/${item.id}`} className={'btn btn-success py-0'}>
                                        EDIT
                                    </Link>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default CabinetUsers;