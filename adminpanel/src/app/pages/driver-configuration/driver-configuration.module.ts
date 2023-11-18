import { NgModule } from '@angular/core';
import { routing } from './driver-configuration.routing';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'app/shared/shared.module';
import { DriverConfigurationComponent } from './driver-configuration.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';



@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule,
        NgbModule,
        routing,
    ],
    declarations: [
        DriverConfigurationComponent,
    ],
    providers: [
    ]
})
export class DriverConfigurationModule { }
