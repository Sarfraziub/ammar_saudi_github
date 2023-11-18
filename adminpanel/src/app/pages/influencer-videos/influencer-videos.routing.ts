import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { InfluencerVideosComponent } from './influencer-videos.component';

export const routes: Routes = [
    {
        path: '',
        component: InfluencerVideosComponent,
    }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
