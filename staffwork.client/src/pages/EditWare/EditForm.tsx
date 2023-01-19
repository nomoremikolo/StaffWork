import React, {FC, useState} from 'react';
import {useForm} from "react-hook-form";
import {
    create_ware,
    fetch_all_brands,
    fetch_all_categories,
    update_ware
} from "../../redux/action_creators/ware_action_creator";
import {Link, useNavigate} from "react-router-dom";
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {IGetByIdWare} from "../../types/ware";
import {wareReducer} from "../../redux/reducers/wareReducer";

interface IEditForm {
    getByIdWare: IGetByIdWare,
}
const EditForm:FC<IEditForm> = ({getByIdWare}) => {
    const dispatch = useAppDispatch()
    const [ware, setWare] = useState<IGetByIdWare | null>(getByIdWare)
    const [thumbnail, setThumbnail] = useState<ArrayBuffer | null | string>("");
    const [images, setImages] =  useState<any>([]);
    const categories = useAppSelector(state => state.wareReducer.allCategories.categories)
    const brands = useAppSelector(state => state.wareReducer.allBrands.brands)
    const navigate = useNavigate()
    const {
        register,
        formState: {
            errors,
        },
        handleSubmit,
    } = useForm()
    const submitHandler = (data: any) => {
        dispatch(update_ware({
            id: ware?.id!,
            name: data.Name,
            brandId: data.Brand,
            categoryId: data.Category,
            description: data.Description,
            sizes: data.Sizes,
            price: data.Price,
            oldPrice: data.OldPrice,
            isDiscount: data.IsDiscount,
            countInStorage: data.CountInStorage,
            thumbnail: thumbnail,
            images: images.join(" "),
        },(statusCode) => {dispatch(wareReducer.actions.CLEAR_WARE_BY_ID());navigate(-1)}))
    }
    const selectThumbnailHandler = (e:any) => {
        let file = e.target.files[0]
        console.log(file)
        let reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = function () {
            setThumbnail(reader.result)
        };
        reader.onerror = function (error) {
            console.log('Error: ', error);
        };
    }
    const selectImagesHandler = async (e: React.ChangeEvent<HTMLInputElement>) => {
        let files = e.target.files
        for (let i = 0; i < files!.length; i++){
            await imgToBase64(files![i])
        }
    }
    const imgToBase64 = async(file:any) => {
        let reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
            setImages((prevState: any) => {
                return [...prevState, reader.result];
            });
        };
    }
    return (
        <form onSubmit={handleSubmit(submitHandler)}>
            <div className="mt-2 mb-3 px-5">
                <label htmlFor="Name" className="form-label">Name</label>
                <input
                    // onInput={(e) => setWare({...ware, name: ""})}
                    {...register('Name', {
                        required: true,
                        value: ware?.name
                    })} type="text" className="form-control" id="Name"/>
            </div>
            <div className="mt-2 mb-3 px-5">
                <label htmlFor="Brand" className="form-label">Brand</label>
                <select onClick={() => dispatch(fetch_all_brands())} {...register('Brand', {
                    required: true,
                    value: ware?.brandId
                })} className={'form-select'} id="Brand">
                    <option></option>
                    {brands?.map(item => (
                        <option value={item.id}>{item.name}</option>
                    ))}
                </select>
                <Link target="_blank" to={"/NewBrand"}>Create new brand</Link>
            </div>
            <div className="mt-2 mb-3 px-5">
                <label htmlFor="Category" className="form-label">Category</label>
                <select onClick={() => dispatch(fetch_all_categories())} {...register('Category', {
                    required: true,
                    value: ware?.categoryId
                })} className={'form-select'} id="Category">
                    <option></option>
                    {categories?.map(item => (
                        <option value={item.id}>{item.name}</option>
                    ))}
                </select>
                <Link target="_blank" to={"/NewCategory"}>Create new category</Link>
            </div>
            <div className="mt-2 mb-3 px-5">
                <label htmlFor="Sizes" className="form-label">Sizes</label>
                <input {...register('Sizes', {
                    required: true,
                    value: ware?.sizes
                })} type="text" className="form-control" id="Sizes"/>
            </div>
            <div className="mt-2 mb-3 px-5">
                <label htmlFor="Price" className="form-label">Price</label>
                <input {...register('Price', {
                    required: true,
                    value: ware?.price
                })} type="text" className="form-control" id="Price"/>
            </div>
            <div className="mt-2 mb-3 px-5">
                <label htmlFor="OldPrice" className="form-label">Old price</label>
                <input {...register('OldPrice', {
                    required: false,
                    value: ware?.oldPrice
                })} type="text" className="form-control" id="OldPrice"/>
            </div>
            <div className="mt-2 mb-3 px-5">
                <label htmlFor="IsDiscount" className="form-label me-2">Is discount</label>
                <input {...register('IsDiscount', {
                    required: false,
                    value: ware?.isDiscount
                })} className="form-check-input" type="checkbox" value="" id="IsDiscount"/>
            </div>
            <div className="mt-2 mb-3 px-5">
                <label htmlFor="CountInStorage" className="form-label">Count in storage</label>
                <input {...register('CountInStorage', {
                    required: true,
                    value: ware?.countInStorage
                })} className="form-control" type="number" id="CountInStorage"/>
            </div>
            <div className="mt-2 mb-3 px-5">
                <label htmlFor="Thumbnail" className="form-label">Thumbnail</label>
                <input {...register('Thumbnail', {
                    required: false,
                })} accept="image/png, image/gif, image/jpeg" onChange={(e:any) => selectThumbnailHandler(e)} className="form-control" type="file" id="Thumbnail"/>
            </div>
            <div className="mt-2 mb-3 px-5">
                <label htmlFor="Images" className="form-label">Images</label>
                <input {...register('Images', {
                    required: false,
                })} accept="image/png, image/gif, image/jpeg" multiple={true} onChange={(e:any) => selectImagesHandler(e)} className="form-control" type="file" id="Images"/>
                <button onClick={(e:any) => {setImages([]);e.currentTarget.previousElementSibling.value = ""}} className={'btn btn-primary'}>Reset</button>
            </div>
            <div className="mt-2 mb-3 px-5">
                <label htmlFor="Description" className="form-label">Description</label>
                <textarea {...register('Description', {
                    required: true,
                    value: ware?.description
                })} id={'Description'} className={'form-control'}></textarea>
            </div>
            <div className={'px-5'}>
                <button type="submit" className="btn btn-primary">Submit</button>
            </div>
        </form>
    );
};

export default EditForm;