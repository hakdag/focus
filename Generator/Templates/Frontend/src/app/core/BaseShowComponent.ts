import { Component, ViewEncapsulation, Input, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params }   from '@angular/router';
import { BaseService } from './BaseService';
import { BaseModel } from './BaseModel';
import { ResponseResult } from './ResponseResult';
import { InstanceLoader } from './InstanceLoader';
import { MessengerBox } from './messenger.directive';

@Component({
})
export abstract class BaseShowComponent<TModel extends BaseModel> implements OnInit {
    @Input()
    protected model: TModel;
    protected responseResult: ResponseResult;
    private messenger: MessengerBox = null;
    
    constructor(protected service: BaseService<TModel>, protected route: ActivatedRoute, protected router: Router) { }

    ngOnInit(): void {

        this.route.params
            .switchMap((params: Params) => {
                return this.getModelValue(params);
            })
            .subscribe(m => {
                this.model = m as TModel;
                this.subscribeServices();
            });
    }

    abstract subscribeServices(): void;
    abstract goBack(): void;
    abstract getModelValue(params: Params): Promise<void|TModel>;

    public handleError(error: any, retryFunc = null): void {
        if (error.status == 401)
            this.router.navigate(['/unauthorized']);
        else {
            if (this.messenger == null)
                this.messenger = new MessengerBox();
            let me = this;
            let errorMessage = "An unexpected error occurred.";
            if (error.status == 404)
                errorMessage = "Not found."
            this.messenger.showErrorMessage(errorMessage, function () {
                if (retryFunc != null)
                    retryFunc();
                else
                    me.ngOnInit();
            });

        }
    }
}