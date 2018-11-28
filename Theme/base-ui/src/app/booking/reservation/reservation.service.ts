import { Injectable }   from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Reservation }         from './reservation';
import { AppConfig } from '../../app.config';
import { BaseService } from 'app/core/BaseService';

@Injectable()
export class ReservationService extends BaseService<Reservation> {
    
    constructor(protected http: Http, config: AppConfig) {
        super(http);
        var cfg: any = config.getConfig();
        this.apiUrl = cfg.service.rootUrl + 'api/booking/reservation';
    }

    get(id: number): Promise<Reservation> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get(url, { headers: this.headers })
            .toPromise()
            .then(response => {
                var json = response.json();
                var model = json as Reservation;

				                model.ArrivalDate = new Date(json.ArrivalDate);
				                model.DepartureDate = new Date(json.DepartureDate);
				                model.CreatedDate = new Date(json.CreatedDate);
				
                return model;
            })
            .catch(super.handleError);
    }
}