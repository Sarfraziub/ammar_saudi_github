import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
})
export class PackageService {
    constructor(private http: HttpClient) {

    }

    public get(visible: boolean = null): any {
        return this.http.get(`${environment.api}/Packages/GetPackages?visible=${visible}`)
            .pipe(first());
    }

    public add(data): any {
        return this.http.post(`${environment.api}/Packages/Add`, data)
            .pipe(first())
    }

    public update(data): any {
        return this.http.put(`${environment.api}/Packages/Update`, data)
            .pipe(first())
    }

    public delete(id): any {
        return this.http.delete(`${environment.api}/Packages/Delete?Id=${id}`)
            .pipe(first())
    }
}