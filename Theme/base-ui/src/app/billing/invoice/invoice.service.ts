import { Injectable }   from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Invoice }         from './invoice';
import { AppConfig } from '../../app.config';
import { BaseService } from 'app/core/BaseService';

@Injectable()
export class InvoiceService extends BaseService<Invoice> {
    
    constructor(protected http: Http, config: AppConfig) {
        super(http);
        var cfg: any = config.getConfig();
        this.apiUrl = cfg.service.rootUrl + 'api/billing/invoice';
    }

    get(id: number): Promise<Invoice> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get(url, { headers: this.headers })
            .toPromise()
            .then(response => {
                var json = response.json();
                var model = json as Invoice;

				                model.DateIn = new Date(json.DateIn);
				                model.DateOut = new Date(json.DateOut);
				                model.CreatedDate = new Date(json.CreatedDate);
				
                return model;
            })
            .catch(super.handleError);
    }
}