import { Routes, RouterModule } from '@angular/router';
import { Pages } from './pages.component';
import { ModuleWithProviders } from '@angular/core';
import { Menu } from 'app/shared/models';

export const routes: Routes = [
    {
        path: 'pages',
        component: Pages,
        children: [
            {
                path: '', redirectTo: 'dashboard', pathMatch: 'full'
            },
            {
                path: 'admin-management',
                loadChildren: () => import('app/pages/admin-management/admin-management.module').then(m => m.AdminManagementModule),

            },
            {
                path: 'driver-management',
                loadChildren: () => import('app/pages/driver-management/driver-management.module').then(m => m.DriverManagementModule),

            },
            {
                path: 'location-management',
                loadChildren: () => import('app/pages/location-management/location-management.module').then(m => m.LocationManagementModule),

            },
            {
                path: 'promotional-link-management',
                loadChildren: () => import('app/pages/promotional-link-management/promotional-link-management.module').then(m => m.PromotionalLinkManagementModule),
            },
            {
                path: 'promotional-link-order-records-management/:id',
                loadChildren: () => import('app/pages/promotional-link-order-records-management/promotional-link-order-records-management.module').then(m => m.PromotionalLinkOrderRecordsManagementModule),
            },
            {
                path: 'location-maps',
                loadChildren: () => import('app/pages/location-maps-management/location-maps-management.module').then(m => m.LocationMapManagementModule),

            },
            {
                path: 'region-management',
                loadChildren: () => import('app/pages/region-management/region-management.module').then(m => m.RegionManagementModule),

            },
            {
                path: 'slider-management',
                loadChildren: () => import('app/pages/slider-management/slider-management.module').then(m => m.SliderManagementModule),

            },
            {
                path: 'promocode-management',
                loadChildren: () => import('app/pages/promocode-management/promocode-management.module').then(m => m.PromoCodeManagementModule),

            },
            {
                path: 'order-management',
                loadChildren: () => import('app/pages/order-management/order-management.module').then(m => m.OrderManagementModule),

            },
            {
                path: 'order-management/:id',
                loadChildren: () => import('app/pages/order-management/order-management.module').then(m => m.OrderManagementModule),

            },
            {
                path: 'feedback-management',
                loadChildren: () => import('app/pages/feedback-management/feedback-management.module').then(m => m.FeedbackManagementModule),

            },
            {
                path: 'item-management',
                loadChildren: () => import('app/pages/item-management/item-management.module').then(m => m.ItemManagementModule),
            },
            {
                path: 'order-details/:id',
                loadChildren: () => import('app/pages/order-details/order-details.module').then(m => m.OrderDetailsModule),
            },
            {
                path: 'user-management',
                loadChildren: () => import('app/pages/user-management/user-management.module').then(m => m.UserManagementModule),
            },
            {
                path: 'claim-management',
                loadChildren: () => import('app/pages/claim-management/claim-management.module').then(m => m.ClaimManagementModule),
            },
            {
                path: 'notification-template',
                loadChildren: () => import('app/pages/notification-template/notification-template.module').then(m => m.NotificationTemplateModule),
            },
            {
                path: 'dashboard',
                loadChildren: () => import('app/pages/dashboard/dashboard.module').then(m => m.DashboardModule),
            },

            {
                path: 'driver-configuration',
                loadChildren: () => import('app/pages/driver-configuration/driver-configuration.module').then(m => m.DriverConfigurationModule),
            },
            {
                path: 'claim-details/:id',
                loadChildren: () => import('app/pages/claim-details/claim-details.module').then(m => m.ClaimDetailsModule),
            },
            {
                path: 'home-page-icon-management',
                loadChildren: () => import('app/pages/home-page-icon-management/home-page-icon-management.module').then(m => m.HomePageIconManagementModule),
            },
            {
                path: 'influencer-videos',
                loadChildren: () => import('app/pages/influencer-videos/influencer-videos.module').then(m => m.InfluencerVideosModule),
            },
            {
                path: 'package-management',
                loadChildren: () => import('app/pages/package-management/package-management.module').then(m => m.PackageManagementModule),
            },
            
        ]
    },
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
