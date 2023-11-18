import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { OrderDetailsComponent } from './order-details.component';

export const routes: Routes = [
    {
        path: '',
        component: OrderDetailsComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
