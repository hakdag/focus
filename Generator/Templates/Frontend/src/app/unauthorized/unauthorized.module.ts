import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { UnAuthorizedComponent } from './unauthorized.component.ts';

export const routes = [
  { path: '', component: UnAuthorizedComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [
    UnAuthorizedComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(routes),
  ]
})
export class UnAuthorizedModule {
  static routes = routes;
}
