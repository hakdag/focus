import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'
import 'rxjs/add/operator/toPromise';
import { AppConfig } from '../../app.config';

@Injectable()
export class AuthenticationService {
    private tokenUrl: string = '';
    private signoutUrl: string = '';
    private headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
    private token = null;

    constructor(private http: Http, config: AppConfig) {
        var cfg: any = config.getConfig();
        this.tokenUrl = cfg.service.rootUrl + 'Token';
        this.signoutUrl = cfg.service.rootUrl + 'api/Account/Logout';
        
        this.token = JSON.parse(localStorage.getItem('token'));
        if (this.token != null && this.token.access_token != null)
            this.headers.append('Authorization','Bearer ' + this.token.access_token)
    }

    login(username: string, password: string) {
        return this.http.post(this.tokenUrl, "grant_type=password&username=" + username + "&password=" + password, { headers: this.headers })
            .map((response: Response) => {
                // login successful if there's a jwt token in the response
                let token = response.json();

                if (token && token.access_token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('token', JSON.stringify(token));
                }
            });
    }

    logout(): Promise<any> {
        return this.http.post(this.signoutUrl, "", { headers: this.headers })
            .toPromise()
            .then(res => {
                // remove user from local storage to log user out
                localStorage.removeItem('token');

                return res.json();
            })
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        return Promise.reject(error.message || error);
    }
}