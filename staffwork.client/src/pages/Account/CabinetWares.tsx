import React, {FC, useEffect, useState} from 'react';
import {Link} from "react-router-dom";
import {CaretDown, CaretUp} from "react-bootstrap-icons";
import {delete_ware, fetch_all_categories, fetch_all_wares} from "../../redux/action_creators/ware_action_creator";
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {IWare} from "../../types/ware";
import DeleteWareModel from "./DeleteWareModel";
import CabinetWareItem from "./CabinetWareItem";

interface ICabinetWares {
    allWares: IWare[]
}

const CabinetWares: FC<ICabinetWares> = ({allWares}) => {
    const dispatch = useAppDispatch()
    const categories = useAppSelector(state => state.wareReducer.allCategories.categories)
    const [sortField, setSortField] = useState({value: "Name", isReverse: false})
    const [categoryId, setCategoryId] = useState<number | null>(null)
    const [countOfRecords, setCountOfRecords] = useState(12)
    useEffect(() => {
        fetch_all()
    }, [sortField, categoryId, countOfRecords])
    useEffect(() => {
        dispatch(fetch_all_categories())
    }, [])
    const fetch_all = () => {
        dispatch(fetch_all_wares({
            sortBy: {value: sortField.value, isReverse: sortField.isReverse},
            categoryId: categoryId === null ? null : categoryId,
            countOfRecords: countOfRecords
        }))
    }
    const changeSortHandler = (field: string) => {
        if (field === "PriceUp")
            setSortField({value: "Price", isReverse: false})

        if (field === "PriceDown")
            setSortField({value: "Price", isReverse: true})
    }
    return (
        <div className={`mx-auto mt-3 mb-5`}>
            <p className={'text-center'}><Link to={`/NewWare`}>Add ware</Link></p>
            <table className="table table-borderless">
                <tbody>
                <tr>
                    <td>
                        <div className={'text-center row mt-3'}>
                            <div className="col">
                                <p>Sort {sortField.isReverse ? <CaretDown/> : <CaretUp/>}</p>
                                <select onChange={e => changeSortHandler(e.currentTarget.value)}
                                        className={'form-select'}>
                                    <option value="PriceUp">Cheaper</option>
                                    <option value="PriceDown">More expensive</option>
                                </select>
                            </div>
                            <div className="col">
                                <p>Category</p>
                                <div className="input-group">
                                    <select onChange={(e) => setCategoryId(Number(e.currentTarget.value))} className={'form-select py-lg-0'}>
                                        {categories?.map(item => (
                                            <option value={item.id}>{item.name}</option>
                                        ))}
                                    </select>
                                    <span onClick={(e:any) => {setCategoryId(null);e.currentTarget.previousElementSibling.value = ""}} className="input-group-text hover">CLEAR</span>
                                </div>
                            </div>
                            <form className="d-flex input-group-sm mt-3" role="search">
                                <input className="form-control py-0" type="search" placeholder="Search by name"/>
                                <button onClick={(e: any) => {
                                    dispatch(fetch_all_wares({keyWords: e.currentTarget.previousElementSibling!.value}));
                                    e.preventDefault()
                                }} className="btn btn-outline-success" type="submit">Search
                                </button>
                                <button onClick={(e:any) => {fetch_all();e.preventDefault();e.currentTarget.previousElementSibling!.previousElementSibling!.value = ""}} className={'btn btn-outline-primary'}>Reset</button>
                            </form>
                        </div>

                    </td>
                </tr>
                {allWares.map(item => (
                    <CabinetWareItem item={item} fetchCallBack={() => {
                        fetch_all()
                    }}/>
                ))}

                </tbody>
            </table>
            {allWares.length >= countOfRecords ? <button onClick={e => setCountOfRecords(countOfRecords+12)} className={'btn btn-outline-secondary position-absolute start-50 mb-5 py-1'}>More</button> : <></>}
        </div>
    );
};

export default CabinetWares;