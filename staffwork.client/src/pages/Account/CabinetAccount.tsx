import React, {FC} from 'react';
import {Google, Mailbox, Person, PersonBadgeFill} from "react-bootstrap-icons";
import {IUser} from "../../types/user";
import {useAppDispatch} from "../../hooks/redux";
import {useForm} from "react-hook-form";
import {update_self_info} from "../../redux/action_creators/users_action_creator";

interface ICabinetAccount {
    user: IUser | null
}

const CabinetAccount: FC<ICabinetAccount> = ({user}) => {
    const dispatch = useAppDispatch()
    const {
        register,
        formState: {
            errors,
        },
        handleSubmit,
    } = useForm()

    const submitHandler = (data: any) => {
        dispatch(update_self_info({
            name: data.Name,
            username: data.Username,
            adress: data.Adress,
            email: data.Email,
            age: data.Age,
            surname: data.Surname
        }))
    }
    return (
        <div className={`text-center mt-5`}>
            <form className={'text-center'} onSubmit={handleSubmit(submitHandler)}>
                <div className="input-group mb-3">
                    <span className="input-group-text" id="basic-addon1"><PersonBadgeFill height={20}  width={20}/></span>
                    <input {...register("Username", {
                        required: true,
                        value: user?.username
                    })} type="text" className="form-control" placeholder="Username"/>
                </div>
                <div className="input-group mb-3">
                    <span className="input-group-text" id="basic-addon1"><Person height={20} width={20}/>1</span>
                    <input {...register("Name", {
                        required: true,
                        value: user?.name
                    })} type="text" className="form-control"
                           placeholder="First Name"/>
                </div>
                <div className="input-group mb-3">
                    <span className="input-group-text" id="basic-addon1"><Person height={20} width={20}/>2</span>
                    <input {...register("Surname", {
                        required: true,
                        value: user?.surname
                    })}  type="text" className="form-control"
                           placeholder="Last Name"/>
                </div>
                <div className="input-group mb-3">
                    <span className="input-group-text" id="basic-addon1">Age</span>
                    <input {...register("Age", {
                        required: true,
                        value: user?.age
                    })}  type="number" className="form-control"
                           placeholder="Age"/>
                </div>
                <div className="input-group mb-3">
                    <span className="input-group-text" id="basic-addon1"><Google height={20} width={20}/></span>
                    <input {...register("Email", {
                        required: true,
                        value: user?.email
                    })} type="text" className="form-control" placeholder="Email"/>
                </div>
                <div className="input-group mb-3">
                    <span className="input-group-text" id="basic-addon1"><Mailbox height={20} width={20}/></span>
                    <input {...register("Adress", {
                        required: false,
                        value: user?.adress
                    })} type="text" className="form-control"
                           placeholder="Adress"/>
                </div>
                <p className={"text-center"}>
                    <button type={'submit'} className={"btn btn-primary w-100"}>Update</button>
                </p>
            </form>
        </div>
    );
};

export default CabinetAccount;