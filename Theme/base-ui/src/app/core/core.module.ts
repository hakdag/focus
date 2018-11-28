import { CommonModule } from '@angular/common';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ListCheckboxPipe } from 'app/core/pipes/listcb.pipe'
import 'messenger/build/js/messenger.js';
import { ActionButton } from 'app/shared/actionbutton.component';
import { BreadCrumb } from 'app/shared/breadcrumb.component';

@NgModule({
    declarations: [ ListCheckboxPipe, ActionButton, BreadCrumb ],
    imports: [CommonModule],
    exports: [ ListCheckboxPipe, ActionButton, BreadCrumb ],
    schemas:  [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class coreModule {
}
