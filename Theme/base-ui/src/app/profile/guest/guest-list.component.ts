import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params }   from '@angular/router';
import { Guest } from './guest';
import { GuestService } from './guest.service';
import { BaseListComponent } from 'app/core/BaseListComponent';

@Component({
  selector: '[guest-list]',
  templateUrl: './guest-list.template.html',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['../../tables/dynamic/tables-dynamic.style.scss', '../../ui-elements/notifications/notifications.style.scss'],
  providers: [GuestService]
})
export class GuestListComponent extends BaseListComponent<Guest> {

  constructor(protected guestService: GuestService, protected route: ActivatedRoute, protected router: Router) {
    super(guestService, route, router);
    this.sortBy = "null";
    this.deleteConfirmMessage = "Are you sure you want to delete Guest Profile?";
  }
}
