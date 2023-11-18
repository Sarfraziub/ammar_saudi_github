import { NgModule } from '@angular/core';
import { routing } from './order-details.routing';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'app/shared/shared.module';

import { OrderDetailsComponent } from './order-details.component';


import { OrderDetailsTableComponent } from './components';
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
        OrderDetailsComponent,
        OrderDetailsTableComponent,
    ],
    providers: [
    ]
})
export class OrderDetailsModule { }
