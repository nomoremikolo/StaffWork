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
export interface IUpdatedUser{
    id: number,
    username: string | null,
    name: string | null,
    surname: string | null,
    email: string | null,
    age: number | null,
    adress: string | null,
    permissions: number[] | null,
    isActivated: boolean | null,
    role: string | null,
}

export interface IUpdateSelf {
    username: string | null,
    name: string | null,
    surname: string | null,
    email: string | null,
    age: number | null,
    adress: string | null,
}