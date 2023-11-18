import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "environments/environment";

@Injectable({
  providedIn: "root",
})
export class PromotionalLinkService {
  PROMOTIONAL_LINK = "/PromotionalLink";
  constructor(private http: HttpClient) {}

  getAllPromotionalLink() {
    return this.http.get(`${environment.api + this.PROMOTIONAL_LINK}/GetAll`);
  }

  checkUniqueNameAvailability(name) {
    return this.http.get(`${environment.api + this.PROMOTIONAL_LINK}/IsUniqueNameExist/${name}`);
  }

  addPromotionalLink(body) {
    return this.http.post(`${environment.api + this.PROMOTIONAL_LINK}/Add`, body);
  }

  updatePromotionalLink(body) {
    return this.http.put(
      `${environment.api + this.PROMOTIONAL_LINK}/Update?Id=${body.Id}&Name=${body.name}&MobileNo=${body.mobileNo}&Email=${body.email}&UniqueName=${
        body.uniqueName
      }
        `,
      body
    );
  }

  deletePromotionalLink(id) {
    return this.http.delete(`${environment.api + this.PROMOTIONAL_LINK}/Delete?Id=${id}`);
  }

  getPromotionalLinkDataById(id) {
    return this.http.get(`${environment.api + this.PROMOTIONAL_LINK}/GetById?Id=${id}`);
  }

  getClientOrderCountByPromotionalId(id, startDate, endDate) {
    return this.http.get(
      `${environment.api}/ClientOrders/GetClientOrderCountByPromotionalId?PromotionalId=${id}&StartDate=${startDate}&EndDate=${endDate}`
    );
  }

  getClientOrderReportByPromotionalId(id, startDate, endDate) {
    return this.http.get(
      `${environment.api}/ClientOrders/GetClientOrderReportByPromotionalId?StartDate=${startDate}&EndDate=${endDate}&Filters=promotionalLinkId==${id}`
    );
  }
}
