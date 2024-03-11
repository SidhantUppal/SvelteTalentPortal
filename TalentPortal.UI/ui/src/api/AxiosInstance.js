// @ts-nocheck
import axios from 'axios';
import { API_URL, AUTH_LOCAL_STORAGE_KEY } from './config';

let token = '';
const talent_user = JSON.parse(localStorage.getItem(AUTH_LOCAL_STORAGE_KEY));
if (talent_user) {
    token = talent_user?.accessToken;
}

const axiosInstance = axios.create({
    // baseURL : baseURL,
    baseURL: process.env.NODE_ENV === 'development' ? API_URL : API_URL,
    headers: {
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': '*',
        Authorization: 'Bearer ' + token,
    }
});

axiosInstance.interceptors.response.use((response) => response, (error) => {
    if (error.response.status === 403) {
        localStorage.removeItem(AUTH_LOCAL_STORAGE_KEY)
        window.location.reload();
    }
});

export default axiosInstance;

export const axiosInstanceMultipart = axios.create({

    baseURL: process.env.NODE_ENV === 'development' ? API_URL : API_URL,
    headers: {
        // error of boundary
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'POST, GET, OPTIONS',
        Authorization: 'Bearer ' + token,
        'Content-Type': 'multipart/form-data',
    },
});
export const getAxiosInstance = () => {
    const source = axios.CancelToken.source();
    let token = '';
    const talent_user = JSON.parse(localStorage.getItem(AUTH_LOCAL_STORAGE_KEY));
    if (talent_user) {
        token = talent_user?.accessToken;
    }
    return axios.create({
        baseURL: process.env.NODE_ENV === 'development' ? API_URL : API_URL,
        headers: {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'POST, GET, OPTIONS',
            "Authorization": 'Bearer ' + token,
        },
        cancelToken: source.token,
    });
}