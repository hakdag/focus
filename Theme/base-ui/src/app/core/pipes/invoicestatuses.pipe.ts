import { Pipe, PipeTransform } from '@angular/core';
import { InvoiceStatuses } from "app/billing/invoicestatuses/invoicestatuses";
import { InvoiceStatusesService } from "app/billing/invoicestatuses/invoicestatuses.service";

@Pipe({
  name: 'InvoiceStatusesPipe',
  pure: false
})
export class InvoiceStatusesPipe implements PipeTransform {

  transform(key: number): string {
      var tc: InvoiceStatuses = new InvoiceStatuses();
      var invoicestatusesService: InvoiceStatusesService = new InvoiceStatusesService();
      var arr = invoicestatusesService.getList();
      return arr.filter(t => t.value === key.toString())[0].key;
  }
}
