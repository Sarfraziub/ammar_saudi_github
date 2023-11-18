import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { OrderManagementComponent } from './order-management.component';

export const routes: Routes = [
    {
        path: '',
        component: OrderManagementComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
