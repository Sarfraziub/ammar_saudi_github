import { NgModule } from '@angular/core';
import { routing } from './location-management.routing';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'app/shared/shared.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LocationManagementComponent } from './location-management.component';
import { DataTablesModule } from "angular-datatables";
import { GoogleMapsModule } from '@angular/google-maps'


import { LocationManagementTableComponent } from './components';
import { NgSelect2Module } from 'ng-select2';
import { LocationManagementDetailsModule } from 'app/shared/components/modals/location-management-details/location-management-details.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule,
        NgbModule,
        DataTablesModule,
        NgSelect2Module,
        routing,
        GoogleMapsModule,
        LocationManagementDetailsModule
    ],
    declarations: [
        LocationManagementComponent,
        LocationManagementTableComponent
    ],
    providers: [
    ]
})
export class LocationManagementModule { }
