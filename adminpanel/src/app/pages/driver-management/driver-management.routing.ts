import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { DriverManagementComponent } from './driver-management.component';

export const routes: Routes = [
    {
        path: '',
        component: DriverManagementComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
