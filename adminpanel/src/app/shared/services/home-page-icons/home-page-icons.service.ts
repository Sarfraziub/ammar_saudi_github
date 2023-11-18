import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
})
export class HomePageIconsService {
    constructor(private http: HttpClient) {

    }

    public get(): any {
        return this.http.get(`${environment.api}/HomePageIcons/GetHomePageIcons`)
            .pipe(first());
    }
    
    public update(data): any {
        return this.http.post(`${environment.api}/HomePageIcons/UpdateHomePageIcon`, data)
            .pipe(first())
    }
}