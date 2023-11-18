import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { SliderManagementComponent } from './slider-management.component';

export const routes: Routes = [
    {
        path: '',
        component: SliderManagementComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
