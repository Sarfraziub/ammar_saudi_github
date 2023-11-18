import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { HomePageIconManagementComponent } from './home-page-icon-management.component';

export const routes: Routes = [
    {
        path: '',
        component: HomePageIconManagementComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
