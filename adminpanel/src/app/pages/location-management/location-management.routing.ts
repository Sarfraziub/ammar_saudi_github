import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { LocationManagementComponent } from './location-management.component';

export const routes: Routes = [
    {
        path: '',
        component: LocationManagementComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
