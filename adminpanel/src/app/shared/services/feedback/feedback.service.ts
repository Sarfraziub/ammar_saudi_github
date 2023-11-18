import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
})
export class FeedbackService {
    constructor(private http: HttpClient) {

    }

    public get(): any {
        return this.http.get(`${environment.api}/Feedbacks/GetFeedbacks`)
            .pipe(first());
    }

    public getById(id): any {
        return this.http.get(`${environment.api}/Feedbacks/GetById?Id=${id}`)
            .pipe(first());
    }
    public update(data): any {
        return this.http.put(`${environment.api}/Feedbacks/ShowHideFeedback`, data)
            .pipe(first())
    }
}