import React, {FC, useState} from 'react';
import {Accordion} from "react-bootstrap";
import {get_orders, update_order} from "../../redux/action_creators/ware_action_creator";
import {useAppDispatch} from "../../hooks/redux";
import {IOrderGraphType} from "../../types/ware";
import EditOrderModal from "./EditOrderModal";
interface ICabinetOrderItem {
    item: IOrderGraphType
}
const CabinetOrderItem:FC<ICabinetOrderItem> = ({item}) => {
    const dispatch = useAppDispatch()
    const [showSetModal, setShowSetModal] = useState(false)
    return (
        <Accordion.Item eventKey={`${item.orderId}`}>
            <Accordion.Header>
                №{item.orderId} | {item.status} | {item.isConfirmed ? <button onClick={e => {dispatch(update_order(item.orderId, "Canceled", false, () => {
                dispatch(get_orders())
            }));e.stopPropagation()}} className={'mx-1 btn btn-success text-white py-0 px-1'}>Click to cancel</button> : <button onClick={e => {dispatch(update_order(item.orderId, "Confirmed", true, () => {
                dispatch(get_orders())
            }));e.stopPropagation()}} className={'mx-1 btn btn-danger text-white py-0 px-1'}>Click to confirm</button>}
                <button onClick={e => {e.stopPropagation();setShowSetModal(true)}} className={'btn btn-primary px-2 py-0 position-absolute end-0 me-5'}>Edit</button>
            </Accordion.Header>
            <EditOrderModal id={item.orderId} show={showSetModal} closeHandler={() => {setShowSetModal(false); dispatch(get_orders())}} status={item.status} isConfirmed={item.isConfirmed}/>
            <Accordion.Body className={'table-responsive'}>
                <table className={'table'}>
                    <thead>
                    <tr>
                        <th>
                            WareName
                        </th>
                        <th>
                            Sizes
                        </th>
                        <th>
                            Size
                        </th>
                        <th>
                            Price
                        </th>
                        <th>
                            CountInStorage
                        </th>
                        <th>
                            Count
                        </th>
                    </tr>
                    </thead>
                    <tbody>
                    {item.orderWares.map(ware => (
                        <tr>
                            <td>
                                {ware.wareName}
                            </td>
                            <td>
                                {ware.sizes}
                            </td>
                            <td>
                                {ware.size}
                            </td>
                            <td>
                                {ware.price}
                            </td>
                            <td>
                                {ware.countInStorage}
                            </td>
                            <td>
                                {ware.count}
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </table>

            </Accordion.Body>
        </Accordion.Item>
    );
};

export default CabinetOrderItem;