import { Routes } from '@angular/router';
import { ErrorComponent } from './error/error.component';
import { NotFoundComponent } from './notfound/notfound.component';
import { UnAuthorizedComponent } from './unauthorized/unauthorized.component';
import { AuthGuard } from './core/auth/index';

export const ROUTES: Routes = [{
   path: '', redirectTo: 'app', pathMatch: 'full'
  }, {
    path: 'app',   loadChildren: './layout/layout.module#LayoutModule', canActivate: [AuthGuard]
  }, {
    path: 'login', loadChildren: './login/login.module#LoginModule'
  }, {
    path: 'notfound', component: NotFoundComponent
  }, {
    path: 'error', component: ErrorComponent
  }, {
    path: 'unauthorized', component: UnAuthorizedComponent
  }, {
    path: '**',    component: ErrorComponent
  }
];
