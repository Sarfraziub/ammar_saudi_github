import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { ClaimDetailsComponent } from './claim-details.component';

export const routes: Routes = [
    {
        path: '',
        component: ClaimDetailsComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
