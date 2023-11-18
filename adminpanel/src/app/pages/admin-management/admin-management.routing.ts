import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { AdminManagementComponent } from './admin-management.component';

export const routes: Routes = [
    {
        path: '',
        component: AdminManagementComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
