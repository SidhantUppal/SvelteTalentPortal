// @ts-nocheck
import axiosInstance from './AxiosInstance.js';
import { API_CREATE_REPORT, API_RECEIVED_REPORT, API_REPORT, API_SENT_REPORT, API_UPDATE_REPORT } from './config.js';

export const getReceivedReport = async () => {
    const response = await axiosInstance.get(API_RECEIVED_REPORT);
    return response;
}

export const getSentReport = async () => {
    const response = await axiosInstance.get(API_SENT_REPORT);
    return response;
}

export const createReport = async (dsr) => {
    const response = await axiosInstance.post(API_CREATE_REPORT, dsr);
    return response;
}

export const updateReport = async (dsr) => {
    const response = await axiosInstance.put(API_UPDATE_REPORT, dsr);
    return response;
}

export const getReportById = async (id) => {
    const response = await axiosInstance.get(API_REPORT + '/' + id);
    return response;
}