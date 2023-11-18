import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { RegionManagementComponent } from './region-management.component';

export const routes: Routes = [
    {
        path: '',
        component: RegionManagementComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
