import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
})
export class DriverService {
    constructor(private http: HttpClient) {

    }

    public get(): any {
        return this.http.get(`${environment.api}/Drivers/GetDrivers`)
            .pipe(first());
    }

    public getActiveDrivers(): any {
        return this.http.get(`${environment.api}/Drivers/GetDrivers?ActiveDriver=true`)
            .pipe(first());
    }

    public getById(id): any {
        return this.http.get(`${environment.api}/Drivers/GetDriverById?Id=${id}`)
            .pipe(first());
    }

    public add(data): any {
        return this.http.post(`${environment.api}/Admins/AddDriver`, data)
            .pipe(first())
    }

    public update(data): any {
        return this.http.put(`${environment.api}/Admins/UpdateDriver`, data)
            .pipe(first())
    }
}