import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    constructor(private http: HttpClient) { }

    public access(data): any {
        return this.http.post(`${environment.api}/AdminAccounts/Access`, data)
            .pipe(first())
    }
    public login(data): any {
        return this.http.post(`${environment.api}/Accounts/Login`, data)
            .pipe(first())
    }
}