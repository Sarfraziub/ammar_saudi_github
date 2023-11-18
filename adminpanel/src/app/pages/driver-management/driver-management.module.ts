import { NgModule } from '@angular/core';
import { routing } from './driver-management.routing';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'app/shared/shared.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DriverManagementComponent } from './driver-management.component';
import { DataTablesModule } from "angular-datatables";


import { DriverManagementTableComponent, DriverManagementDetailsComponent } from './components';
import { NgSelect2Module } from 'ng-select2';

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
    ],
    declarations: [
        DriverManagementComponent,
        DriverManagementTableComponent,
        DriverManagementDetailsComponent
    ],
    providers: [
    ]
})
export class DriverManagementModule { }
