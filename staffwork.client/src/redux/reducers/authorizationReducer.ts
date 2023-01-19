import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {IUser} from "../../types/user";

interface IAuthorizationReducerState {
    user: IUser | null,
    isLoading: boolean,
    error: any,
    isAuthorized: boolean,
}

const initialState: IAuthorizationReducerState = {
    user: null,
    error: "",
    isLoading: false,
    isAuthorized: false,
}

export const authorizationReducer = createSlice({
    name: "authorizationReducer",
    initialState: initialState,
    reducers: {
        LOGIN(state){
            state.error = ""
            state.isLoading = true
        },
        LOGIN_SUCCESS(state, action: PayloadAction<IUser>){
            state.error = ""
            state.isLoading = false
            state.isAuthorized = true
            state.user = action.payload
        },
        LOGIN_ERROR(state, action: PayloadAction<any>){
            state.error = action.payload
            state.isLoading = false
            state.isAuthorized = false
            state.user = null
        },
        SIGN_IN(state){
            state.isLoading = true
        },
        SIGN_IN_SUCCESS(state, action: PayloadAction<IUser>){
            state.isLoading = false
            state.isAuthorized = true
            state.user = action.payload
            state.error = ""
        },
        SIGN_IN_ERROR(state, action: PayloadAction<any>){
            state.isLoading = false
            state.error = action.payload
        },
        REFRESH_TOKEN(state){
            state.error = ""
            state.isLoading = true
        },
        REFRESH_TOKEN_SUCCESS(state, action: PayloadAction<IUser>){
            state.error = ""
            state.isLoading = false
            state.isAuthorized = true
            state.user = action.payload
            console.log(action.payload)
        },
        REFRESH_TOKEN_ERROR(state){
            state.isLoading = false
            state.isAuthorized = false
            state.user = null
        },
        SIGN_OUT(state){
            state.isLoading = true
        },
        SIGN_OUT_SUCCESS(state){
            state.isLoading = false
            state.isAuthorized = false
            state.user = null
            state.error = ""
        },
        SIGN_OUT_ERROR(state, action: PayloadAction<any>){
            state.isLoading = false
            state.error = action.payload
        },
    }
})

export default authorizationReducer.reducer