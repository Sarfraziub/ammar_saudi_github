import { NgModule } from '@angular/core';
import { routing } from './claim-management.routing';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'app/shared/shared.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ClaimManagementComponent } from './claim-management.component';
import { DataTablesModule } from "angular-datatables";


import { ClaimManagementTableComponent } from './components';

@NgModule({
    imports: [

        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule,
        NgbModule,
        DataTablesModule,
        routing,
    ],
    declarations: [
        ClaimManagementComponent,
        ClaimManagementTableComponent,
    ],
    providers: [
    ]
})
export class ClaimManagementModule { }
