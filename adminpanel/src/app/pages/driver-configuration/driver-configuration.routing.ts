import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { DriverConfigurationComponent } from './driver-configuration.component';

export const routes: Routes = [
    {
        path: '',
        component: DriverConfigurationComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
