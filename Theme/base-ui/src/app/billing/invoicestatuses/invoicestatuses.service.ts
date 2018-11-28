import { Injectable }   from '@angular/core';
import { Headers, Http } from '@angular/http';
import { InvoiceStatuses }         from './invoicestatuses';
import 'rxjs/add/operator/toPromise';
import { PageResult }         from 'app/core/pagination/PageResult';

@Injectable()
export class InvoiceStatusesService {
    constructor() {
    }

    getList(): any {
		return [
					{ key: 'Open', value: 1 },
					{ key: 'Closed', value: 2 },
					{ key: 'Cancel', value: 3 },
					{ key: 'Void', value: 4 },
				];
    }
}