import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { DataTablesModule } from "angular-datatables";
import { SharedModule } from "app/shared/shared.module";
import {routing} from './promotional-link-management.routing'
import { PromotionalLinkManagementComponent } from "./promotional-link-management.component";
import { PromotionalLinkTableComponent } from './components/promotional-link-table/promotional-link-table.component';
import { PromotionalLinkDetailsComponent } from "./components";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule,
        NgbModule,
        DataTablesModule,
        routing
    ],
    declarations: [
       PromotionalLinkManagementComponent,
       PromotionalLinkTableComponent,
       PromotionalLinkDetailsComponent
    ],
    providers: [
    ]
})
export class PromotionalLinkManagementModule { }