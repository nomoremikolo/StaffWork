import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {IUser} from "../../types/user";

interface IUsersReducerState {
    allUsers: {
        isLoading: boolean
        errors: any,
        users: IUser[]
    },
    getUserById: {
        isLoading: boolean,
        errors: any,
        user: IUser | null
    },
    permissions: [{
        id: number,
        name: string
    }] | null
}

const initialState: IUsersReducerState = {
    allUsers: {
        errors: null,
        isLoading: false,
        users: []
    },
    getUserById: {
        errors: null,
        isLoading: false,
        user: null
    },
    permissions: null,
}

export const usersReducer = createSlice({
    name: "usersReducer",
    initialState: initialState,
    reducers: {
        GET_USER_BY_ID(state){
            state.getUserById.isLoading = true
            state.getUserById.errors = null
        },
        GET_USER_BY_ID_SUCCESS(state, action: PayloadAction<IUser>){
            state.getUserById.isLoading = false
            state.getUserById.errors = null
            state.getUserById.user = action.payload
        },
        GET_USER_BY_ID_ERROR(state, action: PayloadAction<any>){
            state.getUserById.errors = action.payload
            state.getUserById.isLoading = false
        },
        GET_ALL_USERS(state){
            state.allUsers.isLoading = true
            state.allUsers.errors = null
        },
        GET_ALL_USERS_SUCCESS(state, action: PayloadAction<IUser[]>){
            state.allUsers.isLoading = false
            state.allUsers.errors = null
            state.allUsers.users = action.payload
        },
        GET_ALL_USERS_ERROR(state, action: PayloadAction<any>){
            state.allUsers.errors = action.payload
            state.allUsers.isLoading = false
        },
        GET_PERMISSIONS_LIST(state){

        },
        GET_PERMISSIONS_LIST_SUCCESS(state, action: PayloadAction<[{id: number, name: string}]>){
            state.permissions = action.payload
        },
        GET_PERMISSIONS_LIST_ERROR(state, action: PayloadAction<any>){

        },
    }
})

export default usersReducer.reducer