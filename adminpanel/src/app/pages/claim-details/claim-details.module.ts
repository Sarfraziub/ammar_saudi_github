import { NgModule } from '@angular/core';
import { routing } from './claim-details.routing';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'app/shared/shared.module';

import { ClaimDetailsComponent } from './claim-details.component';


import { ClaimDetailsTableComponent } from './components';
import { NgxBarcodeModule } from 'ngx-barcode';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
    imports: [

        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        NgbModule,
        NgxBarcodeModule,
        SharedModule,
        routing,
    ],
    declarations: [
        ClaimDetailsComponent,
        ClaimDetailsTableComponent,
    ],
    providers: [
    ]
})
export class ClaimDetailsModule { }
