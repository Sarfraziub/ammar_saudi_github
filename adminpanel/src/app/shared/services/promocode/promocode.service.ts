import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
})
export class PromocodeService {
    constructor(private http: HttpClient) {

    }

    public get(): any {
        return this.http.get(`${environment.api}/Promocodes/Getpromocodes`)
            .pipe(first());
    }

    public add(data): any {
        return this.http.post(`${environment.api}/Promocodes/Add`, data)
            .pipe(first())
    }

    public update(data): any {
        return this.http.put(`${environment.api}/Promocodes/Update`, data)
            .pipe(first())
    }

    public delete(id): any {
        return this.http.delete(`${environment.api}/Promocodes/Delete?Id=${id}`)
            .pipe(first())
    }
}