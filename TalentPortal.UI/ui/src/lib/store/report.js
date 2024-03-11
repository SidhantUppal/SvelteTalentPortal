// @ts-nocheck

import { writable } from "svelte/store";
import { getSentReport,getReceivedReport } from "../../api/report_api";



export const sentReports = writable([]);
export const receivedReports = writable([]);

export const reportsLoading = writable(false);
export const reportError = writable(null);

// State Changers
const requestReports = () => {
	reportsLoading.set(true);
};

const receiveSentReportSuccess = (data) => {
    sentReports.set(data);
    reportsLoading.set(false);
};

const  receiveSentReportError = (error) => {
    reportError.set(error);
    reportsLoading.set(false);
};

const receiveReportSucess = (data) => {
    receivedReports.set(data);
    reportsLoading.set(false);
}
const receiveReportsError = (error) => {
    reportError.set(error);
    reportsLoading.set(false);
};

export const fetchSentReports = () => {
    requestReports();
    getSentReport()
        .then((response) => {
            receiveReports(response.data);
        })
        .catch((error) => {
            receiveReportsError(error);
        });
};

export const fetchReceivedReports = () => {
    requestReports();
    getReceivedReport()
        .then((response) => {
            receiveReportSucess(response.data.data);
        })
        .catch((error) => {
            receiveReportsError(error);
        });
};