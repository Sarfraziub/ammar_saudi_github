import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { GoogleMapsModule } from "@angular/google-maps";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { SharedModule } from "app/shared/shared.module";
import { NgSelect2Module } from "ng-select2";
import { LocationManagementDetailsComponent } from "./location-management-details.component";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule,
        NgbModule,
        NgSelect2Module,
        GoogleMapsModule,
    ],
    declarations: [LocationManagementDetailsComponent],
    exports: [LocationManagementDetailsComponent]
})

export class LocationManagementDetailsModule { }