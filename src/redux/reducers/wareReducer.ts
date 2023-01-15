import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {IWare} from "../../types/ware";
import {ICategory} from "../../types/category";
import {IBrand} from "../../types/brand";

interface IWareReducerState{
    discountWares: IWare[],
    noveltyWares: IWare[],
    allWares: {
        errors: any,
        isLoading: boolean,
        wares: IWare[],
    }
    cartWares: any[],
    favoriteWares: IWare[],
    allCategories: {
        categories: ICategory[],
        isLoading: boolean,
        errors: any
    },
    allBrands: {
        brands: IBrand[],
        isLoading: boolean,
        errors: any
    },
    favorite: {
        statusCode: number,
        isLoading: boolean,
        error: any,
    }
    ware: IWare | null,
    isLoading: boolean,
    error: any,
}

const initialState: IWareReducerState = {
    discountWares: [],
    noveltyWares: [],
    favoriteWares: [],
    cartWares: [],
    allCategories: {
        categories: [],
        errors: null,
        isLoading: false
    },
    allBrands: {
      brands: [],
      errors: null,
      isLoading: false,
    },
    allWares: {
        wares: [],
        isLoading: false,
        errors: null,
    },
    favorite: {
        error: null,
        isLoading: false,
        statusCode: 0,
    },
    ware: null,
    isLoading: false,
    error: "",
}

export const wareReducer = createSlice({
    initialState: initialState,
    name: "userReducer",
    reducers: {
        GET_WARES(state){
            state.allWares.isLoading = true
            state.allWares.errors = ""
        },
        GET_WARES_SUCCESS(state, action: PayloadAction<IWare[]>){
            state.allWares.wares = action.payload
            state.allWares.isLoading = false
            state.allWares.errors = ""
        },
        GET_WARES_ERROR(state, action: PayloadAction<any>){
            state.allWares.isLoading = false
            state.allWares.errors = action.payload
        },
        GET_BRANDS(state){
            state.allBrands.isLoading = true
            state.allBrands.errors = ""
        },
        GET_BRANDS_SUCCESS(state, action: PayloadAction<IBrand[]>){
            state.allBrands.brands = action.payload
            state.allBrands.isLoading = false
            state.allBrands.errors = ""
        },
        GET_BRANDS_ERROR(state, action: PayloadAction<any>){
            state.allBrands.isLoading = false
            state.allBrands.errors = action.payload
        },
        GET_CATEGORIES(state){
            state.allCategories.isLoading = true
            state.allCategories.errors = ""
        },
        GET_CATEGORIES_SUCCESS(state, action: PayloadAction<ICategory[]>){
            console.log(action.payload)
            state.allCategories.categories = action.payload
            state.allCategories.isLoading = false
            state.allCategories.errors = ""
        },
        GET_CATEGORIES_ERROR(state, action: PayloadAction<any>){
            state.allCategories.isLoading = false
            state.allCategories.errors = action.payload
        },
        GET_DISCOUNT_WARES(state){
            state.isLoading = true
            state.error = ""
        },
        GET_DISCOUNT_WARES_SUCCESS(state, action: PayloadAction<IWare[]>){
            state.discountWares = action.payload
            state.isLoading = false
            state.error = ""
        },
        GET_DISCOUNT_WARES_ERROR(state, action: PayloadAction<any>){
            state.isLoading = false
            state.error = action.payload
        },
        GET_NOVELTY_WARES(state){
            state.isLoading = true
            state.error = ""
        },
        GET_NOVELTY_WARES_SUCCESS(state, action: PayloadAction<IWare[]>){
            state.noveltyWares = action.payload
            state.isLoading = false
            state.error = ""
        },
        GET_NOVELTY_WARES_ERROR(state, action: PayloadAction<any>){
            state.isLoading = false
            state.error = action.payload
        },
        GET_WARE_BY_ID(state){
            state.isLoading = true
            state.error = ""
        },
        GET_WARE_BY_ID_SUCCESS(state, action: PayloadAction<IWare>){
            state.ware = action.payload
            state.isLoading = false
            state.error = ""
        },
        GET_WARE_BY_ID_ERROR(state, action: PayloadAction<any>){
            state.isLoading = false
            state.error = action.payload
        },
        GET_FAVORITE_WARES(state){
            state.isLoading = true
            state.error = ""
        },
        GET_FAVORITE_WARES_SUCCESS(state, action: PayloadAction<IWare[]>){
            state.favoriteWares = action.payload
            state.isLoading = false
            state.error = ""
        },
        GET_FAVORITE_WARES_ERROR(state, action: PayloadAction<any>){
            state.isLoading = false
            state.error = action.payload
        },
        GET_CART_WARES(state){
            state.isLoading = true
            state.error = ""
        },
        GET_CART_WARES_SUCCESS(state, action: PayloadAction<IWare[]>){
            state.cartWares = action.payload
            state.isLoading = false
            state.error = ""
        },
        GET_CART_WARES_ERROR(state, action: PayloadAction<any>){
            state.isLoading = false
            state.error = action.payload
        },
        ADD_WARE_TO_FAVORITE(state){
            state.favorite.isLoading = true
        },
        ADD_WARE_TO_FAVORITE_SUCCESS(state){
            state.favorite.isLoading = false
            state.favorite.error = null
        },
        ADD_WARE_TO_FAVORITE_ERROR(state, action: PayloadAction<any>){
            state.favorite.isLoading = false
            state.favorite.error = action.payload
        },
        REMOVE_WARE_FROM_FAVORITE(state){
            state.favorite.isLoading = true
        },
        REMOVE_WARE_FROM_FAVORITE_SUCCESS(state){
            state.favorite.isLoading = false
            state.favorite.error = null
        },
        REMOVE_WARE_FROM_FAVORITE_ERROR(state, action: PayloadAction<any>){
            state.favorite.isLoading = false
            state.favorite.error = action.payload
        },
    }
})
export default wareReducer.reducer