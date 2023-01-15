import React, {useEffect, useState} from 'react';
import {Google, Mailbox, Person, PersonBadgeFill, PlusCircle} from "react-bootstrap-icons";
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {sign_out} from "../../redux/action_creators/authorization_action_creator";
import {Button, ButtonGroup} from "react-bootstrap";
import {
    delete_ware,
    fetch_all_wares,
    fetch_discount_wares,
    fetch_discount_wares_authorized
} from "../../redux/action_creators/ware_action_creator";
import {Link} from "react-router-dom";

const Cabinet = () => {
    const user = useAppSelector(state => state.authorizationReducer.user)
    const {wares, isLoading, errors} = useAppSelector(state => state.wareReducer.allWares)
    const [sortField, setSortField] = useState({value: "Name", isReverse: false})
    const dispatch = useAppDispatch()
    const [currentPage, setCurrentPage] = useState('account')
    const signOutHandler = () => {
        dispatch(sign_out())
    }

    useEffect(() => {
        if(!isAuthorized){
            dispatch(fetch_discount_wares({sortBy: {value: sortField.value, isReverse: sortField.isReverse}}))
        }else{
            dispatch(fetch_discount_wares_authorized({sortBy: {value: sortField.value, isReverse: sortField.isReverse}}))
        }
    }, [sortField])

    useEffect(() => {
        if (currentPage === 'wares')
            dispatch(fetch_all_wares())
    }, [currentPage])

    useEffect(() => {
        console.log(wares)
    }, [wares])

    return (
        <>
            <div className={"container"}>
                <h2 className={"text-center display-6 mt-5"}>Personal Account</h2>
                <p onClick={signOutHandler} style={{cursor: 'pointer'}}
                   className={"text-decoration-underline text-center"}>Sign Out</p>
            </div>
            <div className={"container"}>
                <div className={'text-center'}>
                    <ButtonGroup>
                        <Button onClick={() => setCurrentPage('account')}
                                className={`hover-white ${currentPage === 'account' ? 'opacity-75' : 'opacity-100'}`}
                                variant="primary">Account</Button>
                        <Button onClick={() => setCurrentPage('cart')}
                                className={`hover-white ${currentPage === 'cart' ? 'opacity-75' : 'opacity-100'}`}
                                variant="primary">Cart</Button>
                        <Button onClick={() => setCurrentPage('favorites')}
                                className={`hover-white ${currentPage === 'favorites' ? 'opacity-75' : 'opacity-100'}`}
                                variant="primary">Favorites</Button>
                        {user?.permissions?.includes(1) ? <>
                            <Button onClick={() => setCurrentPage('wares')}
                                    className={`hover-white ${currentPage === 'wares' ? 'opacity-75' : 'opacity-100'}`}
                                    variant="primary">Wares</Button>
                        </> : <></>}
                    </ButtonGroup>
                </div>
            </div>
            <div className={`container ${currentPage === 'account' ? 'd-block' : 'd-none'}`}>
                <div className={`w-50 mx-auto mt-5 `}>
                    <div className="input-group mb-3">
                        <span className="input-group-text" id="basic-addon1"><PersonBadgeFill height={20}
                                                                                              width={20}/></span>
                        <input disabled value={user?.username} type="text" className="form-control"
                               placeholder="Username"/>
                    </div>
                    {/*<div className="input-group mb-3">*/}
                    {/*    <span className="input-group-text" id="basic-addon1"><Bricks height={20} width={20}/></span>*/}
                    {/*    <input disabled value={} type="text" className="form-control" placeholder="Password"/>*/}
                    {/*</div>*/}
                    <div className="input-group mb-3">
                        <span className="input-group-text" id="basic-addon1"><Person height={20} width={20}/>1</span>
                        <input disabled value={user?.name} type="text" className="form-control"
                               placeholder="First Name"/>
                    </div>
                    <div className="input-group mb-3">
                        <span className="input-group-text" id="basic-addon1"><Person height={20} width={20}/>2</span>
                        <input disabled value={user?.surname} type="text" className="form-control"
                               placeholder="Last Name"/>
                    </div>
                    <div className="input-group mb-3">
                        <span className="input-group-text" id="basic-addon1"><Google height={20} width={20}/></span>
                        <input disabled value={user?.email} type="text" className="form-control" placeholder="Email"/>
                    </div>
                    <div className="input-group mb-3">
                        <span className="input-group-text" id="basic-addon1"><Mailbox height={20} width={20}/></span>
                        <input disabled value={user?.adress ?? ""} type="text" className="form-control"
                               placeholder="Adress"/>
                    </div>
                    <p className={"text-center"}>
                        <button className={"btn btn-primary w-100"}>Update</button>
                    </p>
                </div>
            </div>
            <div className={`container-fluid ${currentPage === 'wares' ? 'd-block' : 'd-none'}`}>
                <div className={`w-50 mx-auto mt-3`}>
                    {/*<p className={'text-center'}><Link to={'NewWare'}>New ware</Link></p>*/}
                    <table className="table table-borderless">
                        <tbody>
                        <tr>
                            <td>
                                <Link to={'/NewWare'}>
                                    <div className={'row border border-2'}>
                                        <div className="col text-center">
                                            <PlusCircle width={200} height={200} className={'py-5'}/>
                                        </div>
                                    </div>
                                </Link>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <form className="d-flex" role="search">
                                    <input className="form-control me-2" type="search" placeholder="Search"/>
                                    <button onClick={(e:any) => {dispatch(fetch_all_wares({keyWords: e.currentTarget.previousElementSibling!.value}));e.preventDefault()}} className="btn btn-outline-success" type="submit">Search</button>
                                </form>
                            </td>
                        </tr>
                        {wares.map(item => (
                            <tr>
                                <td>
                                    <div className={'d-block position-relative'}>
                                        <div className={'row border border-2'}>
                                            <div className="col-3">
                                                <img height={200} width={200}
                                                     src={new Image().src = item.thumbnail ?? ""}
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
                                                    <p className={'mt-1'}>{item.countInStorage} in storage</p> :
                                                    <p className={'text-decoration-underline mt-1'}>Ended</p>}
                                                <div className={'position-absolute bottom-0 end-0'}>
                                                    <button onClick={() => dispatch(delete_ware(item.id, () => dispatch(fetch_all_wares())))} className={'btn btn-danger py-1 mb-1'}>Delete</button>
                                                    <Link className={'btn btn-warning py-1 mb-1'}
                                                          to={`/Wares/${item.id}`}>Update</Link>
                                                    <Link className={'btn btn-primary py-1 mb-1'}
                                                          to={`/Wares/${item.id}`}>More</Link>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        ))}
                        </tbody>
                    </table>
                </div>
            </div>
        </>
    );
};

export default Cabinet;