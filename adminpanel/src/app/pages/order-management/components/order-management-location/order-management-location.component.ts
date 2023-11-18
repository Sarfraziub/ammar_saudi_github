import { Component, Input, OnInit, ViewChild } from "@angular/core";
import { GoogleMap } from "@angular/google-maps";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "app-order-management-location",
  templateUrl: "./order-management-location.component.html",
})
export class OrderManagementLocationComponent implements OnInit {

  @Input() markers = [];
  @Input() item;

  @ViewChild(GoogleMap)

  zoom = 12;
  center!: google.maps.LatLngLiteral;
  options: google.maps.MapOptions = {
    zoomControl: true,
    scrollwheel: false,
    disableDefaultUI: true,
    fullscreenControl: true,
    disableDoubleClickZoom: true,
    mapTypeId: "hybrid",
  };
  latitudeMap!: any;
  longitudeMap!: any;

  constructor(private activeModal: NgbActiveModal, private toastr: ToastrService) {}

  ngOnInit() {
    if(this.item.latitude === 0 && this.item.longitude === 0) {
      this.dismissModal();
      this.toastr.error("Location not found");
    }
    console.log(this.item)
    // Kaaba is the default position
    this.center = {
      lat: 21.4225,
      lng: 39.8262,
    };
  }
  dismissModal() {
    this.activeModal.close();
  }
}
