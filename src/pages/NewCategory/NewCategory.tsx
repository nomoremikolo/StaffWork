import React from 'react';
import {
    create_new_category,
    fetch_all_brands,
    fetch_all_categories
} from "../../redux/action_creators/ware_action_creator";
import {Link} from "react-router-dom";
import {useAppDispatch} from "../../hooks/redux";
import {useForm} from "react-hook-form";

const NewCategory = () => {
    const dispatch = useAppDispatch()
    const {
        register,
        formState: {
            errors,
        },
        handleSubmit,
    } = useForm()

    const submitHandler = (data: any) => {
        const statusCode = dispatch(create_new_category(data.Name, (statusCode) => { alert(statusCode);}))
    }
    return (
        <div className={'container-fluid'}>
            <div className={'col-md-5 col-lg-4 m-auto'}>
                <div className={'my-5 border border-1 py-3'}>
                    <div className={'w-100 border-bottom border-1 py-3'}>
                        <h5 className="text-center">Wear | New category</h5>
                        {/*<p className={"text-danger text-center"}>{server_errors ? server_errors : ""}</p>*/}
                    </div>
                    <form onSubmit={handleSubmit(submitHandler)}>
                        <div className="mt-2 mb-3 px-5">
                            <label htmlFor="Name" className="form-label">Name</label>
                            <input
                                {...register('Name', {
                                    required: true,
                                })} type="text" className="form-control" id="Name"/>
                        </div>
                        <div className={'px-5'}>
                            <button type="submit" className="btn btn-primary">Submit</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default NewCategory;