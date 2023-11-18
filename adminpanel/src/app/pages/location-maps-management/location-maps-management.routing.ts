import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { LocationMapsManagementComponent } from './location-maps-management.component';

export const routes: Routes = [
    {
        path: '',
        component: LocationMapsManagementComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
