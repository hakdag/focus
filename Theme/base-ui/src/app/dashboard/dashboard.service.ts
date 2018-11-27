import { Injectable }   from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Dashboard, FlotChart } from './dashboard';
import { AppConfig } from 'app/app.config';

@Injectable()
export class DashboardService {
    protected apiUrl: string = '';
    protected headers = new Headers({ 'Content-Type': 'application/json' });
    
    constructor(protected http: Http, config: AppConfig) {
        var token = JSON.parse(localStorage.getItem('token'));
        if (token != null && token.access_token != null)
            this.headers.append('Authorization', 'Bearer ' + token.access_token);

        var cfg: any = config.getConfig();
        this.apiUrl = cfg.service.rootUrl + 'api/dashboard/dashboard';
    }

    get(): Promise<Dashboard> {
        return this.http.get(this.apiUrl, { headers: this.headers })
            .toPromise()
            .then(response => {
                var json = response.json();
                var model = json as Dashboard;
                return model;
            })
            .catch(this.handleError);
    }

    getCharts(): Promise<FlotChart> {
        return this.http.get(this.apiUrl + '/Charts', { headers: this.headers })
            .toPromise()
            .then(response => {
                var json = response.json();
                var model = json as FlotChart;
                return model;
            })
            .catch(this.handleError);
    }

    protected handleError(error: any): Promise<any> {
        return Promise.reject(error.message || error);
    }
}