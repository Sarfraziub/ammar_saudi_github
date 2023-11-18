import { NgModule } from '@angular/core';
import { routing } from './order-management.routing';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'app/shared/shared.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { OrderManagementComponent } from './order-management.component';
import { DataTablesModule } from "angular-datatables";


import { OrderManagementTableComponent, orderManagementViewFeedbackComponent, OrderManagementSetDriverComponent, OrderManagementSetLocationComponent } from './components';
import { NgSelect2Module } from 'ng-select2';
import { OrderManagementLocationComponent } from './components/order-management-location/order-management-location.component';
import { GoogleMapsModule } from '@angular/google-maps';

@NgModule({
    imports: [

        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule,
        NgbModule,
        GoogleMapsModule,
        DataTablesModule,
        NgSelect2Module,
        routing,
    ],
    declarations: [
        OrderManagementComponent,
        OrderManagementTableComponent,
        OrderManagementSetDriverComponent,
        orderManagementViewFeedbackComponent,
        OrderManagementSetLocationComponent,
        OrderManagementLocationComponent
    ],
    providers: [
    ]
})
export class OrderManagementModule { }
