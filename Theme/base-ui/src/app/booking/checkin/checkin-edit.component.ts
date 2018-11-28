import { Component, ViewEncapsulation, Input, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params }   from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Select2TemplateFunction, Select2OptionData } from 'ng2-select2';
import { __platform_browser_private__ } from '@angular/platform-browser';
import 'rxjs/add/operator/toPromise';
import { BaseEditComponent } from 'app/core/BaseEditComponent';

import { ResponseResult } from '../../core/ResponseResult';
import { CheckIn } from './checkin';
import { CheckInService } from './checkin.service';

@Component({
    selector: 'checkin-edit',
    templateUrl: './checkin-edit.template.html',
    styleUrls: ['../../forms/elements/elements.style.scss', '../../ui-elements/notifications/notifications.style.scss'],
    providers: [
		CheckInService			],
    encapsulation: ViewEncapsulation.None
})
export class CheckInEditComponent extends BaseEditComponent<CheckIn> {
	    datepickerOpts = { }
    constructor(
        protected checkinService: CheckInService,
		        protected route: ActivatedRoute,
        protected router: Router) {
        super(checkinService, route, router);
    }

    subscribeServices(): void {
	                
	                
	    }

    goBack(): void {
        this.router.navigate(['/app/booking/checkinlist']);
    }

    getModelValue(params: Params): Promise<void|CheckIn> {
        if (params['id'])
        {
            this.upsertText = 'Edit Check In';
            return this.checkinService.get(+params['id'])
                .catch(err => {
                    this.router.navigate(['/notfound']);       
                });
        }
        else {
            this.upsertText = 'New Check In';
            let m: CheckIn = this.getInitialValue();;
            let p: Promise<CheckIn> = new Promise((resolve, reject) => resolve(m));
            return p;
        }
    }

    getInitialValue(): CheckIn {
        return {
				GuestName: null,
				CheckInNumber: 0,
				InvoiceNo: null,
				ArrivalDate: null,
				DepartureDate: null,
				Adults: 0,
				Infant: null,
				Child: null,
				Baby: null,
				RoomNumber: 0,
				Id: 0,
				CreatedDate: null,
				DeletedDate: null,
				InsertNew: null
				};
    }
}