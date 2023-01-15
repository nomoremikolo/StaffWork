import {combineReducers, configureStore} from "@reduxjs/toolkit";
import wareReducer from "./reducers/wareReducer";
import authorizationReducer from "./reducers/authorizationReducer";

const rootReducer = combineReducers({
    wareReducer,
    authorizationReducer
})
export const setupStore = () => {
    return configureStore({
        reducer: rootReducer,
    })
}

export type RootState = ReturnType<typeof rootReducer>
export type AppStore = ReturnType<typeof setupStore>
export type AppDispatch = AppStore['dispatch'];