import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
})
export class AdminService {
    constructor(private http: HttpClient) {

    }

    public get(): any {
        return this.http.get(`${environment.api}/Admins/GetAdmins`)
            .pipe(first());
    }

    public add(data): any {
        return this.http.post(`${environment.api}/Admins/AddNewAdmin`, data)
            .pipe(first())
    }

    public lockoutAccount(data): any {
        return this.http.put(`${environment.api}/Admins/LockoutAccount`, data)
            .pipe(first())
    }

    public unlockedAccount(data): any {
        return this.http.put(`${environment.api}/Admins/UnlockedAccount`, data)
            .pipe(first())
    }
}