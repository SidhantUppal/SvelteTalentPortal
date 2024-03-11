// @ts-nocheck
// dateUtils.js

/**
 * @param {string | number | Date} dateTimeString
 */
export function formatDate(dateTimeString) {
    const date = new Date(dateTimeString);
    return date.toLocaleTimeString('en-US', { hour12: false });
}

// export function formatTimeToDate(time) {
//     console.log(time);
//     const [hours, minutes,seconds] = time.split(':').map(Number);
//     debugger;
//     const currentDate = new Date();
//     currentDate.setHours(hours, minutes);
//     currentDate.setMinutes(currentDate.getMinutes() - currentDate.getTimezoneOffset());
//     return currentDate;
// }
export function formatTimeToDate(time) {
    // check if time is in ISO format
    let currentDate = new Date();
    if (time.includes('T')) {
        currentDate = new Date(time);
        currentDate.setMinutes(currentDate.getMinutes() - currentDate.getTimezoneOffset());
        return currentDate;
    }
    const parts = time.split(':').map(Number);
    if (parts.length === 3) {
        // Format: "hh:mm:ss"
        currentDate.setHours(parts[0], parts[1], parts[2]);
    } else if (parts.length === 2) {
        // Format: "hh:mm"
        currentDate.setHours(parts[0], parts[1]);
    } else {
        return null;
    }
    // Adjust for local time zone
    currentDate.setMinutes(currentDate.getMinutes() - currentDate.getTimezoneOffset());
    return currentDate;
}
export function formatTimeToIsoDate(time) {
    const isoDate = new Date(time);
    return isoDate;
}

export function formatDateWithoutTime(dateTimeString) {
    const date = new Date(dateTimeString);
    return date.toLocaleDateString('en-US');
}
export function formatDateToMinHrs(dateTimeString) {
    if (!dateTimeString) return '';
    let hours = dateTimeString.split(':')[0];
    let minutes = dateTimeString.split(':')[1];
    return `${hours}h ${minutes}m`;
}