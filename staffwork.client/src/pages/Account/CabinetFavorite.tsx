import React, {FC} from 'react';
import {Link} from "react-router-dom";
import {fetch_favorite_wares, remove_from_favorite} from "../../redux/action_creators/ware_action_creator";
import {Heartbreak} from "react-bootstrap-icons";
import {IWare} from "../../types/ware";
import {useAppDispatch} from "../../hooks/redux";
import {ImagesEndpoint} from "../../global_variables";

interface ICabinetFavorite {
    favoriteWares: IWare[]
}
const CabinetFavorite:FC<ICabinetFavorite> = ({favoriteWares}) => {
    const dispatch = useAppDispatch()
    const fetch_favorite = () => {
        dispatch(fetch_favorite_wares())
    }
    return (
        <div className={`w-50 mx-auto mt-3`}>
            {/*<p className={'text-center'}><Link to={'NewWare'}>New ware</Link></p>*/}
            <table className="table table-borderless">
                <tbody>
                {favoriteWares.length > 0 ? favoriteWares.map(item => (
                    <tr>
                        <td>
                            <Link className={'text-decoration-none'} to={`/Wares/${item.wareId}`}>
                                <div className={'d-block position-relative'}>
                                    <div className={'row border border-2'}>
                                        <div className="col-3">
                                            <img height={200} width={200}
                                                 src={`${ImagesEndpoint}/Get/${item.thumbnail}`}
                                                 alt=""/>
                                            <div className={'position-absolute top-0 start-0 mt-2'}>
                                                {item.isDiscount ? (
                                                    <p className="bg-danger px-2 py-1 text-white d-inline">-{100 - Math.floor(100 * item.price / item.oldPrice)}%</p>) : <></>}
                                                {item.countInStorage < 10 ? (
                                                    <p className="bg-danger px-2 py-1 text-white d-inline">{item.countInStorage < 1 ? <>Ended</> : <>Ends</>}</p>) : <></>}
                                            </div>
                                        </div>
                                        <div className="col">
                                            <h4 className={'mt-4'}>{item.name}</h4>
                                            <p className="card-text text-muted">{item.sizes}</p>
                                            <p className="text-danger d-inline">{item.price} грн.</p>
                                            {item.isDiscount ? <p className="text-muted d-inline">
                                                <del>{item.oldPrice} грн.</del>
                                            </p> : <></>}
                                            {item.countInStorage > 0 ?
                                                <></> :
                                                <p className={'text-decoration-underline mt-1'}>Ended</p>}
                                        </div>
                                    </div>
                                    <button onClick={() => dispatch(remove_from_favorite(item.wareId!, () => fetch_favorite()))} className="btn position-absolute bottom-0 end-0"> Remove from favorite</button>
                                </div>
                            </Link>
                        </td>
                    </tr>
                )) : <h3 className={'text-center mt-5'}>Oops, it's empty<Heartbreak width={64} height={64}/></h3>}
                </tbody>
            </table>
        </div>
    );
};

export default CabinetFavorite;