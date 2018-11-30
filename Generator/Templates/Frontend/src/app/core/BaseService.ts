import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { PageResult } from './pagination/PageResult';
import { ResponseResult } from './ResponseResult';
import { BaseModel } from './BaseModel';

@Injectable()
export class BaseService<TModel extends BaseModel> {
    protected apiUrl: string = '';
    protected headers = new Headers({ 'Content-Type': 'application/json' });

    constructor(protected http: Http) {
        var token = JSON.parse(localStorage.getItem('token'));
        if (token != null && token.access_token != null)
            this.headers.append('Authorization', 'Bearer ' + token.access_token)
    }

    get(id: number): Promise<TModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get(url, { headers: this.headers })
            .toPromise()
            .then(response => {
                var json = response.json();
                var model = json as TModel;
                model.CreatedDate = new Date(json.CreatedDate);
                return model;
            })
            .catch(this.handleError);
    }

    getList(filterQuery: string = '', startIndex: number = 0, rowsOnPage: number = 100, sortBy: string = '', sortOrder: string = ''): Promise<PageResult> {
        const url = `${this.apiUrl}/?filterQuery=${filterQuery}&startIndex=${startIndex}&rowsOnPage=${rowsOnPage}&sortBy=${sortBy}&sortOrder=${sortOrder}`;
        return this.http.get(url, { headers: this.headers })
            .toPromise()
            .then(response => {
                return response.json();
            })
            .catch(this.handleError);
    }

    delete(model: TModel): Promise<ResponseResult> {
        const url = `${this.apiUrl}/${model.Id}`;
        return this.http.delete(url, { headers: this.headers })
            .toPromise()
            .then(res => res.json())
            .catch(this.handleError);
    }

    create(model: TModel): Promise<ResponseResult> {
        return this.http.post(this.apiUrl, JSON.stringify(model), { headers: this.headers })
            .toPromise()
            .then(res => res.json())
            .catch(this.handleError);
    }

    update(model: TModel): Promise<ResponseResult> {
        const url = `${this.apiUrl}/${model.Id}`;
        return this.http.put(url, JSON.stringify(model), { headers: this.headers })
            .toPromise()
            .then(res => res.json())
            .catch(this.handleError);
    }

    protected handleError(error: any): Promise<any> {
        return Promise.reject(error.message || error);
    }
}