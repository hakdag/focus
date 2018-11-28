import { Component, ViewEncapsulation, Input, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params }   from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Select2TemplateFunction, Select2OptionData } from 'ng2-select2';
import { __platform_browser_private__ } from '@angular/platform-browser';
import 'rxjs/add/operator/toPromise';
import { BaseEditComponent } from 'app/core/BaseEditComponent';

import { ResponseResult } from '../../core/ResponseResult';
import { Reservation } from './reservation';
import { ReservationService } from './reservation.service';

@Component({
    selector: 'reservation-edit',
    templateUrl: './reservation-edit.template.html',
    styleUrls: ['../../forms/elements/elements.style.scss', '../../ui-elements/notifications/notifications.style.scss'],
    providers: [
		ReservationService			],
    encapsulation: ViewEncapsulation.None
})
export class ReservationEditComponent extends BaseEditComponent<Reservation> {
	    datepickerOpts = { }
    constructor(
        protected reservationService: ReservationService,
		        protected route: ActivatedRoute,
        protected router: Router) {
        super(reservationService, route, router);
    }

    subscribeServices(): void {
	                
	                
	    }

    goBack(): void {
        this.router.navigate(['/app/booking/reservationlist']);
    }

    getModelValue(params: Params): Promise<void|Reservation> {
        if (params['id'])
        {
            this.upsertText = 'Edit Reservation';
            return this.reservationService.get(+params['id'])
                .catch(err => {
                    this.router.navigate(['/notfound']);       
                });
        }
        else {
            this.upsertText = 'New Reservation';
            let m: Reservation = this.getInitialValue();;
            let p: Promise<Reservation> = new Promise((resolve, reject) => resolve(m));
            return p;
        }
    }

    getInitialValue(): Reservation {
        return {
				GuestName: null,
				NumberOfGuest: 0,
				Telephone: null,
				ReservationMadeBy: null,
				ArrivalDate: null,
				DepartureDate: null,
				Id: 0,
				CreatedDate: null,
				DeletedDate: null,
				InsertNew: null
				};
    }
}