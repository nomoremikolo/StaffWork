import React from 'react';
import {Google} from "react-bootstrap-icons";
import {Link} from "react-router-dom";
import {useForm} from "react-hook-form";
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {authorizationReducer} from "../../redux/reducers/authorizationReducer";
import {login} from "../../redux/action_creators/authorization_action_creator";

const LoginPage = () => {
    const dispatch = useAppDispatch()
    const server_errors = useAppSelector(state => state.authorizationReducer.error)
    const {
        register,
        formState: {
            errors,
        },
        handleSubmit,
    } = useForm()

    const LogIn = (data: any) => {
        dispatch(login(data.username, data.password))
    };
    document.title = "Authorization"
    return (
        <div className={"container-fluid"}>
            <div className="row mt-5">
                <div className="col-md-5 col-lg-4 m-auto">
                    <div className="my-5 border border-1 ">
                        <div className="w-100 border-bottom border-1 py-3">
                            <h5 className="text-center">Wear | Authorization</h5>
                            <p className={"text-danger text-center"}>{server_errors ? server_errors : ""}</p>
                        </div>
                        <form onSubmit={handleSubmit(LogIn)}>
                            <div className="mb-3 px-5 mt-2">
                                <label htmlFor="username" className="form-label">
                                    Username
                                </label>
                                <input
                                    type="text"
                                    className="form-control"
                                    placeholder={"Your username"}
                                    {...register('username', {
                                        required: true,
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
                                    className="form-control"
                                    placeholder={"Your password"}
                                    {...register('password',{
                                        required: true
                                    })}
                                />
                                <span className={"text-danger mt-1 opacity-100"}>{errors?.password && "This field is required!"}</span>
                            </div>
                            <div className="px-5 mb-3 padding-bottom text-center text-sm-start">
                                <button className={"btn btn-primary me-1 px-4"} type={"submit"}>Log in</button>
                                <button className={"btn btn-secondary me-1 px-4"} type={"submit"}><Google height={18} width={18}/></button>
                            </div>
                            <p className={"px-5 text-center text-sm-start"}><Link to={"/SignIn"}>Don't have account?</Link></p>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default LoginPage;