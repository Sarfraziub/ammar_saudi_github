import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
})
export class ClaimService {
    constructor(private http: HttpClient) {

    }

    public get(): any {
        return this.http.get(`${environment.api}/ManageClaims/GetClaims`)
            .pipe(first());
    }

    public getClientOrdersByDriverClaimId(id): any {
        return this.http.get(`${environment.api}/ManageClaims/GetClientOrdersByDriverClaimId?Id=${id}`)
            .pipe(first());
    }
    
    public closeClaim(data): any {
        return this.http.put(`${environment.api}/ManageClaims/CloseClaim`, data)
            .pipe(first())
    }
}