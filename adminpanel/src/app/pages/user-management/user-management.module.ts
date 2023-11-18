import { NgModule } from '@angular/core';
import { routing } from './user-management.routing';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'app/shared/shared.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UserManagementComponent } from './user-management.component';
import { DataTablesModule } from "angular-datatables";


import { UserManagementTableComponent } from './components';
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
        UserManagementComponent,
        UserManagementTableComponent,
    ],
    providers: [
    ]
})
export class UserManagementModule { }
