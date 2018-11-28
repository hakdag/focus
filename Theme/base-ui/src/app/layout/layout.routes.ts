import { Routes, RouterModule }  from '@angular/router';
import { Layout } from './layout.component';

const routes: Routes = [
  { path: '', component: Layout, children: [
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    { path: 'dashboard', loadChildren: '../dashboard/dashboard.module#DashboardModule' },
    { path: 'booking', loadChildren: '../booking/booking.module#bookingModule' }, 
    { path: 'profile', loadChildren: '../profile/profile.module#profileModule' }, 
    { path: 'billing', loadChildren: '../billing/billing.module#billingModule' }, 
  ]}
];

export const ROUTES = RouterModule.forChild(routes);
