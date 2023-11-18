import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
})
export class DriverFeesService {
    constructor(private http: HttpClient) {

    }

    public get(): any {
        return this.http.get(`${environment.api}/DriverFees/GetDriverFeeSettings`)
            .pipe(first());
    }

    public add(data): any {
        return this.http.post(`${environment.api}/DriverFees/AddDriverFee`, data)
            .pipe(first())
    }
}