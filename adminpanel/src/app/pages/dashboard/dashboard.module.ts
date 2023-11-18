import { NgModule } from '@angular/core';
import { routing } from './dashboard.routing';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'app/shared/shared.module';
import { DashboardComponent } from './dashboard.component';



@NgModule({
    imports: [

        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule,
        routing,
    ],
    declarations: [
        DashboardComponent,
    ],
    providers: [
    ]
})
export class DashboardModule { }
