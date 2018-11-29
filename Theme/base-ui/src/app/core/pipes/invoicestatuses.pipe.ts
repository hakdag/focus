import { Pipe, PipeTransform } from '@angular/core';
import { InvoiceStatuses } from "app/billing/invoicestatuses/invoicestatuses";
import { InvoiceStatusesService } from "app/billing/invoicestatuses/invoicestatuses.service";

@Pipe({
  name: 'InvoiceStatusesPipe',
  pure: false
})
export class InvoiceStatusesPipe implements PipeTransform {

  transform(key: number): string {
    if (key == null)
      return "";

      var invoicestatusesService: InvoiceStatusesService = new InvoiceStatusesService();
      var arr = invoicestatusesService.getList();
      var res = arr.filter(t => t.value === key);
      if (res != null)
        return res[0].key;
      return "";
    }
}
