import React, {FC} from 'react';
import {useAppDispatch} from "../../hooks/redux";
import {Button, Modal} from "react-bootstrap";
import {useForm} from "react-hook-form";
import {fetch_all_wares, update_order} from "../../redux/action_creators/ware_action_creator";

interface IEditOrderModal {
    show: boolean,
    closeHandler: (statusCode?: number) => void,
    id: number,
    status: string,
    isConfirmed: boolean
}
const EditOrderModal:FC<IEditOrderModal> = ({id, closeHandler, show, status, isConfirmed}) => {
    const dispatch = useAppDispatch()
    const {
        register,
        formState: {
            errors,
        },
        handleSubmit,
    } = useForm()

    const submitHandler = (data: any) => {
        dispatch(update_order(id, data.Status, data.IsConfirmed, closeHandler))
        dispatch(fetch_all_wares())
    }
    if (!show) return null
    return (
        <Modal show={show} onHide={closeHandler}>
            <Modal.Header closeButton>
                <Modal.Title>Edit order</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <form onSubmit={handleSubmit(submitHandler)}>
                    <div className="mb-3">
                        <label htmlFor="status" className="form-label">Status</label>
                        <input {...register("Status", {
                            required: true,
                            value: status
                        })} type="text" className="form-control" id="status"/>
                    </div>
                    <div className="mb-3">
                        <label htmlFor="status" className="form-label">IsConfirmed</label>
                        <select {...register("IsConfirmed", {
                            required: true,
                            value: isConfirmed
                        })} className={'form-select'}>
                            <option value="true">Yes</option>
                            <option value="false">No</option>
                        </select>
                    </div>
                    <Button onClick={() => closeHandler()} variant="secondary">Close</Button>
                    <Button type={'submit'} variant="danger">Edit</Button>
                </form>
            </Modal.Body>
        </Modal>
    );
};

export default EditOrderModal;