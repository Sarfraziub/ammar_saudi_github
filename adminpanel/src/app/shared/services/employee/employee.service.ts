import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
})
export class EmployeeService {
    constructor(private http: HttpClient) {

    }

    public get(): any {
        return this.http.get(`${environment.api}/Employees/GetList`)
            .pipe(first());
    }

    public getById(id): any {
        return this.http.get(`${environment.api}/Employees/GetById?Id=${id}`)
            .pipe(first());
    }

    public add(data): any {
        return this.http.post(`${environment.api}/Employees/AddEmployee`, data)
            .pipe(first())
    }

    public update(data): any {
        return this.http.put(`${environment.api}/Employees/Update`, data)
            .pipe(first())
    }

    public delete(id): any {
        return this.http.delete(`${environment.api}/Employees/Delete?Id=${id}`)
            .pipe(first())
    }
}