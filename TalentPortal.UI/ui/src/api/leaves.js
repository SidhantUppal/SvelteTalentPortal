import axiosInstance from "./AxiosInstance";
import { API_LEAVE } from "./config";

export const getleaves = async () => {
    const response = await axiosInstance.get(API_LEAVE)
    return response;
}

export const addLeave = async (data) => {
    const response = await axiosInstance.post(API_LEAVE, data);
    return response;
}