import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
})
export class ManageClientOrderService {
    constructor(private http: HttpClient) {

    }

    public viewClientOrders(): any {
        return this.http.get(`${environment.api}/ManageClientOrders/ViewClientOrders`)
            .pipe(first());
    }

    public getClientOrderDetailsById(id): any {
        return this.http.get(`${environment.api}/ManageClientOrders/GetClientOrderDetailsById?Id=${id}`)
            .pipe(first());
    }

    public getClientOrdersByClientId(clientId): any {
        return this.http.get(`${environment.api}/ManageClientOrders/GetClientOrdersByClientId?ClientId=${clientId}`)
            .pipe(first());
    }

    public assignLocationForClientOrder(data): any {
        return this.http.put(`${environment.api}/ManageClientOrders/AssignLocationForClientOrder`, data)
            .pipe(first())
    }

    public assignDriverForClientOrder(data): any {
        return this.http.put(`${environment.api}/ManageClientOrders/AssignDriverForClientOrder`, data)
            .pipe(first())
    }

    public unassignDriverForClientOrder(data): any {
        return this.http.put(`${environment.api}/ManageClientOrders/UnassignDriverForClientOrder`, data)
            .pipe(first())
    }

    public getClientOrderImagesById(id): any {
        return this.http.get(`${environment.api}/ClientOrders/GetClientOrderImagesById?Id=${id}`)
            .pipe(first());
    }
}