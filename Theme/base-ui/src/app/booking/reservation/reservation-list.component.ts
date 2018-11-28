import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params }   from '@angular/router';
import { Reservation } from './reservation';
import { ReservationService } from './reservation.service';
import { BaseListComponent } from 'app/core/BaseListComponent';

@Component({
  selector: '[reservation-list]',
  templateUrl: './reservation-list.template.html',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['../../tables/dynamic/tables-dynamic.style.scss', '../../ui-elements/notifications/notifications.style.scss'],
  providers: [ReservationService]
})
export class ReservationListComponent extends BaseListComponent<Reservation> {

  constructor(protected reservationService: ReservationService, protected route: ActivatedRoute, protected router: Router) {
    super(reservationService, route, router);
    this.sortBy = "null";
    this.deleteConfirmMessage = "Are you sure you want to delete Reservation?";
  }
}
