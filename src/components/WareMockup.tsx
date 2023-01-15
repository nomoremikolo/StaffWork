import React, {FC, useEffect, useRef, useState} from 'react';
import {IWare} from "../types/ware";
import "./WareMockup.module.css"
import {Heart, HeartFill} from "react-bootstrap-icons";
import {Link, useNavigate} from "react-router-dom";
import {useAppDispatch} from "../hooks/redux";
import {wareReducer} from "../redux/reducers/wareReducer";
import {add_to_favorite, remove_from_favorite} from "../redux/action_creators/ware_action_creator";

interface IWareMockupProps{
    item: IWare
}
const WareMockup:FC<IWareMockupProps> = ({item}) => {
    const dispatch = useAppDispatch()
    const [liked, setLiked] = useState<boolean>(item.isFavorite != null && true ? item.isFavorite : false)
    // console.log(item.isFavorite)
    const navigate = useNavigate()

    const handleClick = () => {
        setLiked(!liked)
        dispatch(liked ? remove_from_favorite(item.id) : add_to_favorite(item.id))
    }
    useEffect(() => {
        setLiked(item.isFavorite ?? liked)
    }, [item])
    return (
        <div className="grow card hover shadow mt-4 me-lg-4 col-12 col-md-4 mx-auto mx-lg-0 d-inline-block" style={{"width": "18rem"}}>
            <Link className={'text-decoration-none'} to={`/Wares/${item.id}`}>
                {item.isDiscount ? (<p className="position-absolute top-0 start-0 bg-danger px-2 py-1 m-2 text-white">-{100 - Math.floor(100 * item.price / item.oldPrice)}%</p>) : <></>}
                {item.countInStorage < 10 ? (<p className="position-absolute top-0 end-0 bg-danger px-2 py-1 m-2 text-white">{item.countInStorage < 1 ? <>Ended</> : <>Ends</>}</p>) : <></>}
                <img
                    src={new Image().src = item.thumbnail ?? ""}
                    className="card-img-top blur-elem pt-3" alt="..."/>
                <div className="card-body">
                    <h5 className="card-title"><p className={'d-inline'}>{item.name}</p></h5>
                    <p className="card-text text-muted">{item.sizes}</p>
                    {item.countInStorage > 0 ? <>
                        <p className="text-danger d-inline">{item.price} грн.</p>
                        <p className="text-muted d-inline">
                            <del>{item.oldPrice} грн.</del>
                        </p>
                    </> : <p className={'text-decoration-underline'}>Ended</p>}
                </div>
            </Link>
            <button className="btn position-absolute bottom-0 end-0" onClick={handleClick}>{liked ? <HeartFill height={20} width={20}/> : <Heart height={20} width={20}/>}</button>
        </div>
    );
};

export default WareMockup;