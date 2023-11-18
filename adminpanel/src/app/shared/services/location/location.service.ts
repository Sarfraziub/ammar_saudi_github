import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
})
export class LocationService {
    constructor(private http: HttpClient) {

    }

    public get(): any {
        return this.http.get(`${environment.api}/Locations/GetLocations`)
            .pipe(first());
    }

    public getById(id): any {
        return this.http.get(`${environment.api}/Locations/GetById?Id=${id}`)
            .pipe(first());
    }

    public add(data): any {
        return this.http.post(`${environment.api}/Locations/Add`, data)
            .pipe(first())
    }

    public update(data): any {
        return this.http.put(`${environment.api}/Locations/UpdateLocation`, data)
            .pipe(first())
    }

    public delete(id): any {
        return this.http.delete(`${environment.api}/Locations/Delete?Id=${id}`)
            .pipe(first())
    }

    public removeImage(id): any {
        return this.http.delete(`${environment.api}/Locations/RemoveLocationImage?Id=${id}`)
            .pipe(first())
    }

    public addImage(data): any {
        return this.http.post(`${environment.api}/Locations/AddLocationImage`, data)
            .pipe(first())
    }
}