import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { StorageService } from 'app/shared/infrastructure/storage.service';
import { environment } from 'environments/environment';
import { interval, Subject } from 'rxjs';

const tokenKey = 'auth-token';
const userKey = 'auth-user';
const remmberMeKey = 'rememeber-me';

@Injectable({
    providedIn: 'root'
})
export class TokenStorageService {
    timerInterval: any;

    constructor(private storageService: StorageService, private http: HttpClient) { }

    signOut(): void {
        this.storageService.secureStorage.clear();
        this.storageService.rememberMeSecureStorage.clear();
    }


    tokenSignOut(): void {
        const redirectUrl = this.storageService.secureStorage.getItem('redirect-url');
        this.storageService.secureStorage.clear();
        this.storageService.secureStorage.setItem('redirect-url', redirectUrl);
    }

    tokenRefreshedSource = new Subject();
    tokenRefreshed$ = this.tokenRefreshedSource.asObservable();

    public saveToken(token: any): void {
        // this.storageService.secureStorage.clear();
        this.storageService.secureStorage.setItem(tokenKey, token);
    }

    public saveRememberMe(data) {
        this.storageService.rememberMeSecureStorage.removeItem(remmberMeKey);
        this.storageService.rememberMeSecureStorage.setItem(remmberMeKey, data);
    }

    public getRememberMe() {
        let data = this.storageService.rememberMeSecureStorage.getItem(remmberMeKey);
        return data ? data : null;
    }

    public getToken(): any | null {
        let token = this.storageService.secureStorage.getItem(tokenKey);
        return token ? token : null;
    }

    public saveUser(user: any): void {
        this.storageService.secureStorage.setItem(userKey, JSON.stringify(user));
    }

    public deocodeToken(token) {
        var base64Url = token.split('.')[1];
        var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));

        return JSON.parse(jsonPayload);
    }

    public refreshToken(minutes, token) {
        clearInterval(this.timerInterval);
        this.timerInterval = interval(minutes * 60 * 100).subscribe(x => {
            this.renewToken(token);
        });
    }

    private renewToken(token: string) {
        return this.http.post(`${environment.api}/Accounts/RefreshTokenSync`, {
            refreshToken: token
        }).subscribe(result => {
            this.saveToken(result);
        });
    }

    public async getUser() {
        let user = this.storageService.secureStorage.getItem(userKey);
        if (user) {
            return JSON.parse(user);
        }
        const response = await this.http.get(`${environment.api}/Accounts/GetUserProfileSync`).toPromise();
        this.saveUser(response);
        this.storageService.secureStorage.setItem('permissions', JSON.stringify(response));
        return response;
    }
}