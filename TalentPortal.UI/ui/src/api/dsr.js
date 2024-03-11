// @ts-nocheck
import axiosInstance from './AxiosInstance.js';
import { API_CREATE_DSR, API_DSR, API_DSR_FINAL, API_DSR_LOG, API_RECEIVED_DSR, API_SENT_DSR, API_TODAY_DSR } from './config.js';

export const getReceivedDsr = async () => {
    const response = await axiosInstance.get(API_RECEIVED_DSR);
    return response;
}

export const getSentDsr = async () => {
    const response = await axiosInstance.get(API_SENT_DSR);
    return response;
}

export const getTodayDsr = async () => {
    const response = await axiosInstance.get(API_TODAY_DSR);
    return response;
}

export const createDsr = async (dsr) => {
    const response = await axiosInstance.post(API_CREATE_DSR, dsr);
    return response;
}

export const updateDsr = async (dsr) => {
    const response = await axiosInstance.put(API_UPDATE_DSR, dsr);
    return response;
}

export const getDsrById = async (id) => {
    const response = await axiosInstance.get(API_DSR + '/' + id);
    return response;
}
export const submitDsr = async (data) => {
    const response = await axiosInstance.post(API_DSR_FINAL, data);
    return response;
}
export const getDsrByLogId = async (logId) => {
    const response = await axiosInstance.get(API_DSR_LOG + '/' + logId);
    return response;
}