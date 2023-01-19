export interface IWare{
    id: number,
    wareId: number | null,
    name: string,
    brandId: number,
    categoryId: number,
    description: string,
    sizes: string,
    price: number,
    oldPrice: number,
    isDiscount: boolean,
    isFavorite: boolean | null | undefined,
    countInStorage: number,
    thumbnail: string | null,
    images: string | null,
}
export interface INewWare{
    name: string,
    brandId: number,
    categoryId: number,
    description: string,
    sizes: string,
    size?: string,
    price: number,
    oldPrice: number | null,
    isDiscount: boolean,
    countInStorage: number,
    thumbnail: string | ArrayBuffer | null,
    images: string | ArrayBuffer | null,
}
export interface ISortType{
    value: string,
    isReverse: boolean
}
export interface IGetByIdWare {
    id: number
    name: string
    brandId: number
    categoryId: number
    description: string
    sizes: string
    price: number
    oldPrice: number
    isDiscount: boolean
    countInStorage: number
    brandName: string
    categoryName: string
    phone: string
    countryManufactured: string
    thumbnail: string | null
    images: string | null
}
export interface IUpdateWareType {
    id: number
    name: string,
    brandId: number,
    categoryId: number,
    description: string,
    sizes: string,
    price: number,
    oldPrice: number,
    isDiscount: boolean,
    countInStorage: number,
    thumbnail: string | ArrayBuffer | null,
    images: string | ArrayBuffer | null,
}
export interface IOrderGraphType {
    isConfirmed: boolean
    orderId: number
    status: string
    orderWares: [{
        wareName: string
        categoryName: string
        brandName: string
        description: string
        sizes: string
        price: number
        oldPrice: number
        isDiscount: boolean
        countInStorage: number
        size: string
        wareId: number
        count: number
    }]
}