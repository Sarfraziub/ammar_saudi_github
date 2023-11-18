import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { PromoCodeManagementComponent } from './promocode-management.component';

export const routes: Routes = [
    {
        path: '',
        component: PromoCodeManagementComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
