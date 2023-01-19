import {AppDispatch} from "../store";
import {authorizationReducer} from "../reducers/authorizationReducer";
import axios from "axios";
import {GraphQlEndpoint} from "../../global_variables";
import {INewUser} from "../../types/user";

export const login = (username: string, password: string) => async (dispatch: AppDispatch) => {
    const graphqlQuery = {
        "query": `
            query login{
                authorization{
                  login(userLogin: {
                    username: "${username}"
                    password: "${password}"
                  }){
                    errors
                    refreshToken
                    statusCode
                    token
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
    dispatch(authorizationReducer.actions.LOGIN())
    try {
        const response = await axios({
            url: GraphQlEndpoint,
            method: 'post',
            withCredentials: true,
            headers: {
                "content-type": "application/json",
            },
            data: graphqlQuery
        })
        let r = response.data.data.authorization.login
        if (r.statusCode === 200){
            dispatch(authorizationReducer.actions.LOGIN_SUCCESS(r.user))
            localStorage.setItem('accessToken', r.token)
        }else{
            dispatch(authorizationReducer.actions.LOGIN_ERROR(r.errors))
        }
    } catch (e) {
        dispatch(authorizationReducer.actions.LOGIN_ERROR(e))
        localStorage.removeItem('accessToken')
    }
}

export const refresh_token = () => async (dispatch: AppDispatch) => {
    try{
        const graphqlQuery = {
            "query": `
                mutation refresh{
                  authorization{
                    refreshToken{
                      errors
                      refreshToken
                      statusCode
                      token
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
            `
        }
        dispatch(authorizationReducer.actions.REFRESH_TOKEN())
        let response = await axios({
            url: GraphQlEndpoint,
            method: 'post',
            withCredentials: true,
            headers: {
                "content-type": "application/json",
            },
            data: graphqlQuery
        })

        let r = response.data.data.authorization.refreshToken
        if (r.statusCode === 200){
            dispatch(authorizationReducer.actions.REFRESH_TOKEN_SUCCESS(r.user))
            localStorage.setItem('accessToken', r.token)
        }else{
            dispatch(authorizationReducer.actions.REFRESH_TOKEN_ERROR(r.errors))
            localStorage.removeItem('accessToken')
        }
    }
    catch (e) {
        console.log(e)
    }
}

export const sign_out = () => async (dispatch: AppDispatch) => {
    try{
        const graphqlQuery = {
            "query": `
                mutation sign_out{
                  authorization{
                    signOut{
                      errors
                      statusCode
                    }
                  }
                }
            `
        }
        dispatch(authorizationReducer.actions.SIGN_OUT())
        let response = await axios({
            url: GraphQlEndpoint,
            method: 'post',
            withCredentials: true,
            headers: {
                "content-type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem('accessToken')}`,
            },
            data: graphqlQuery
        })
        let r = response.data.data.authorization.signOut
        if (r.statusCode === 200){
            dispatch(authorizationReducer.actions.SIGN_OUT_SUCCESS())
            localStorage.removeItem('accessToken')
        }else{
            dispatch(authorizationReducer.actions.SIGN_OUT_ERROR(r.errors))
        }
    }
    catch (e){
        console.log(e)
    }
}

export const sign_in = (user: INewUser) => async (dispatch: AppDispatch) => {
    try{
        const graphqlQuery = {
            "query": `
                mutation reg{
                  user{
                    signIn(user: {
                      name: "${user.name}",
                      surname: "${user.surname}",
                      age: ${user.age},
                      adress: "${user.adress}",
                      email: "${user.email}"
                      password: "${user.password}",
                      username: "${user.username}",
                    }){
                      errors
                      statusCode
                      refreshToken
                      token
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

            `
        }
        dispatch(authorizationReducer.actions.SIGN_IN())
        let response = await axios({
            url: GraphQlEndpoint,
            method: 'post',
            withCredentials: true,
            headers: {
                "content-type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem('accessToken')}`,
            },
            data: graphqlQuery
        })
        let r = response.data.data.user.signIn
        if (r.statusCode === 201){
            dispatch(authorizationReducer.actions.SIGN_IN_SUCCESS(r.user))
            localStorage.setItem('accessToken', r.token)
        }else{
            dispatch(authorizationReducer.actions.SIGN_IN_ERROR(r.errors))
            localStorage.removeItem('accessToken')
        }
    }
    catch (e){
        console.log(e)
    }
}
