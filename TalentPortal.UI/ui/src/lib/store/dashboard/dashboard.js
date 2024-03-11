// @ts-nocheck

import { writable } from "svelte/store";
import { getAttendancetoday } from "../../../api/dashboard";

export const attendance = writable({});
export const attendanceLoading = writable(false);
export const attendanceError = writable(null);

// State Changers
const requestAttendance = () => {
    attendanceLoading.set(true);
};

const receiveAttendanceSuccess = (data) => {
    attendance.set(data);
    attendanceLoading.set(false);
};
const receiveAttendanceEmpty = () => {
    attendance.set({});
    attendanceLoading.set(false);
};
const receiveAttendanceError = (error) => {
    attendanceError.set(error);
    attendanceLoading.set(false);
};

export async function fetchAttendance() {
    requestAttendance();
    getAttendancetoday()
        .then((response) => {
            if (!response.data.status) {
                receiveAttendanceEmpty();
                return;
            }
            receiveAttendanceSuccess(response.data);
        })
        .catch((error) => {
            receiveAttendanceError(error);
        });
}