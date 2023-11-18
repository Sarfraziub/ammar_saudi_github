import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
})
export class ClientOrderService {
    constructor(private http: HttpClient) {

    }

    public get(id): any {
        return this.http.get(`${environment.api}/ClientOrders/GetClientOrderDetailsById?Id=${id}`)
            .pipe(first());
    }
}