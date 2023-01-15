import React from 'react';
import {Google} from "react-bootstrap-icons";
import {Link} from "react-router-dom";
import {useForm} from "react-hook-form";
import {login, sign_in} from "../../redux/action_creators/authorization_action_creator";
import {useAppDispatch, useAppSelector} from "../../hooks/redux";

const RegistrationPage = () => {
    const dispatch = useAppDispatch()
    const server_errors = useAppSelector(state => state.authorizationReducer.error)
    const {
        register,
        formState: {
            errors,
        },
        handleSubmit,
    } = useForm()

    const SignIn = (data: any) => {
        if (data.password === data.password2){
            dispatch(sign_in({
                username: data.username,
                password: data.password,
                email: data.email,
                adress: data.adress ?? "",
                age: data.age,
                surname: data.surname,
                name: data.name
            }))
        }else{
            errors!.password2!.message = "Passwords do not match!"
        }
    };
    return (
        <div className={"container-fluid"}>
            <div className="row mt-5">
                <div className="col-md-5 col-lg-4 m-auto">
                    <div className="my-5 border border-1 ">
                        <div className="w-100 border-bottom border-1 py-3">
                            <h5 className="text-center">Wear | New user</h5>
                            <p className={"text-danger text-center"}>{server_errors}</p>
                        </div>
                        <form onSubmit={handleSubmit(SignIn)}>
                            <div className="mb-3 px-5 mt-2">
                                <label htmlFor="username" className="form-label">
                                    Username
                                </label>
                                <input
                                    type="text"
                                    id={'username'}
                                    className="form-control"
                                    placeholder={"Your username"}
                                    {...register('username', {
                                        required: true,
                                        pattern: {
                                            value: /^[A-Za-z0-9_]+$/g,
                                            message: "Please use only latin letters, numbers and _ in username!"
                                        },
                                        minLength: {
                                            value: 6,
                                            message: "Username must have at least 6 characters"
                                        },
                                    })}
                                />
                                <span className={"text-danger mt-1 opacity-100"}>{errors?.username && "This field is required!"}</span>
                            </div>
                            <div className="mb-3 px-5">
                                <label htmlFor="password" className="form-label">
                                    Password
                                </label>
                                <input
                                    type="password"
                                    id={'password'}
                                    className="form-control"
                                    placeholder={"Your password"}
                                    {...register('password', {
                                        required: true,
                                        pattern: {
                                            value: /^[A-Za-z0-9]+$/g,
                                            message: "Please use only latin letters and numbers in password!"
                                        },
                                        minLength: {
                                            value: 6,
                                            message: "Password must have at least 6 characters",
                                        }
                                    })}
                                />
                                <span className={"text-danger mt-1 opacity-100"}>{errors?.password && "This field is required!"}</span>
                            </div>
                            <div className="mb-3 px-5">
                                <label htmlFor="password" className="form-label">
                                    Password Check
                                </label>
                                <input
                                    type="password"
                                    id={'password2'}
                                    className="form-control"
                                    placeholder={"Your password"}
                                    {...register('password2', {
                                        required: true,
                                        pattern: {
                                            value: /^[A-Za-z0-9]+$/g,
                                            message: "Please use only latin letters and numbers in password!"
                                        },
                                        minLength: {
                                            value: 6,
                                            message: "Password must have at least 6 characters",
                                        },
                                        onBlur: (event) => {
                                            if (event.currentTarget.value !== event.currentTarget.parentNode.parentNode.password.value){
                                                event.currentTarget.nextElementSibling.innerHTML = "Passwords do not match!"
                                            }
                                        },
                                        onChange: (event) => {
                                            event.currentTarget.nextElementSibling.innerHTML = ""
                                        }
                                    })}
                                />
                                <span className={"text-danger mt-1 opacity-100"}>{errors?.password2 && "This field is required!"}</span>
                            </div>
                            <div className="mb-3 px-5 mt-2">
                                <label htmlFor="name" className="form-label">
                                    First name
                                </label>
                                <input
                                    type="text"
                                    id={'name'}
                                    className="form-control"
                                    placeholder={"John"}
                                    {...register('name', {
                                        required: true,
                                    })}
                                />
                                <span className={"text-danger mt-1 opacity-100"}>{errors?.name && "This field is required!"}</span>
                            </div>
                            <div className="mb-3 px-5 mt-2">
                                <label htmlFor="surname" className="form-label">
                                    Last name
                                </label>
                                <input
                                    type="text"
                                    id={'surname'}
                                    className="form-control"
                                    placeholder={"Wick"}
                                    {...register('surname', {
                                        required: true,
                                    })}
                                />
                                <span className={"text-danger mt-1 opacity-100"}>{errors?.surname && "This field is required!"}</span>
                            </div>
                            <div className="mb-3 px-5 mt-2">
                                <label htmlFor="email" className="form-label">
                                    Email
                                </label>
                                <input
                                    type="text"
                                    id={'email'}
                                    className="form-control"
                                    placeholder={"example@example.com"}
                                    {...register('email', {
                                        required: true,
                                    })}
                                />
                                <span className={"text-danger mt-1 opacity-100"}>{errors?.email && "This field is required!"}</span>
                            </div>
                            <div className=" mb-3 px-5 mt-2">
                                <label htmlFor="age" className="form-label">
                                    Age
                                </label>
                                <div className={"input-group"}>
                                    <input
                                        type="range"
                                        id={'age'}
                                        className="form-control"
                                        placeholder={"Wick"}
                                        min={1}
                                        max={100}
                                        defaultValue={18}
                                        {...register('age', {
                                            required: false,
                                        })}
                                        onInput={(e)=> {e.currentTarget!.nextElementSibling!.innerHTML = e.currentTarget.value}}
                                    />
                                    <span className=" input-group-text">18</span>
                                </div>
                            </div>
                            <div className="mb-3 px-5 mt-2">
                                <label htmlFor="adress" className="form-label">
                                    Residence
                                </label>
                                <input
                                    type="text"
                                    id={'adress'}
                                    className="form-control"
                                    placeholder={"вулиця Чуднівська, 103, Житомир, Житомирська область, 10005"}
                                />
                            </div>
                            <div className="px-5 mb-3 padding-bottom text-center text-sm-start">
                                <button className={"btn btn-primary me-1 px-4"} type={"submit"}>Sign In</button>
                                <button className={"btn btn-secondary me-1 px-4"} type={"submit"}><Google height={18} width={18}/></button>
                            </div>
                            <p className={"px-5 text-center text-sm-start"}><Link to={"/Cabinet"}>Have an account?</Link></p>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default RegistrationPage;