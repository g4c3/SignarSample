import axios, { AxiosError, AxiosResponse } from 'axios';
axios.defaults.baseURL = 'http://localhost:5142';

/*
 * Functions
 */
export function getGroups(): Promise<IAllGroups> 
{            
    return get<IAllGroups>(`groups/getallgroups`);
}

export function createGroup(command: IGroupManagement): Promise<void> 
{
    return post('/groups/creategroup', command);
}

export function leaveGroup(command: IGroupManagement): Promise<void> 
{
    return post('/groups/leavegroup', command);
}
/*
 * Interfaces
 */

export interface IAllGroups {
    allGroups: string[]
}

export interface IGroupManagement {
    connectionId: string;
    groupName: string;
}
/* 
 * Framework 
 */

async function get<T>(url: string, params?: any, responseType?: 'blob'): Promise<T> {
    try {
        const response = await axios.get(url, { params, responseType });
        return response.data;
    }
    catch (error : any) {
        throw getException(error.response, error.data);
    }
}

async function post(url: string, data?: any, params?: any, headers?: any): Promise<void> {
    try {
        await axios.post(url, data, { params, headers });
    }
    catch (error : any) {

        const res = error.response;

        throw getException(error.response, res.data);
    }
}

function getException(response: any, errorBody: any ) {

    if (response
        && response.data
        && response.data.Type
        )
        return createTypedObject(response.data);

    throw new UnrecognizedErrorException(errorBody);
}

export class UnrecognizedErrorException {

    errorhandler: any;

    constructor(errorMessages: any) {
      this.errorhandler = errorMessages;
    }
}

function createTypedObject(rawObject: any) {
    const typedObject = eval(`new ${rawObject.Type}()`);
    assignProperties(typedObject, rawObject);
    return typedObject;
}

function assignProperties(target: any, source: any) {
    const keys = Object.keys(target);

    for (let i = 0; i < keys.length; i++) {
        const key = keys[i];
        target[key] = source[key];
    }
}