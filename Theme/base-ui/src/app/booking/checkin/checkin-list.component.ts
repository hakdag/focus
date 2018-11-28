import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params }   from '@angular/router';
import { CheckIn } from './checkin';
import { CheckInService } from './checkin.service';
import { BaseListComponent } from 'app/core/BaseListComponent';

@Component({
  selector: '[checkin-list]',
  templateUrl: './checkin-list.template.html',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['../../tables/dynamic/tables-dynamic.style.scss', '../../ui-elements/notifications/notifications.style.scss'],
  providers: [CheckInService]
})
export class CheckInListComponent extends BaseListComponent<CheckIn> {

  constructor(protected checkinService: CheckInService, protected route: ActivatedRoute, protected router: Router) {
    super(checkinService, route, router);
    this.sortBy = "null";
    this.deleteConfirmMessage = "Are you sure you want to delete Check In?";
  }
}
