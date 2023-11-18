import { NgModule } from '@angular/core';
import { routing } from './influencer-videos.routing';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'app/shared/shared.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { InfluencerVideosComponent } from './influencer-videos.component';
import { DataTablesModule } from "angular-datatables";


import { InfluencerVideosDetailsComponent, InfluencerVideosTableComponent } from './components';
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
        InfluencerVideosComponent,
        InfluencerVideosDetailsComponent,
        InfluencerVideosTableComponent
    ],
    providers: [
    ]
})
export class InfluencerVideosModule { }
