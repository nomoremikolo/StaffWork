import React, {FC, useEffect, useState} from 'react';
import Select from "react-select";
import {useForm} from "react-hook-form";
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {IUser} from "../../types/user";
import {update_user} from "../../redux/action_creators/users_action_creator";
import {useNavigate} from "react-router-dom";
interface ICabinetUserEditForm {
    user: IUser
}
const CabinetUserEditForm:FC<ICabinetUserEditForm> = ({user}) => {
    const [permissionsOptions, setPermissionsOptions] = useState<{ label: string; value: string }[]>([]);
    const [currentPermissionsOptions, setCurrentPermissionsOptions] = useState<{ label: string; value: string }[]>([]);
    const permissionsList = useAppSelector(state => state.UsersReducer.permissions)
    const navigate = useNavigate()

    const [newPermissions, setPermissions] = useState<null | number[]>(null)
    const dispatch = useAppDispatch()
    const {
        register,
        formState: {
            errors,
        },
        handleSubmit,
    } = useForm()
    useEffect(() => {
        if (permissionsList != null){
            if (permissionsList.length > 0) {
                permissionsList.forEach((item) =>
                    setPermissionsOptions((prevState: any) => [
                        ...prevState,
                        {
                            value: item.id.toString(),
                            label: item.name,
                        },
                    ])
                );
            }
        }
    }, [permissionsList]);
    useEffect(() => {
        let userPermissions: { label: string; value: string }[] = [];
        if (user != null && permissionsList !== null) {
            user!.permissions.forEach((permission) => {
                userPermissions = [
                    ...userPermissions,
                    {value: permission.toString(), label: permissionsList.find((p) => p.id == permission)?.name ?? ""},
                ];
            });
            setCurrentPermissionsOptions(userPermissions);
        }
    }, [user])
    const onPermissionsChanged = (newValue: any) => {
        setCurrentPermissionsOptions(newValue);
        setPermissions((prevState) => {
            prevState = newValue.map((v: any) => Number(v.value));
            return prevState;
        });
    };
    const submitHandler = (data: any) => {
        dispatch(update_user({
            id: user.id,
            name: data.Name,
            surname: data.Surname,
            username: data.Username,
            role: data.Role,
            adress: data.Adress,
            email: data.Email,
            age: data.Age,
            isActivated: data.IsActivated,
            permissions: newPermissions,
        }))
        navigate(-1)
    }
    document.title = "Edit user"
    return (
        <div className={'col-md-5 col-lg-4 m-auto'}>
            <div className={'my-5 border border-1 py-3'}>
                <div className={'w-100 border-bottom border-1 py-3'}>
                    <h5 className="text-center">Wear | Edit user</h5>
                    {/*<p className={"text-danger text-center"}>{server_errors ? server_errors : ""}</p>*/}
                </div>
                <form onSubmit={handleSubmit(submitHandler)}>
                    <div className={'mb-3 mt-3 px-5'}>
                        <label className={'form-label'} htmlFor="username">Username</label>
                        <input {...register("Username", {
                            required: true,
                            value: user!.username ?? "",
                        })} id={'username'} className={'form-control'} type="text"/>
                    </div>
                    <div className={'mb-3 px-5'}>
                        <label className={'form-label'} htmlFor="name">Name</label>
                        <input {...register("Name", {
                            required: true,
                            value: user!.name ?? "",
                        })} id={'name'} className={'form-control'} type="text"/>
                    </div>
                    <div className={'mb-3 px-5'}>
                        <label className={'form-label'} htmlFor="surname">Surname</label>
                        <input {...register("Surname", {
                            required: true,
                            value: user!.surname ?? "",
                        })} id={'surname'} className={'form-control'} type="text"/>
                    </div>
                    <div className={'mb-3 px-5'}>
                        <label className={'form-label'} htmlFor="email">Email</label>
                        <input {...register("Email", {
                            required: true,
                            value: user!.email ?? "",
                        })} id={'email'} className={'form-control'} type="email"/>
                    </div>
                    <div className={'mb-3 px-5'}>
                        <label className={'form-label'} htmlFor="age">Age</label>
                        <input {...register("Age", {
                            required: false,
                            value: user!.age ?? "",
                        })} id={'age'} className={'form-control'} type="number"/>
                    </div>
                    <div className={'mb-3 px-5'}>
                        <label className={'form-label'} htmlFor="adress">Adress</label>
                        <input {...register("Adress", {
                            required: false,
                            value: user!.adress ?? "",
                        })} id={'adress'} className={'form-control'} type="text"/>
                    </div>
                    <div className={"mb-3 px-5"}>
                        <label className={'form-label'} htmlFor="permissions">Permissions</label>
                        <Select
                            id={'permissions'}
                            value={currentPermissionsOptions}
                            onChange={onPermissionsChanged}
                            placeholder={"Permissions"}
                            isMulti={true}
                            options={permissionsOptions}
                        />
                    </div>
                    <div className={'mb-3 px-5'}>
                        <label className={'form-label'} htmlFor="role">Role</label>
                        <input {...register("Role", {
                            required: true,
                            value: user!.role ?? "",
                        })} id={'role'} className={'form-control'} type="text"/>
                    </div>
                    <div className={'mb-3 px-5'}>
                        <label className={'form-label'} htmlFor="IsActivated">Is activated</label>
                        <input {...register("IsActivated", {
                            required: false,
                            value: user!.isActivated ?? "",
                        })} id={'IsActivated'} className={'form-check-input ms-2'} type="checkbox"/>
                    </div>
                    <div className={"px-5 mb-3"}>
                        <button className={'btn btn-primary '}>Edit</button>
                    </div>
                </form>
            </div>
        </div>

    );
};

export default CabinetUserEditForm;