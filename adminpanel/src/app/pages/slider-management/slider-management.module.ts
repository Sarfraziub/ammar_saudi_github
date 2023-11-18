import { NgModule } from '@angular/core';
import { routing } from './slider-management.routing';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'app/shared/shared.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SliderManagementComponent } from './slider-management.component';
import { DataTablesModule } from "angular-datatables";


import { SliderManagementTableComponent, SliderManagementDetailsComponent } from './components';
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
        SliderManagementComponent,
        SliderManagementTableComponent,
        SliderManagementDetailsComponent
    ],
    providers: [
    ]
})
export class SliderManagementModule { }
