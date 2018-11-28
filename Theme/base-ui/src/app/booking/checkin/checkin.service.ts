import { Injectable }   from '@angular/core';
import { Headers, Http } from '@angular/http';
import { CheckIn }         from './checkin';
import { AppConfig } from '../../app.config';
import { BaseService } from 'app/core/BaseService';

@Injectable()
export class CheckInService extends BaseService<CheckIn> {
    
    constructor(protected http: Http, config: AppConfig) {
        super(http);
        var cfg: any = config.getConfig();
        this.apiUrl = cfg.service.rootUrl + 'api/booking/checkin';
    }

    get(id: number): Promise<CheckIn> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get(url, { headers: this.headers })
            .toPromise()
            .then(response => {
                var json = response.json();
                var model = json as CheckIn;

				                model.ArrivalDate = new Date(json.ArrivalDate);
				                model.DepartureDate = new Date(json.DepartureDate);
				                model.CreatedDate = new Date(json.CreatedDate);
				
                return model;
            })
            .catch(super.handleError);
    }
}