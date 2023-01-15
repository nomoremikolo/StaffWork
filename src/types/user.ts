export interface IUser{
    id: number,
    username: string,
    name: string,
    surname: string,
    email: string,
    age: number | null,
    adress: string | null,
    permissions: number[],
    isActivated: boolean,
    role: string,
}
export interface IAuthorization{
    username: string,
    password: string,
}
export interface INewUser{
    username: string,
    name: string,
    surname: string,
    password: string,
    email: string,
    age: number | null,
    adress: string | null,
}