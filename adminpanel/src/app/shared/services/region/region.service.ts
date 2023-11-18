import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
})
export class RegionService {
    constructor(private http: HttpClient) {

    }

    public get(): any {
        return this.http.get(`${environment.api}/Regions/GetRegions`)
            .pipe(first());
    }

    public add(data): any {
        return this.http.post(`${environment.api}/Regions/Add`, data)
            .pipe(first())
    }

    public update(data): any {
        return this.http.put(`${environment.api}/Regions/Update`, data)
            .pipe(first())
    }

    public delete(id): any {
        return this.http.delete(`${environment.api}/Regions/Delete?Id=${id}`)
            .pipe(first())
    }
}