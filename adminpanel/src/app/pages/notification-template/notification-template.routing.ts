import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { NotificationTemplateComponent } from './notification-template.component';

export const routes: Routes = [
    {
        path: '',
        component: NotificationTemplateComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
