import React from 'react';
import {useAppDispatch} from "../../hooks/redux";
import {useForm} from "react-hook-form";
import {create_new_brand, create_new_category} from "../../redux/action_creators/ware_action_creator";

const NewBrand = () => {
    const dispatch = useAppDispatch()
    const {
        register,
        formState: {
            errors,
        },
        handleSubmit,
    } = useForm()

    const submitHandler = (data: any) => {
        const statusCode = dispatch(create_new_brand({name: data.Name, countryManufactured: data.CountryManufactured, phone: data.Phone}, (statusCode) => statusCode))
    }
    return (
        <div className={'container-fluid'}>
            <div className={'col-md-5 col-lg-4 m-auto'}>
                <div className={'my-5 border border-1 py-3'}>
                    <div className={'w-100 border-bottom border-1 py-3'}>
                        <h5 className="text-center">Wear | New brand</h5>
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
                        <div className="mt-2 mb-3 px-5">
                            <label htmlFor="Phone" className="form-label">Phone number</label>
                            <input
                                {...register('Phone', {
                                    required: true,
                                })} type="text" className="form-control" id="Phone"/>
                        </div>
                        <div className="mt-2 mb-3 px-5">
                            <label htmlFor="CountryManufactured" className="form-label">Country manufactured</label>
                            <input
                                {...register('CountryManufactured', {
                                    required: true,
                                })} type="text" className="form-control" id="CountryManufactured"/>
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

export default NewBrand;