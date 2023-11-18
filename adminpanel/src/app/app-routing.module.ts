import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import { LoginComponent } from './pages/login';
import { AccessCodeComponent } from './pages/access-code';
import { PrivacyPolicyComponent } from './pages/privacy-policy/privacy-policy.component';

export const routes: Routes =
  [{
    path: 'access-code',
    component: AccessCodeComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'privacy-policy',
    component: PrivacyPolicyComponent
  },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' },
  ];

export const routing: ModuleWithProviders = RouterModule.forRoot(routes);
