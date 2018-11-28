import { Component, ViewEncapsulation, Input, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params }   from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Select2TemplateFunction, Select2OptionData } from 'ng2-select2';
import { __platform_browser_private__ } from '@angular/platform-browser';
import 'rxjs/add/operator/toPromise';
import { BaseEditComponent } from 'app/core/BaseEditComponent';

import { ResponseResult } from '../../core/ResponseResult';
import { Guest } from './guest';
import { GuestService } from './guest.service';

@Component({
    selector: 'guest-edit',
    templateUrl: './guest-edit.template.html',
    styleUrls: ['../../forms/elements/elements.style.scss', '../../ui-elements/notifications/notifications.style.scss'],
    providers: [
		GuestService			],
    encapsulation: ViewEncapsulation.None
})
export class GuestEditComponent extends BaseEditComponent<Guest> {
	    datepickerOpts = { }
    constructor(
        protected guestService: GuestService,
		        protected route: ActivatedRoute,
        protected router: Router) {
        super(guestService, route, router);
    }

    subscribeServices(): void {
	                
	                
	    }

    goBack(): void {
        this.router.navigate(['/app/profile/guestlist']);
    }

    getModelValue(params: Params): Promise<void|Guest> {
        if (params['id'])
        {
            this.upsertText = 'Edit Guest Profile';
            return this.guestService.get(+params['id'])
                .catch(err => {
                    this.router.navigate(['/notfound']);       
                });
        }
        else {
            this.upsertText = 'New Guest Profile';
            let m: Guest = this.getInitialValue();;
            let p: Promise<Guest> = new Promise((resolve, reject) => resolve(m));
            return p;
        }
    }

    getInitialValue(): Guest {
        return {
				FirstName: null,
				LastName: null,
				Email: null,
				PassportNumber: null,
				DateOfBirth: null,
				Id: 0,
				CreatedDate: null,
				DeletedDate: null,
				InsertNew: null
				};
    }
}