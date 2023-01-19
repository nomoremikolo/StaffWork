import React, {FC, useState} from 'react';
import DeleteWareModel from "./DeleteWareModel";
import {Link} from "react-router-dom";
import {IWare} from "../../types/ware";
import {ImagesEndpoint} from "../../global_variables";

interface ICabinetWareItem {
    item: IWare,
    fetchCallBack: () => void
}
const CabinetWareItem:FC<ICabinetWareItem> = ({item, fetchCallBack}) => {
    const [showDeleteModal, setShowDeleteModal] = useState(false)
    return (
        <tr>
            <td>
                <div className={'d-block position-relative'}>
                    <div className={'row border border-2'}>
                        <div className="col-lg-2 col-md-2 me-lg-0 me-lg-5">
                            <img height={200} width={"100%"}
                                 style={{objectFit: 'cover'}}
                                 src={`${ImagesEndpoint}/Get/${item.thumbnail}`}
                                 alt=""/>
                            <div className={'position-absolute top-0 start-0 mt-2'}>
                                {item.isDiscount ? (
                                    <p className="bg-danger px-2 py-1 text-white d-inline">-{100 - Math.floor(100 * item.price / item.oldPrice)}%</p>) : <></>}
                                {item.countInStorage < 10 ? (
                                    <p className="bg-danger px-2 py-1 text-white d-inline">{item.countInStorage < 1 ? <>Ended</> : <>Ends</>}</p>) : <></>}
                            </div>
                        </div>
                        <div className="col-8 col-lg pb-5 pb-md">
                            <h4 className={'mt-4'}>{item.name}</h4>
                            <p className="card-text text-muted">{item.sizes}</p>
                            <p className="text-danger d-inline">{item.price} грн.</p>
                            {item.isDiscount ? <p className="text-muted d-inline">
                                <del>{item.oldPrice} грн.</del>
                            </p> : <></>}
                            {item.countInStorage > 0 ?
                                <p className={'mt-1'}>{item.countInStorage} in storage</p> :
                                <p className={'text-decoration-underline mt-1'}>Ended</p>}
                            <div className={'position-absolute bottom-0 end-0'}>
                                <button
                                    onClick={() => setShowDeleteModal(true)}
                                    className={'btn btn-danger py-lg-1 mb-1'}>Delete
                                </button>
                                <DeleteWareModel show={showDeleteModal} closeHandler={(statusCode) => {
                                    if (statusCode != null)
                                        fetchCallBack()
                                    setShowDeleteModal(false)
                                }} id={item.id}/>
                                <Link className={'btn btn-warning py-lg-1 mb-1'}
                                      to={`/EditWare/${item.id}`}>Update</Link>
                                <Link className={'btn btn-primary py-lg-1 mb-1'}
                                      to={`/Wares/${item.id}`}>More</Link>
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    );
};

export default CabinetWareItem;