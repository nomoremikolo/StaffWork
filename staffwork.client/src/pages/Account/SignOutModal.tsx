import React, {FC} from 'react';
import {useAppDispatch} from "../../hooks/redux";
import {sign_out} from "../../redux/action_creators/authorization_action_creator";
import {Button, Modal} from "react-bootstrap";
interface ISignOutModal {
    show: boolean,
    closeHandler: () => void,
}
const SignOutModal:FC<ISignOutModal> = ({closeHandler, show}) => {
    const dispatch = useAppDispatch()

    const signOutHandler = () => {
        dispatch(sign_out())
        closeHandler()
    }
    return (
        <Modal show={show} onHide={closeHandler}>
            <Modal.Header closeButton>
                <Modal.Title>Sign out</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <h6>Are you sure you want to sign out?</h6>
            </Modal.Body>
            <Modal.Footer>
                <Button onClick={() => closeHandler()} variant="secondary">Close</Button>
                <Button onClick={signOutHandler} variant="danger">SignOut</Button>
            </Modal.Footer>
        </Modal>
    );
};

export default SignOutModal;