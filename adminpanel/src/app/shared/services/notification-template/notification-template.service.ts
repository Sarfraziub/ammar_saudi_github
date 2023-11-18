import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
})
export class NotificationTemplateService {
    constructor(private http: HttpClient) {

    }

    public get(): any {
        return this.http.get(`${environment.api}/NotificationTemplates/GetNotificationTemplates`)
            .pipe(first());
    }

    public add(data): any {
        return this.http.post(`${environment.api}/NotificationTemplates/AddNotificationTemplate`, data)
            .pipe(first())
    }
    public send(data): any {
        return this.http.post(`${environment.api}/NotificationTemplates/SendNotificationTemplate`, data)
            .pipe(first())
    }

    public update(data): any {
        return this.http.put(`${environment.api}/NotificationTemplates/UpdateNotificationTemplate`, data)
            .pipe(first())
    }

    public delete(id): any {
        return this.http.delete(`${environment.api}/NotificationTemplates/DeleteNotificationTemplate?Id=${id}`)
            .pipe(first())
    }
}