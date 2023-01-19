import { useEffect } from "react";
import { Toast, ToastContainer } from "react-bootstrap";
import { useAppDispatch, useAppSelector } from "../../hooks/redux";
import {MessageType, NotificationReducer} from "../../redux/reducers/NotificationReducer";
import ReactDOM from "react-dom";

const NotificationToastMessage = () => {
    const { DEACTIVATE_MESSAGE } = NotificationReducer.actions;
    const dispatch = useAppDispatch();
    const messages: MessageType[] | null = useAppSelector((state) => state.NotificationReducer.messages);
    const activeMessages = messages ? messages.filter((message) => message.isActive) : null;

    const deactivateMessage = (id: number) => {
        dispatch(DEACTIVATE_MESSAGE(id));
    };

    const pushMessage = (id: number) => {
        setTimeout(() => {
            deactivateMessage(id);
        }, 3000);
    };

    useEffect(() => {
        if (activeMessages) {
            if (activeMessages.length > 0) {
                const lastMessage = activeMessages!.length - 1;
                const id = activeMessages![lastMessage].id;
                pushMessage(id);
                if(activeMessages.length > 5){
                    deactivateMessage(activeMessages[0].id);
                }
            }
        }
    }, [activeMessages]);

    return ReactDOM.createPortal(
        <>
            <ToastContainer className="position-fixed end-0 bottom-0 mb-4 me-4 p-1">
                {activeMessages != null ? (
                    activeMessages.reverse().map((message: MessageType) => {
                        return (
                            <Toast
                                className={message.isSuccess ? "bg-success" : message.isError ? "bg-danger" : "bg-warning"}
                                key={message.id}
                            >
                                <div className="position-relative">
                                    <Toast.Body className={"text-white"}>{message.message}</Toast.Body>
                                    <button
                                        onClick={() => deactivateMessage(message.id)}
                                        className={"btn btn-close position-absolute end-0 top-0 mt-1 me-2"}
                                    ></button>
                                </div>
                            </Toast>
                        );
                    })
                ) : (
                    <></>
                )}
            </ToastContainer>
        </>,
        document.getElementById('portal') as HTMLElement
    );
};

export default NotificationToastMessage;