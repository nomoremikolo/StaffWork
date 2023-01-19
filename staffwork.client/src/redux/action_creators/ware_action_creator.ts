import {AppDispatch} from "../store";
import {wareReducer} from "../reducers/wareReducer";
import axios from "axios";
import {GraphQlEndpoint} from "../../global_variables";
import {INewWare, ISortType, IUpdateWareType, IWare} from "../../types/ware";
import {INewBrand} from "../../types/brand";
import {NotificationReducer} from "../reducers/NotificationReducer";

const {SHOW_SUCCESS_MESSAGE, SHOW_ERROR_MESSAGE, SHOW_WARNING_MESSAGE, DEACTIVATE_MESSAGE} = NotificationReducer.actions

export const create_new_brand = (brand: INewBrand, callback: (statusCode: number) => void) => async (dispatch: AppDispatch) => {
    try {
        const graphqlQuery = {
            "query": `
                mutation createB{
                  ware{
                    createBrand(brand: {
                      name: "${brand.name}"
                      phone: "${brand.phone}"
                      countryManufactured: "${brand.countryManufactured}"
                    }){
                      brand{
                        id
                        name
                        phone
                        countryManufactured
                      }
                      errors
                      statusCode
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.createBrand
        r.statusCode === 200 ? dispatch(SHOW_SUCCESS_MESSAGE("Brand was successfully created!")) : dispatch(SHOW_ERROR_MESSAGE("An error occurred while adding the brand"))
        callback(r.statusCode)

    } catch (e) {
        console.log(e)
    }
}
export const update_order = (id: number, status?: string, isConfirmed?: boolean, callback?: () => void) => async (dispatch: AppDispatch) => {
    try {
        const graphqlQuery = {
            "query": `
                mutation updateO{
                  ware{
                    updateOrder(id: ${id},isConfirmed: ${isConfirmed ?? "null"}, status: ${status != null ? `"${status}"` : "null"}){
                      errors
                      statusCode
                      order{
                        id
                        isConfirmed
                        status
                        userId
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.updateOrder
        r.statusCode === 200 ? dispatch(SHOW_SUCCESS_MESSAGE("Order was successfully updated!")) : dispatch(SHOW_ERROR_MESSAGE("An error occurred while updating the order"))
        if(callback)
            callback()
    } catch (e) {
        console.log(e)
    }
}
export const get_orders = (confirmed?: boolean | null, orderNumber?: number | null) => async (dispatch: AppDispatch) => {
    dispatch(wareReducer.actions.GET_ORDERS())
    try {
        const graphqlQuery = {
            "query": `
                query getO{
                  ware{
                    getOrders(confirmed: ${confirmed === null ? "null" : confirmed}, orderNumber: ${orderNumber == null ? "null" : `"${orderNumber}"`}){
                      errors
                      statusCode
                      wares{
                        isConfirmed
                        orderId
                        status
                        orderWares{
                          wareName
                          categoryName
                          brandName
                          description
                          sizes
                          price
                          oldPrice
                          isDiscount
                          countInStorage
                          size
                          wareId
                          count
                        }
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.getOrders
        if(r.statusCode === 200){
            dispatch(wareReducer.actions.GET_ORDERS_SUCCESS(r.wares))
            dispatch(wareReducer.actions.GET_ORDERS_ERROR(r.errors))

        }
    } catch (e) {
        dispatch(wareReducer.actions.GET_ORDERS_ERROR(e))
        console.log(e)
    }
}
export const update_ware = (ware: IUpdateWareType, callback: (statusCode: number) => void) => async (dispatch: AppDispatch) => {
    try {
        const graphqlQuery = {
            "query": `
                mutation updateW{
                  ware{
                    updateWare(ware: {
                      id: ${ware.id},
                      name: "${ware.name}",
                      brandId: ${ware.brandId},
                      categoryId: ${ware.categoryId},
                      countInStorage: ${ware.countInStorage},
                      description: """${ware.description}""",
                      price: ${ware.price},
                      oldPrice: ${ware.oldPrice},
                      isDiscount: ${ware.isDiscount},
                      sizes: "${ware.sizes}",
                      thumbnail: ${ware.thumbnail === "" ? "null" : `"""${ware.thumbnail}"""`}
                      images: ${ware.images === "" ? "null" : `"""${ware.images}"""`}
                    }){
                      errors
                      statusCode
                    }
                  }
                }
            `
        }
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

        let r = response.data.data.ware.updateWare
        r.statusCode === 200 ? dispatch(SHOW_SUCCESS_MESSAGE("Ware was successfully updated!")) : dispatch(SHOW_ERROR_MESSAGE("An error occurred while updating the ware"))

        callback(r.statusCode)
    } catch (e) {
        console.log(e)
    }
}
export const create_new_category = (name: string, callback: (statusCode: number) => void) => async (dispatch: AppDispatch) => {
    try {
        const graphqlQuery = {
            "query": `
                mutation createC{
                  ware{
                    createCategory(categoryName: "${name}"){
                      category{
                        id
                        name
                      },
                      errors,
                      statusCode
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.createCategory
        r.statusCode === 200 ? dispatch(SHOW_SUCCESS_MESSAGE("Category was successfully created!")) : dispatch(SHOW_ERROR_MESSAGE("An error occurred while creating the category"))
        callback(r.statusCode)
    } catch (e) {
        console.log(e)
    }
}
export const confirmOrder = (callback: () => void) => async (dispatch: AppDispatch) => {
    try {
        const graphqlQuery = {
            "query": `
                mutation confirmO {
                  ware {
                    confirmOrder {
                      errors
                      statusCode
                      wares {
                        basketId
                        count
                          id
                          name
                          brandId
                          categoryId
                          description
                          sizes
                          price
                          oldPrice
                          isDiscount
                          countInStorage
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.confirmOrder
        r.statusCode === 200 ? dispatch(SHOW_SUCCESS_MESSAGE("Order was successfully confirmed, wait for accepting!")) : dispatch(SHOW_ERROR_MESSAGE("An error occurred while confirming the order"))
        callback()
    } catch (e) {
        console.log(e)
    }
}
export const delete_ware = (id: number, callback: (statusCode: number) => void) => async (dispatch: AppDispatch) => {
    try {
        const graphqlQuery = {
            "query": `
                mutation deleteW{
                  ware{
                    deleteWare(wareId: ${id}){
                      errors
                      statusCode
                      ware{
                        name
                        brandId
                        categoryId
                        description
                        sizes
                        price
                        oldPrice
                        isDiscount
                        countInStorage
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.deleteWare
        r.statusCode === 200 ? dispatch(SHOW_SUCCESS_MESSAGE("Ware was successfully deleted!")) : dispatch(SHOW_ERROR_MESSAGE("An error occurred while deleting the ware"))
        callback(r.statusCode)
    } catch (e) {
        console.log(e)
    }
}

export const delete_category = (id: number, callback: (statusCode: number) => void) => async (dispatch: AppDispatch) => {
    try {
        const graphqlQuery = {
            "query": `
                mutation deleteC{
                  ware{
                    deleteCategory(categoryId: ${id}){
                      statusCode
                      errors
                      category{
                        id
                        name
                      },
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.deleteCategory
        r.statusCode === 200 ? dispatch(SHOW_SUCCESS_MESSAGE("Category was successfully deleted!")) : dispatch(SHOW_ERROR_MESSAGE("An error occurred while deleting the category"))
        callback(r.statusCode)
    } catch (e) {
        console.log(e)
    }
}
export const create_ware = (ware: INewWare, callback: () => void) => async (dispatch: AppDispatch) => {
    try {
        const graphqlQuery = {
            "query": `
                mutation newWare {
                  ware {
                    createWare(
                      ware: {
                        name: "${ware.name}"
                        brandId: ${ware.brandId}
                        categoryId: ${ware.categoryId}
                        countInStorage: ${ware.countInStorage}
                        isDiscount: ${ware.isDiscount}
                        oldPrice: ${ware.oldPrice != null ? ware.oldPrice : "null"}
                        price: ${ware.price}
                        sizes: "${ware.sizes}"
                        description: """${ware.description}"""
                        thumbnail: """${ware.thumbnail}"""
                        images: """${ware.images}"""
                      }
                    ) {
                      errors
                      statusCode
                      ware {
                        name
                        brandId
                        categoryId
                        description
                        sizes
                        price
                        oldPrice
                        isDiscount
                        countInStorage
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.createWare
        r.statusCode === 200 ? dispatch(SHOW_SUCCESS_MESSAGE("Ware was successfully created!")) : dispatch(SHOW_ERROR_MESSAGE("An error occurred while adding the brand"))
        if ( r.statusCode === 200)
            callback()
    } catch (e) {
        console.log(e)
    }
}
export const clearBasket = (callback?: () => void) => async (dispatch: AppDispatch) => {
    try {
        const graphqlQuery = {
            "query": `
                mutation clearB{
                  ware{
                    clearCart{
                      errors
                      statusCode
                      ware{
                        id
                        name
                        brandId
                        categoryId
                        description
                        sizes
                        price
                        oldPrice
                        isDiscount
                        countInStorage
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.clearCart
        r.statusCode === 200 ? dispatch(SHOW_SUCCESS_MESSAGE("Cart was successfully cleared!")) : dispatch(SHOW_ERROR_MESSAGE("An error occurred while clearing the cart"))
        if(callback)
            callback()
    } catch (e) {
        console.log(e)
    }
}
export const add_to_cart = (id: number, size: string) => async (dispatch: AppDispatch) => {
    try {
        const graphqlQuery = {
            "query": `
                mutation addWTB {
                  ware {
                    addWareToBasket(ware: { count: 1, wareId: ${id}, size: "${size}" }) {
                      errors
                      statusCode
                      ware {
                        basketId
                        count
                        id
                        name
                        brandId
                        categoryId
                        description
                        sizes
                        size
                        price
                        oldPrice
                        isDiscount
                        countInStorage
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.addWareToBasket
        r.statusCode === 200 ? dispatch(SHOW_SUCCESS_MESSAGE("Ware was successfully added to you cart!")) : dispatch(SHOW_ERROR_MESSAGE("An error occurred while adding ware to your cart"))
    } catch (e) {
        console.log(e)
    }
}
export const changeBasketWareCount = (WareId: number, Count: number) => async (dispatch: AppDispatch) => {
    try {
        const graphqlQuery = {
            "query": `
                mutation changeBWC{
                  ware{
                    changeCount(ware: {
                      count: ${Count},
                      wareId: ${WareId}
                    }){
                      errors
                      statusCode
                      ware{
                        id
                        name
                        brandId
                        categoryId
                        description
                        sizes
                        price
                        oldPrice
                        isDiscount
                        countInStorage
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.addWareToBasket
    } catch (e) {
        console.log(e)
    }
}
export const removeFromBasket = (WareId: number) => async (dispatch: AppDispatch) => {
    try {
        const graphqlQuery = {
            "query": `
                mutation removeWFB {
                  ware {
                    removeWareFromBasket(wareId: ${WareId}) {
                      errors
                      statusCode
                      ware {
                         basketId
                        count
                        id
                        name
                        brandId
                        categoryId
                        description
                        sizes
                        price
                        oldPrice
                        isDiscount
                        countInStorage
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.removeFromBasket
        r.statusCode === 200 ? dispatch(SHOW_SUCCESS_MESSAGE("Ware was successfully deleted from cart!")) : dispatch(SHOW_ERROR_MESSAGE("An error occurred while deleting ware from cart"))
    } catch (e) {
        console.log(e)
    }
}
interface IFetch_discount_wares {
    sortBy?: ISortType | null,
    categoryId?: number | null,
    countOfRecords?: number | null
    keyWords?: string | null,
}
export const fetch_discount_wares_authorized = (settings?: IFetch_discount_wares) => async (dispatch: AppDispatch) => {
    dispatch(wareReducer.actions.GET_DISCOUNT_WARES())
    try {
        const graphqlQuery = {
            "query": `
                query getaware{
                  ware{
                    getAllWaresAuthorized(settings: {
                      categoryId: ${settings?.categoryId ?? "null"},
                      countOfRecords: ${settings?.countOfRecords ?? 20},
                      sortParam: {
                        isReverse: ${settings?.sortBy?.isReverse ?? "false"},
                        value: "${settings?.sortBy?.value ?? "name"}",
                        },
                      filter: "Discount"
                      keyWords: ${settings?.keyWords == null ? null : `"${settings?.keyWords}"`}
                    }){
                      errors
                      statusCode
                      wares{
                        id
                        name
                        brandId
                        categoryId
                        description
                        sizes
                        price
                        oldPrice
                        isDiscount
                        isFavorite  
                        countInStorage
                        thumbnail
                        images
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.getAllWaresAuthorized
        if (r.statusCode === 200) {
            dispatch(wareReducer.actions.GET_DISCOUNT_WARES_SUCCESS(r.wares))
        } else {
            dispatch(wareReducer.actions.GET_DISCOUNT_WARES_ERROR(r.errors))
        }

    } catch (e) {
        dispatch(wareReducer.actions.GET_DISCOUNT_WARES_ERROR(e))
        console.log(e)
    }
}
export const fetch_novelty_wares_authorized = (settings?: IFetch_discount_wares) => async (dispatch: AppDispatch) => {
    dispatch(wareReducer.actions.GET_NOVELTY_WARES())
    try {
        const graphqlQuery = {
            "query": `
                query getaware{
                  ware{
                    getAllWaresAuthorized(settings: {
                      categoryId: ${settings?.categoryId ?? "null"},
                      countOfRecords: ${settings?.countOfRecords ?? 20},
                      sortParam: {
                        isReverse: ${settings?.sortBy?.isReverse ?? "false"},
                        value: "${settings?.sortBy?.value ?? "name"}",
                        },
                      filter: "Novelty"
                      keyWords: ${settings?.keyWords == null ? null : `"${settings?.keyWords}"`}
                    }){
                      errors
                      statusCode
                      wares{
                        id
                        name
                        brandId
                        categoryId
                        description
                        sizes
                        price
                        oldPrice
                        isDiscount
                        isFavorite  
                        countInStorage
                        thumbnail
                        images
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.getAllWaresAuthorized
        if (r.statusCode === 200) {
            dispatch(wareReducer.actions.GET_NOVELTY_WARES_SUCCESS(r.wares))
        } else {
            dispatch(wareReducer.actions.GET_NOVELTY_WARES_ERROR(r.errors))
        }

    } catch (e) {
        dispatch(wareReducer.actions.GET_NOVELTY_WARES_ERROR(e))
        console.log(e)
    }
}
export const fetch_cart_wares = () => async (dispatch: AppDispatch) => {
    dispatch(wareReducer.actions.GET_CART_WARES())
    try {
        const graphqlQuery = {
            "query": `
                query getWFB {
                  ware {
                    getWaresFromBasket {
                      errors
                      statusCode
                      wares {
                        basketId
                        count
                        id
                        name
                        brandId
                        categoryId
                        description
                        sizes
                        size
                        price
                        oldPrice
                        isDiscount
                        countInStorage
                        thumbnail
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.getWaresFromBasket
        if (r.statusCode === 200) {
            dispatch(wareReducer.actions.GET_CART_WARES_SUCCESS(r.wares))
        } else {
            dispatch(wareReducer.actions.GET_CART_WARES_ERROR(r.errors))
        }

    } catch (e) {
        dispatch(wareReducer.actions.GET_CART_WARES_ERROR(e))
        console.log(e)
    }
}
export const fetch_favorite_wares = () => async (dispatch: AppDispatch) => {
    dispatch(wareReducer.actions.GET_FAVORITE_WARES())
    try {
        const graphqlQuery = {
            "query": `
            query getfwares {
              ware {
                getFavoriteWares {
                  errors
                  statusCode
                  wares {
                    wareId
                    favoriteId
                    name
                    brandId
                    categoryId
                    description
                    sizes
                    price
                    oldPrice
                    isDiscount
                    countInStorage
                    thumbnail
                  }
                }
              }
            }
            `
        }
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
        let r = response.data.data.ware.getFavoriteWares
        if (r.statusCode === 200) {
            dispatch(wareReducer.actions.GET_FAVORITE_WARES_SUCCESS(r.wares))
        } else {
            dispatch(wareReducer.actions.GET_FAVORITE_WARES_ERROR(r.errors))
        }

    } catch (e) {
        dispatch(wareReducer.actions.GET_FAVORITE_WARES_ERROR(e))
        console.log(e)
    }
}
export const fetch_all_categories = () => async (dispatch: AppDispatch) => {
    dispatch(wareReducer.actions.GET_CATEGORIES())
    try {
        const graphqlQuery = {
            "query": `
                query GetAC{
                  ware{
                    getAllCategories{
                      errors
                      statusCode
                      categories{
                        id
                        name
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.getAllCategories

        if (r.statusCode === 200) {
            dispatch(wareReducer.actions.GET_CATEGORIES_SUCCESS(r.categories))
        } else {
            dispatch(wareReducer.actions.GET_CATEGORIES_ERROR(r.errors))
        }

    } catch (e) {
        dispatch(wareReducer.actions.GET_CATEGORIES_ERROR(e))
        console.log(e)
    }
}
export const fetch_all_brands = () => async (dispatch: AppDispatch) => {
    dispatch(wareReducer.actions.GET_BRANDS())
    try {
        const graphqlQuery = {
            "query": `
                query getAB{
                  ware{
                    getAllBrands{
                      statusCode
                      errors
                      brands{
                        id
                        name
                        countryManufactured
                        phone
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.getAllBrands

        if (r.statusCode === 200) {
            dispatch(wareReducer.actions.GET_BRANDS_SUCCESS(r.brands))
        } else {
            dispatch(wareReducer.actions.GET_BRANDS_ERROR(r.errors))
        }

    } catch (e) {
        dispatch(wareReducer.actions.GET_BRANDS_ERROR(e))
        console.log(e)
    }
}
export const fetch_discount_wares = (settings: IFetch_discount_wares) => async (dispatch: AppDispatch) => {
    dispatch(wareReducer.actions.GET_DISCOUNT_WARES())
    try {
        const graphqlQuery = {
            "query": `
                query getaware{
                  ware{
                    getAllWares(settings: {
                      categoryId: ${settings.categoryId ?? "null"},
                      countOfRecords: ${settings.countOfRecords ?? 20},
                      sortParam: {
                        isReverse: ${settings.sortBy?.isReverse ?? "false"},
                        value: "${settings.sortBy?.value ?? "name"}",
                        }
                      filter: "Discount"
                       keyWords: ${settings?.keyWords == null ? null : `"${settings?.keyWords}"`}
                    }){
                      errors
                      statusCode
                      wares{
                        id
                        name
                        brandId
                        categoryId
                        description
                        sizes
                        price
                        oldPrice
                        isDiscount
                        countInStorage
                        thumbnail
                        images
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.getAllWares
        if (r.statusCode === 200) {
            dispatch(wareReducer.actions.GET_DISCOUNT_WARES_SUCCESS(r.wares))
        } else {
            dispatch(wareReducer.actions.GET_DISCOUNT_WARES_ERROR(r.errors))
        }

    } catch (e) {
        dispatch(wareReducer.actions.GET_DISCOUNT_WARES_ERROR(e))
        console.log(e)
    }
}

interface IFetch_all_wares {
    sortBy?: ISortType | null,
    categoryId?: number | null,
    countOfRecords?: number | null
    keyWords?: string | null
    filter?: string | null
}

export const fetch_all_wares = (settings?: IFetch_all_wares) => async (dispatch: AppDispatch) => {
    dispatch(wareReducer.actions.GET_WARES())
    try {
        const graphqlQuery = {
            "query": `
                query getaware{
                  ware{
                    getAllWares(settings: {
                      categoryId: ${settings?.categoryId ?? "null"},
                      countOfRecords: ${settings?.countOfRecords ?? 20},
                      sortParam: {
                        isReverse: ${settings?.sortBy?.isReverse ?? "false"},
                        value: "${settings?.sortBy?.value ?? "name"}",
                      }
                      filter: ${settings?.filter === undefined ? "null" : `"${settings?.filter}"`}
                      keyWords: ${settings?.keyWords == null ? null : `"${settings?.keyWords}"`}
                    }){
                      errors
                      statusCode
                      wares{
                        id
                        name
                        brandId
                        categoryId
                        description
                        sizes
                        price
                        oldPrice
                        isDiscount
                        countInStorage
                        thumbnail  
                        images
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.getAllWares
        if (r.statusCode === 200) {
            dispatch(wareReducer.actions.GET_WARES_SUCCESS(r.wares))
        } else {
            dispatch(wareReducer.actions.GET_WARES_ERROR(r.errors))
        }

    } catch (e) {
        dispatch(wareReducer.actions.GET_WARES_ERROR(e))
        console.log(e)
    }
}

export const fetch_all_wares_with_favorites = (settings: IFetch_all_wares) => async (dispatch: AppDispatch) => {
    dispatch(wareReducer.actions.GET_WARES())
    try {
        const graphqlQuery = {
            "query": `
                query getAWA    {
                  ware{
                    getAllWaresAuthorized(settings: {
                      categoryId: ${settings.categoryId ?? "null"},
                      countOfRecords: ${settings.countOfRecords ?? 20},
                      sortParam: {
                        isReverse: ${settings.sortBy?.isReverse ?? "false"},
                        value: "${settings.sortBy?.value ?? "name"}",
                      }
                    }){
                      errors
                      statusCode
                      wares {
                        id
                        isFavorite
                        name
                        brandId
                        categoryId
                        description
                        sizes
                        price
                        oldPrice
                        isDiscount
                        countInStorage
                        thumbnail
                        images
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.getAllWares
        if (r.statusCode === 200) {
            dispatch(wareReducer.actions.GET_WARES_SUCCESS(r.wares))
        } else {
            dispatch(wareReducer.actions.GET_WARES_ERROR(r.errors))
        }

    } catch (e) {
        dispatch(wareReducer.actions.GET_WARES_ERROR(e))
        console.log(e)
    }
}

export const fetch_ware_by_id = (id: number) => async (dispatch: AppDispatch) => {
    dispatch(wareReducer.actions.GET_WARE_BY_ID())
    try {
        const graphqlQuery = {
            "query": `
                query getwarebi {
                  ware {
                    getWareById(wareId: ${id}) {
                      errors
                      statusCode
                      ware {
                        id
                        name
                        brandId
                        categoryId
                        description
                        sizes
                        price
                        oldPrice
                        isDiscount
                        countInStorage
                        brandName
                        categoryName
                        phone
                        countryManufactured
                        thumbnail
                        images
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.getWareById
        if (r.statusCode === 200) {
            dispatch(wareReducer.actions.GET_WARE_BY_ID_SUCCESS(r.ware))
        } else {
            dispatch(wareReducer.actions.GET_WARE_BY_ID_ERROR(r.errors))
        }

    } catch (e) {
        dispatch(wareReducer.actions.GET_WARE_BY_ID_ERROR(e))
        console.log(e)
    }
}

interface IFetch_novelty_wares {
    sortBy?: ISortType | null,
    categoryId?: number | null,
    countOfRecords?: number | null,
    filter?: string | null,
    keyWords?: string | null
}

export const fetch_novelty_wares = (settings: IFetch_novelty_wares) => async (dispatch: AppDispatch) => {
    dispatch(wareReducer.actions.GET_NOVELTY_WARES())
    try {
        const graphqlQuery = {
            "query": `
                query getaware{
                  ware{
                    getAllWares(settings: {
                      categoryId: ${settings.categoryId ?? "null"},
                      countOfRecords: ${settings.countOfRecords ?? 20},
                      sortParam: {
                        isReverse: ${settings.sortBy?.isReverse ?? "false"},
                        value: "${settings.sortBy?.value ?? "name"}",
                        }
                      filter: "Novelty"
                      keyWords: ${settings?.keyWords == null ? null : `"${settings?.keyWords}"`}
                    }){
                      errors
                      statusCode
                      wares{
                        id
                        name
                        brandId
                        categoryId
                        description
                        sizes
                        price
                        oldPrice
                        isDiscount
                        countInStorage
                        thumbnail
                        images
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.getAllWares
        if (r.statusCode === 200) {
            dispatch(wareReducer.actions.GET_NOVELTY_WARES_SUCCESS(r.wares))
        } else {
            dispatch(wareReducer.actions.GET_NOVELTY_WARES_ERROR(r.errors))
        }

    } catch (e) {
        dispatch(wareReducer.actions.GET_NOVELTY_WARES_ERROR(e))
        console.log(e)
    }
}

export const add_to_favorite = (id: number) => async (dispatch: AppDispatch) => {
    dispatch(wareReducer.actions.ADD_WARE_TO_FAVORITE())
    try {
        const graphqlQuery = {
            "query": `
                mutation addfav{
                  ware{
                    addToFavorite(wareId: ${id}){
                      errors
                      statusCode
                      ware{
                        name
                        brandId
                        categoryId
                        description
                        sizes
                        price
                        oldPrice
                        isDiscount
                        countInStorage
                        
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.addToFavorite
        r.statusCode === 200 ? dispatch(SHOW_SUCCESS_MESSAGE("Successfully added to favorite wares!")) : dispatch(SHOW_ERROR_MESSAGE("An error occurred while adding to favorite"))
        if (r.statusCode === 200) {
            dispatch(wareReducer.actions.ADD_WARE_TO_FAVORITE_SUCCESS())
        } else {
            dispatch(wareReducer.actions.ADD_WARE_TO_FAVORITE_ERROR(r.errors))
        }
    } catch (e) {
        dispatch(wareReducer.actions.ADD_WARE_TO_FAVORITE_ERROR(e))
        console.log(e)
    }
}

export const remove_from_favorite = (id: number, callback?: () => void) => async (dispatch: AppDispatch) => {
    dispatch(wareReducer.actions.ADD_WARE_TO_FAVORITE())
    try {
        const graphqlQuery = {
            "query": `
                mutation removeFF {
                  ware {
                    removeFromFavorite(wareId: ${id}) {
                      errors
                      statusCode
                      ware {
                        name
                        brandId
                        categoryId
                        description
                        sizes
                        price
                        oldPrice
                        isDiscount
                        countInStorage
                      }
                    }
                  }
                }
            `
        }
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
        let r = response.data.data.ware.removeFromFavorite
        if (r.statusCode === 200) {
            r.statusCode === 200 ? dispatch(SHOW_SUCCESS_MESSAGE("Ware was successfully deleted from favorites!")) : dispatch(SHOW_ERROR_MESSAGE("An error occurred while deleting the ware from favorites"))
            dispatch(wareReducer.actions.REMOVE_WARE_FROM_FAVORITE_SUCCESS())
            if (callback){
                callback()
            }
        } else {
            dispatch(wareReducer.actions.REMOVE_WARE_FROM_FAVORITE_ERROR(r.errors))
        }
    } catch (e) {
        dispatch(wareReducer.actions.REMOVE_WARE_FROM_FAVORITE_ERROR(e))
        console.log(e)
    }
}