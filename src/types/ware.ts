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
    price: number,
    oldPrice: number,
    isDiscount: boolean,
    countInStorage: number,
    thumbnail: string | ArrayBuffer | null,
    images: string | ArrayBuffer | null,
}
export interface ISortType{
    value: string,
    isReverse: boolean
}