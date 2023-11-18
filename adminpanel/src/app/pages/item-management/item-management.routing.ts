import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { ItemManagementComponent } from './item-management.component';

export const routes: Routes = [
    {
        path: '',
        component: ItemManagementComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
