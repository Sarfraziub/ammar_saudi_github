import { Component, OnInit, ViewChild } from "@angular/core";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { LocationManagementDetailsComponent } from "app/shared/components/modals/location-management-details";
import { BaseComponent } from "app/shared/infrastructure/base.component";
import { LocationService } from "app/shared/services";

@Component({
  selector: "app-location-maps-management",
  templateUrl: "./location-maps-management.component.html",
})
export class LocationMapsManagementComponent extends BaseComponent {
  isFullScreen: boolean;
  markers: any[] = [];
  selectedItem: any;
  isEdit = false

  @ViewChild("viewModal") viewModal: any;
  @ViewChild("deleteModal") deleteModal: any;

  constructor(private locationService: LocationService, private modal: NgbModal) {
    super();
  }

  ngOnInit(): void {
    this.locationService.get().subscribe(({ locations }) => {
      const image = "assets/mosque.png";
      this.markers = locations.map((location) => {
        return {
          position: {
            lat: location.latitude,
            lng: location.longitude,
          },
          options: {
            icon: image,
            animation: google.maps.Animation.DROP,
            id: location.id,
          },
        };
      });
    });
  }

  openModal(id) {
    this.isEdit = true
    this.selectedItem = { id };
    this.viewModal.nativeElement.click();
  }
}
