import axiosInstance from "./AxiosInstance";
import {API_GET_ATTENDANCE, API_TIME_IN, API_TIME_OUT } from "./config";

export const getAttendance = async (params) => {
    let { id, fromDate, toDate } = params;
    const response = await axiosInstance.get(API_GET_ATTENDANCE + '?id=' + id + '&fromDate=' + fromDate + '&toDate=' + toDate);
    return response;
}
export const timeIn = async () => {
    const response = await axiosInstance.post(API_TIME_IN);
    return response;
}
export const timeOut = async () => {
    const response = await axiosInstance.post(API_TIME_OUT);
    return response;
}
export const getAttendancetoday = async () => {
    const response = await axiosInstance.get(API_GET_ATTENDANCE + '/today');
    return response;
}