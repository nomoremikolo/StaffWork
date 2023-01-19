import React, {FC, useEffect, useState} from 'react';
import {Button, Modal} from "react-bootstrap";
import {Link} from "react-router-dom";
import ReactDOM from "react-dom";
import {delete_ware, fetch_all_wares} from "../../redux/action_creators/ware_action_creator";
import {useAppDispatch} from "../../hooks/redux";

interface IDeleteWareModel {
    show: boolean,
    closeHandler: (statusCode?: number) => void,
    id: number,
}
const DeleteWareModel:FC<IDeleteWareModel> = ({show, closeHandler, id}) => {
    const dispatch = useAppDispatch()

    const deleteHandler = () => {
        dispatch(delete_ware(id, (statusCode) => {
            closeHandler(statusCode)
        }))
    }
    if(!show) return null
    return(
        <Modal show={show} onHide={closeHandler}>
            <Modal.Header closeButton>
                <Modal.Title>Delete ware</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <h6>Are you sure you want to delete this ware?</h6>
            </Modal.Body>
            <Modal.Footer>
                <Button onClick={() => closeHandler()} variant="secondary">Close</Button>
                <Button onClick={deleteHandler} variant="danger">Delete</Button>
            </Modal.Footer>
        </Modal>
    );
};

export default DeleteWareModel;