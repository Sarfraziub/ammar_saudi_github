import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { UserManagementComponent } from './user-management.component';

export const routes: Routes = [
    {
        path: '',
        component: UserManagementComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
