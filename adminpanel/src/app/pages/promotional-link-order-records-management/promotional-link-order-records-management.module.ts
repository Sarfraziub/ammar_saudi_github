import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { DataTablesModule } from "angular-datatables";
import { SharedModule } from "app/shared/shared.module";
import { PromotionalLinkOrderRecordsDetailsComponent, PromotionalLinkOrderRecordsTableComponent } from "./components";
import { routing } from "./promotional-link-order-records-management.routing";
import { PromotionalLinkOrderRecordsManagementComponent } from "./promotional-link-order-records-management.component";

@NgModule({
  imports: [CommonModule, FormsModule, ReactiveFormsModule, SharedModule, NgbModule, DataTablesModule, routing],
  declarations: [
    PromotionalLinkOrderRecordsManagementComponent,
    PromotionalLinkOrderRecordsDetailsComponent,
    PromotionalLinkOrderRecordsTableComponent,
  ],
  providers: [],
})
export class PromotionalLinkOrderRecordsManagementModule {}
