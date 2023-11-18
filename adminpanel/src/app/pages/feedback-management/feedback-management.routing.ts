import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { FeedbackManagementComponent } from './feedback-management.component';

export const routes: Routes = [
    {
        path: '',
        component: FeedbackManagementComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
