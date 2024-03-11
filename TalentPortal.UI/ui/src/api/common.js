import axiosInstance from './AxiosInstance.js';
import { API_COMMON_LEAVE_TYPE, API_COMMON_PROJECT, API_COMMON_USER } from './config.js';
export const getProjects = async () => {
    const response = await axiosInstance.get(API_COMMON_PROJECT);
    return response;
}

export const getUsers = async () => {
    const response = await axiosInstance.get(API_COMMON_USER);
    return response;
}
export const getLeaveTypes = async () => {
    const response = await axiosInstance.get(API_COMMON_LEAVE_TYPE);
    return response;
}