import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import { PromotionalLinkOrderRecordsManagementComponent } from './promotional-link-order-records-management.component';


export const routes: Routes = [
    {
        path: '',
        component: PromotionalLinkOrderRecordsManagementComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
