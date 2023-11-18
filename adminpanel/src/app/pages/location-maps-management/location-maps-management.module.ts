import { NgModule } from '@angular/core';
import { routing } from './location-maps-management.routing';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'app/shared/shared.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { GoogleMapsModule } from '@angular/google-maps'

import { LocationMapsManagementComponent } from './location-maps-management.component';
import { LocationManagementDetailsModule } from 'app/shared/components/modals/location-management-details/location-management-details.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule,
        NgbModule,
        routing,
        GoogleMapsModule,
        LocationManagementDetailsModule
    ],
    declarations: [
      LocationMapsManagementComponent,
    ],
    providers: [
    ]
})
export class LocationMapManagementModule { }
