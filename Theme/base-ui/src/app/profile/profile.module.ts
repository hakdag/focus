
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AlertModule, TooltipModule } from 'ng2-bootstrap';
import { ButtonsModule, DropdownModule, PaginationModule  } from 'ng2-bootstrap';
import { DataTableModule } from "angular2-serverpagination-datatable";
import { HttpModule }    from '@angular/http';

import { WidgetModule } from '../layout/widget/widget.module';
import { UtilsModule } from '../layout/utils/utils.module';
import { JqSparklineModule } from '../components/sparkline/sparkline.module';

/*Forms*/
import { Select2Module } from 'ng2-select2';
declare var global: any;
// libs
/* tslint:disable */
var markdown = require('markdown').markdown;
/* tslint:enable */
global.markdown = markdown;
import 'bootstrap-markdown/js/bootstrap-markdown.js';
import 'bootstrap-select/dist/js/bootstrap-select.js';
import 'parsleyjs';
import 'bootstrap-application-wizard/src/bootstrap-wizard.js';
import 'twitter-bootstrap-wizard/jquery.bootstrap.wizard.js';
import 'jasny-bootstrap/docs/assets/js/vendor/holder.js';
import 'jasny-bootstrap/js/fileinput.js';
import 'ng2-datetime/src/vendor/bootstrap-datepicker/bootstrap-datepicker.min.js';
import 'ng2-datetime/src/vendor/bootstrap-timepicker/bootstrap-timepicker.min.js';
import 'bootstrap-colorpicker';
import 'bootstrap-slider/dist/bootstrap-slider.js';
import 'jasny-bootstrap/docs/assets/js/vendor/holder.js';
import 'jasny-bootstrap/js/fileinput.js';
import 'jasny-bootstrap/js/inputmask.js';
import { NKDatetimeModule } from 'ng2-datetime/ng2-datetime';
import { coreModule } from 'app/core/core.module'

import { GuestListComponent } from './guest/guest-list.component';
import { GuestEditComponent } from './guest/guest-edit.component';
  

export const routes = [
  {path: '', redirectTo: 'guest', pathMatch: 'full'},
	  
  {path: 'guestlist', component: GuestListComponent},
  {path: 'guest/:id', component: GuestEditComponent},
  {path: 'guest', component: GuestEditComponent}  
];

@NgModule({
  declarations: [
    // Components / Directives/ Pipes
    GuestListComponent,
    GuestEditComponent
  
  ],
  imports: [
    coreModule,
    CommonModule,
    JqSparklineModule,
    FormsModule,
    AlertModule.forRoot(),
    TooltipModule.forRoot(),
    ButtonsModule.forRoot(),
    DropdownModule.forRoot(),
    PaginationModule.forRoot(),
    HttpModule,
    NKDatetimeModule,
    WidgetModule,
    UtilsModule,
    DataTableModule,
    Select2Module,
    RouterModule.forChild(routes)
  ],
  schemas:  [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class profileModule {
  static routes = routes;
}