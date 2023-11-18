import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
})
export class SystemSettingsService {
    constructor(private http: HttpClient) {

    }

    public get(): any {
        return this.http.get(`${environment.api}/ContentSettings/GetContentSettings`)
            .pipe(first());
    }

    public update(data): any {
        return this.http.put(`${environment.api}/ContentSettings/UpdateContentSettings`, data)
            .pipe(first())
    }
}