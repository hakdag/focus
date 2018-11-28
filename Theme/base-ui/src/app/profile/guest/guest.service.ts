import { Injectable }   from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Guest }         from './guest';
import { AppConfig } from '../../app.config';
import { BaseService } from 'app/core/BaseService';

@Injectable()
export class GuestService extends BaseService<Guest> {
    
    constructor(protected http: Http, config: AppConfig) {
        super(http);
        var cfg: any = config.getConfig();
        this.apiUrl = cfg.service.rootUrl + 'api/profile/guest';
    }

    get(id: number): Promise<Guest> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get(url, { headers: this.headers })
            .toPromise()
            .then(response => {
                var json = response.json();
                var model = json as Guest;

				                model.DateOfBirth = new Date(json.DateOfBirth);
				                model.CreatedDate = new Date(json.CreatedDate);
				
                return model;
            })
            .catch(super.handleError);
    }
}