import { Component, ViewEncapsulation, Input, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params }   from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Select2TemplateFunction, Select2OptionData } from 'ng2-select2';
import { __platform_browser_private__ } from '@angular/platform-browser';
import 'rxjs/add/operator/toPromise';
import { BaseEditComponent } from 'app/core/BaseEditComponent';

import { ResponseResult } from '../../core/ResponseResult';
import { Invoice } from './invoice';
import { InvoiceService } from './invoice.service';
import { InvoiceStatuses } from "app/billing/invoicestatuses/invoicestatuses";
import { InvoiceStatusesService } from "app/billing/invoicestatuses/invoicestatuses.service";

@Component({
    selector: 'invoice-edit',
    templateUrl: './invoice-edit.template.html',
    styleUrls: ['../../forms/elements/elements.style.scss', '../../ui-elements/notifications/notifications.style.scss'],
    providers: [
		InvoiceService,					InvoiceStatusesService			],
    encapsulation: ViewEncapsulation.None
})
export class InvoiceEditComponent extends BaseEditComponent<Invoice> {
		invoicestatusesList: InvoiceStatuses[];
        datepickerOpts = { }
    constructor(
        protected invoiceService: InvoiceService,
					private invoicestatusesService: InvoiceStatusesService,
		        protected route: ActivatedRoute,
        protected router: Router) {
        super(invoiceService, route, router);
    }

    subscribeServices(): void {
	                
	                
			this.invoicestatusesList = this.invoicestatusesService.getList();
	    }

    goBack(): void {
        this.router.navigate(['/app/billing/invoicelist']);
    }

    getModelValue(params: Params): Promise<void|Invoice> {
        if (params['id'])
        {
            this.upsertText = 'Edit Invoice';
            return this.invoiceService.get(+params['id'])
                .catch(err => {
                    this.router.navigate(['/notfound']);       
                });
        }
        else {
            this.upsertText = 'New Invoice';
            let m: Invoice = this.getInitialValue();;
            let p: Promise<Invoice> = new Promise((resolve, reject) => resolve(m));
            return p;
        }
    }

    getInitialValue(): Invoice {
        return {
				Status: null,
				GuestName: null,
				InvoiceNo: null,
				DateIn: null,
				DateOut: null,
				Id: 0,
				CreatedDate: null,
				DeletedDate: null,
				InsertNew: null
				};
    }
}