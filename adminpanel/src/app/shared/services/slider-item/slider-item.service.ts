import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
})
export class SliderItemService {
    constructor(private http: HttpClient) {

    }

    public get(): any {
        return this.http.get(`${environment.api}/SliderItems/GetSliderItems`)
            .pipe(first());
    }

    public add(data): any {
        return this.http.post(`${environment.api}/SliderItems/Add`, data)
            .pipe(first())
    }

    public update(data): any {
        return this.http.put(`${environment.api}/SliderItems/Update`, data)
            .pipe(first())
    }

    public delete(id): any {
        return this.http.delete(`${environment.api}/SliderItems/Delete?Id=${id}`)
            .pipe(first())
    }
}