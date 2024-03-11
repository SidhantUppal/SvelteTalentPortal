// @ts-nocheck
import { writable, derived } from 'svelte/store';
import { API_LOGIN, API_USER } from '../../api/config';
import axiosInstance from '../../api/AxiosInstance';
// @ts-ignore
export const AUTH_LOCAL_STORAGE_KEY = 'talent_portal_user';
export const CURRENT_LAYOUT_KEY = 'talent_portal_layout';

export const user = writable(null);
export const profile = writable({});
export const accessToken = writable(null);
export const currentLayout = writable(localStorage.getItem(CURRENT_LAYOUT_KEY) || 'auth');

export const isLoggedIn = derived(
    [user, accessToken],
    ([$user, $accessToken]) => {
        return $user !== null && $accessToken !== null;
    }
);

export const getCurrentUser = () => {
    if (!localStorage) return
    const res = localStorage.getItem(AUTH_LOCAL_STORAGE_KEY);
    if (!res) return null;
    return JSON.parse(res);
}

// @ts-ignore
/**
 * Logs in a user with the provided username and password.
 * @param {string} username - The username of the user.
 * @param {string} password - The password of the user.
 * @returns {Promise<boolean>} - A promise that resolves to true if the login is successful, false otherwise.
 */
export const login = async (username, password) => {
    var res = await axiosInstance.post(API_LOGIN, { username, password })
    if (res) {
        const userData = res.data;
        user.set(userData);
        accessToken.set(userData.accessToken);
        localStorage.setItem(AUTH_LOCAL_STORAGE_KEY, JSON.stringify(userData));
        return true;
    }
    else {
        return undefined;
    }

};

export const logout = async () => {
    user.set(null);
    accessToken.set(null);
    localStorage.removeItem(AUTH_LOCAL_STORAGE_KEY);
    currentLayout.set('auth');
    localStorage.setItem(CURRENT_LAYOUT_KEY, 'auth');
    // window.location.reload();
}


export const getUserProfile = async () => {
    const res = await axiosInstance.get(API_USER);
    if (res) {
        profile.set(res.data.data);
        return true;
    }
    else {
        return false;
    }
}