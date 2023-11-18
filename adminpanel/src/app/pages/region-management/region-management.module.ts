import { NgModule } from '@angular/core';
import { routing } from './region-management.routing';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'app/shared/shared.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RegionManagementComponent } from './region-management.component';
import { DataTablesModule } from "angular-datatables";


import { RegionManagementTableComponent, RegionManagementDetailsComponent } from './components';
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
        RegionManagementComponent,
        RegionManagementTableComponent,
        RegionManagementDetailsComponent
    ],
    providers: [
    ]
})
export class RegionManagementModule { }
