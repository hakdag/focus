import { CommonModule } from '@angular/common';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { Adres } from 'app/tanimlamalar/adres/adres';
import { AdresShortPipe } from 'app/core/pipes/adres.pipe'
import { MalHizmetTipleri } from "app/tanimlamalar/malhizmettipleri/malhizmettipleri";
import { MalHizmetTipleriPipe } from 'app/core/pipes/malhizmettipleri.pipe'
import { SiparisInfoPipe } from 'app/core/pipes/SiparisInfoPipe.pipe'
import { ListCheckboxPipe } from 'app/core/pipes/listcb.pipe'
import 'messenger/build/js/messenger.js';
import { ActionButton } from 'app/shared/actionbutton.component';
import { BreadCrumb } from 'app/shared/breadcrumb.component';

@NgModule({
    declarations: [ AdresShortPipe, MalHizmetTipleriPipe, ListCheckboxPipe, SiparisInfoPipe, ActionButton, BreadCrumb ],
    imports: [CommonModule],
    exports: [ AdresShortPipe, MalHizmetTipleriPipe, ListCheckboxPipe, SiparisInfoPipe, ActionButton, BreadCrumb ],
    schemas:  [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class coreModule {
}
