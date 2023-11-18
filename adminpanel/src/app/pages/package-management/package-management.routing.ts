import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { PackageManagementComponent } from './package-management.component';

export const routes: Routes = [
    {
        path: '',
        component: PackageManagementComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
