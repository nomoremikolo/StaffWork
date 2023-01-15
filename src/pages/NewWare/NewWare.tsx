import React, {useEffect, useState} from 'react';
import {useForm} from "react-hook-form";
import {useAppDispatch, useAppSelector} from "../../hooks/redux";
import {create_ware, fetch_all_brands, fetch_all_categories} from "../../redux/action_creators/ware_action_creator";
import {Link} from "react-router-dom";
interface base{
    item: string | base | ArrayBuffer | null
}
const NewWare = () => {
    const dispatch = useAppDispatch()
    const categories = useAppSelector(state => state.wareReducer.allCategories.categories)
    const brands = useAppSelector(state => state.wareReducer.allBrands.brands)
    const [thumbnail, setThumbnail] = useState<ArrayBuffer | null | string>("");
    const [images, setImages] =  useState<any>([]);
    const {
        register,
        formState: {
            errors,
        },
        handleSubmit,
    } = useForm()
    const submitHandler = (data: any) => {
        dispatch(create_ware({
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
        }))
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
    useEffect(() => {
        dispatch(fetch_all_categories())
        dispatch(fetch_all_brands())
    }, [])
    return (
        <div className={'container-fluid'}>
            <div className={'col-md-5 col-lg-4 m-auto'}>
                <div className={'my-5 border border-1 py-3'}>
                    <div className={'w-100 border-bottom border-1 py-3'}>
                        <h5 className="text-center">Wear | New ware</h5>
                        {/*<p className={"text-danger text-center"}>{server_errors ? server_errors : ""}</p>*/}
                    </div>
                    <form onSubmit={handleSubmit(submitHandler)}>
                        <div className="mt-2 mb-3 px-5">
                            <label htmlFor="Name" className="form-label">Name</label>
                            <input
                                {...register('Name', {
                                required: true,
                            })}type="text" className="form-control" id="Name"/>
                        </div>
                        <div className="mt-2 mb-3 px-5">
                            <label htmlFor="Brand" className="form-label">Brand</label>
                            <select onClick={() => dispatch(fetch_all_brands())} {...register('Brand', {
                                required: true,
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
                            })} type="text" className="form-control" id="Sizes"/>
                        </div>
                        <div className="mt-2 mb-3 px-5">
                            <label htmlFor="Price" className="form-label">Price</label>
                            <input {...register('Price', {
                                required: true,
                            })} type="text" className="form-control" id="Price"/>
                        </div>
                        <div className="mt-2 mb-3 px-5">
                            <label htmlFor="OldPrice" className="form-label">Old price</label>
                            <input {...register('OldPrice', {
                                required: false,
                            })} type="text" className="form-control" id="OldPrice"/>
                        </div>
                        <div className="mt-2 mb-3 px-5">
                            <label htmlFor="IsDiscount" className="form-label me-2">Is discount</label>
                            <input {...register('IsDiscount', {
                                required: false,
                            })} className="form-check-input" type="checkbox" value="" id="IsDiscount"/>
                        </div>
                        <div className="mt-2 mb-3 px-5">
                            <label htmlFor="CountInStorage" className="form-label">Count in storage</label>
                            <input {...register('CountInStorage', {
                                required: true,
                            })} className="form-control" type="number" id="CountInStorage"/>
                        </div>
                        <div className="mt-2 mb-3 px-5">
                            <label htmlFor="Thumbnail" className="form-label">Thumbnail</label>
                            <input accept="image/png, image/gif, image/jpeg" onChange={(e:any) => selectThumbnailHandler(e)} className="form-control" type="file" id="Thumbnail"/>
                        </div>
                        <div className="mt-2 mb-3 px-5">
                            <label htmlFor="Images" className="form-label">Images</label>
                            <input accept="image/png, image/gif, image/jpeg" multiple={true} onChange={(e:any) => selectImagesHandler(e)} className="form-control" type="file" id="Images"/>
                            <button onClick={(e:any) => {setImages([]);e.currentTarget.previousElementSibling.value = ""}} className={'btn btn-primary'}>Очистить</button>
                        </div>
                        <div className="mt-2 mb-3 px-5">
                            <label htmlFor="Description" className="form-label">Description</label>
                            <textarea {...register('Description', {
                                required: true,
                            })} id={'Description'} className={'form-control'}></textarea>
                        </div>
                        <div className={'px-5'}>
                            <button type="submit" className="btn btn-primary">Submit</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default NewWare;