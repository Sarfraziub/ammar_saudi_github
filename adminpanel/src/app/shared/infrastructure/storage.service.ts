import { Injectable } from '@angular/core';
import SecureStorage from 'secure-web-storage';
const SECRET_KEY = 'Qatarat-Admin-cms-front-end-P@ssw0rd@944561-Qatarat-Admin-cms';
import CryptoJS from 'crypto-js';
@Injectable({
    providedIn: 'root'
})
export class StorageService {
    constructor() { }
    public secureStorage = new SecureStorage(sessionStorage, {
        hash: function hash(key): any {
            key = CryptoJS.SHA256(key, SECRET_KEY);
            return key.toString();
        },
        // Encrypt the localstorage data
        encrypt: function encrypt(data) {
            data = CryptoJS.AES.encrypt(data, SECRET_KEY);
            data = data.toString();
            return data;
        },
        // Decrypt the encrypted data
        decrypt: function decrypt(data) {
            data = CryptoJS.AES.decrypt(data, SECRET_KEY);
            data = data.toString(CryptoJS.enc.Utf8);
            return data;
        }
    });

    public rememberMeSecureStorage = new SecureStorage(localStorage, {
        hash: function hash(key): any {
            key = CryptoJS.SHA256(key, SECRET_KEY);
            return key.toString();
        },
        // Encrypt the localstorage data
        encrypt: function encrypt(data) {
            data = CryptoJS.AES.encrypt(data, SECRET_KEY);
            data = data.toString();
            return data;
        },
        // Decrypt the encrypted data
        decrypt: function decrypt(data) {
            data = CryptoJS.AES.decrypt(data, SECRET_KEY);
            data = data.toString(CryptoJS.enc.Utf8);
            return data;
        }
    });
}
