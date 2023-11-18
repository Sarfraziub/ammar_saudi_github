import { NgModule } from '@angular/core';
import { routing } from './home-page-icon-management.routing';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'app/shared/shared.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HomePageIconManagementComponent } from './home-page-icon-management.component';
import { DataTablesModule } from "angular-datatables";


import { HomePageIconManagementDetailsComponent, HomePageIconManagementTableComponent } from './components';
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
        HomePageIconManagementComponent,
        HomePageIconManagementDetailsComponent,
        HomePageIconManagementTableComponent
    ],
    providers: [
    ]
})
export class HomePageIconManagementModule { }
