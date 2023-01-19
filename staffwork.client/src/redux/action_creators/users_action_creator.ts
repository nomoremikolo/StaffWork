import {AppDispatch} from "../store";
import {authorizationReducer} from "../reducers/authorizationReducer";
import axios from "axios";
import {GraphQlEndpoint} from "../../global_variables";
import {usersReducer} from "../reducers/usersReducer";
import {NotificationReducer} from "../reducers/NotificationReducer";
import {IUpdatedUser, IUpdateSelf} from "../../types/user";
const {SHOW_SUCCESS_MESSAGE, SHOW_ERROR_MESSAGE, SHOW_WARNING_MESSAGE, DEACTIVATE_MESSAGE} = NotificationReducer.actions

export const update_self_info = (userInfo: IUpdateSelf) => async (dispatch: AppDispatch) => {
    const graphqlQuery = {
        "query": `
            mutation updateSI{
              user{
                updateSelfInfo(user: {
                  name: ${userInfo.name != null ? `"${userInfo.name}"` : "null"},
                  surname: ${userInfo.surname != null ? `"${userInfo.surname}"` : "null"},,
                  username: ${userInfo.username != null ? `"${userInfo.username}"` : "null"},
                  email: ${userInfo.email != null ? `"${userInfo.email}"` : "null"},
                  age: ${userInfo.age != null ? `${userInfo.age}` : "null"},
                  adress: ${userInfo.adress != null ? `"${userInfo.adress}"` : "null"},
                }){
                  errors
                  statusCode
                  user{
                    id
                    username
                    name
                    surname
                    age
                    email
                    adress
                    role
                    isActivated
                    permissions
                  }
                }
              }
            }
        `,
    };
    dispatch(usersReducer.actions.GET_PERMISSIONS_LIST())
    try {
        const response = await axios({
            url: GraphQlEndpoint,
            method: 'post',
            withCredentials: true,
            headers: {
                "content-type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem('accessToken')}`,
            },
            data: graphqlQuery
        })
        let r = response.data.data.user.updateSelfInfo
        if (r.statusCode === 200){
            dispatch(SHOW_SUCCESS_MESSAGE("Successfully changed"))
        }else{
            dispatch(SHOW_ERROR_MESSAGE(r.errors))
        }
    } catch (e:any) {
        dispatch(SHOW_ERROR_MESSAGE(e))
        localStorage.removeItem('accessToken')
    }
}
export const fetch_all_permissions = () => async (dispatch: AppDispatch) => {
    const graphqlQuery = {
        "query": `
            query getAP{
              user{
                getAllPermissions{
                  errors
                  permissions{
                    id
                    name
                  }
                  statusCode
                }
              }
            }
        `,
    };
    dispatch(usersReducer.actions.GET_PERMISSIONS_LIST())
    try {
        const response = await axios({
            url: GraphQlEndpoint,
            method: 'post',
            withCredentials: true,
            headers: {
                "content-type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem('accessToken')}`,
            },
            data: graphqlQuery
        })
        let r = response.data.data.user.getAllPermissions
        if (r.statusCode === 200){
            dispatch(usersReducer.actions.GET_PERMISSIONS_LIST_SUCCESS(r.permissions))
        }else{
            dispatch(usersReducer.actions.GET_PERMISSIONS_LIST_ERROR(r.errors))
        }
    } catch (e) {
        dispatch(usersReducer.actions.GET_PERMISSIONS_LIST_ERROR(e))
        localStorage.removeItem('accessToken')
    }
}
export const fetch_all_users = (keyWords?: string) => async (dispatch: AppDispatch) => {
    const graphqlQuery = {
        "query": `
            query getAll {
              user {
                getAll(keyWords: ${keyWords != null ? `"${keyWords}"` : "null"}){
                  errors
                  statusCode
                  users {
                    id
                    username
                    name
                    surname
                    age
                    email
                    adress
                    role
                    isActivated
                    permissions
                  }
                }
              }
            }
        `,
    };
    dispatch(usersReducer.actions.GET_ALL_USERS())
    try {
        const response = await axios({
            url: GraphQlEndpoint,
            method: 'post',
            withCredentials: true,
            headers: {
                "content-type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem('accessToken')}`,
            },
            data: graphqlQuery
        })
        let r = response.data.data.user.getAll
        if (r.statusCode === 200){
            dispatch(usersReducer.actions.GET_ALL_USERS_SUCCESS(r.users))
        }else{
            dispatch(usersReducer.actions.GET_ALL_USERS_ERROR(r.errors))
        }
    } catch (e) {
        dispatch(usersReducer.actions.GET_ALL_USERS_ERROR(e))
        localStorage.removeItem('accessToken')
    }
}
export const fetch_user_by_id = (id: number) => async (dispatch: AppDispatch) => {
    const graphqlQuery = {
        "query": `
            query getUBI{
              user{
                getUserById(userId: ${id}){
                  errors
                  statusCode
                  user{
                    id
                    username
                    name
                    surname
                    age
                    email
                    adress
                    role
                    isActivated
                    permissions
                  }
                }
              }
            }
        `,
    };
    dispatch(usersReducer.actions.GET_USER_BY_ID())
    try {
        const response = await axios({
            url: GraphQlEndpoint,
            method: 'post',
            withCredentials: true,
            headers: {
                "content-type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem('accessToken')}`,
            },
            data: graphqlQuery
        })
        let r = response.data.data.user.getUserById
        if (r.statusCode === 200){
            dispatch(usersReducer.actions.GET_USER_BY_ID_SUCCESS(r.user))
        }else{
            dispatch(usersReducer.actions.GET_USER_BY_ID_ERROR(r.errors))
        }
    } catch (e) {
        dispatch(usersReducer.actions.GET_USER_BY_ID_ERROR(e))
        localStorage.removeItem('accessToken')
    }
}
export const update_user = (user: IUpdatedUser) => async (dispatch: AppDispatch) => {
    console.log(user)
    const graphqlQuery = {
        "query": `
            mutation updateU{
              user{
                updateUser(user: {
                  id: ${user.id},
                  name: ${user.name != null ? `"${user.name}"` : "null"}
                  surname: ${user.surname != null ? `"${user.surname}"` : "null"},
                  username: ${user.username != null ? `"${user.username}"` : "null"},
                  role: ${user.role != null ? `"${user.role}"` : "null"},
                  isActivated: ${user.isActivated ?? "null"},
                  age: ${user.age ?? "null"},
                  email: ${user.email != null ? `"${user.email}"` : "null"},
                  permissions: ${user.permissions != null && user.permissions != undefined ? `"${user.permissions.join(" ")}"` : "null"},
                  adress: ${user.adress != null ? `"${user.adress}"` : "null"}
                }){
                  errors
                  statusCode
                  user{
                    id
                    username
                    name
                    surname
                    age
                    email
                    adress
                    role
                    isActivated
                    permissions
                  }
                }
              }
            }
        `,
    };
    try {
        const response = await axios({
            url: GraphQlEndpoint,
            method: 'post',
            withCredentials: true,
            headers: {
                "content-type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem('accessToken')}`,
            },
            data: graphqlQuery
        })
        let r = response.data.data.user.updateUser
        console.log(r)
        if (r.statusCode === 200){
            dispatch(SHOW_SUCCESS_MESSAGE("Successfully updated!"))
        }else{
            dispatch(SHOW_ERROR_MESSAGE(r.errors))
        }
    } catch (e:any) {
        dispatch(SHOW_ERROR_MESSAGE(e.toString()))
        localStorage.removeItem('accessToken')
    }
}