import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params }   from '@angular/router';
import { Invoice } from './invoice';
import { InvoiceService } from './invoice.service';
import { BaseListComponent } from 'app/core/BaseListComponent';

@Component({
  selector: '[invoice-list]',
  templateUrl: './invoice-list.template.html',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['../../tables/dynamic/tables-dynamic.style.scss', '../../ui-elements/notifications/notifications.style.scss'],
  providers: [InvoiceService]
})
export class InvoiceListComponent extends BaseListComponent<Invoice> {

  constructor(protected invoiceService: InvoiceService, protected route: ActivatedRoute, protected router: Router) {
    super(invoiceService, route, router);
    this.sortBy = "null";
    this.deleteConfirmMessage = "Are you sure you want to delete Invoice?";
  }
}
