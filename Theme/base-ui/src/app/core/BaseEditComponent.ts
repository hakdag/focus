import { Component, ViewEncapsulation, Input, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { BaseService } from './BaseService';
import { BaseModel } from './BaseModel';
import { ResponseResult } from './ResponseResult';
import { MessengerBox } from './messenger.directive';

@Component({
})
export abstract class BaseEditComponent<TModel extends BaseModel> implements OnInit {
    protected upsertText: string;
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
                if (this.model != null)
                    this.subscribeServices();
            });
    }

    abstract subscribeServices(): void;
    abstract goBack(): void;
    abstract getModelValue(params: Params): Promise<void | TModel>;

    cancel(isDirty: boolean): void {
        if (isDirty) {
            if (confirm("Değişiklikleri kaydetmeden çıkmak istediğinizden emin misiniz?")) {
                this.goBack();
            }
        } else {
            this.goBack();
        }
    }

    save(model: TModel, isValid: boolean) {
        let me = this;
        let res: Promise<ResponseResult>;
        if (model.Id > 0)
            res = this.service.update(model);
        else
            res = this.service.create(model);

        res.then(res => {
            this.responseResult = res;
            if (res.Success)
                this.goBack();
        }).catch(err => this.handleError(err, function () { me.save(model, isValid); }));
    }

    public handleError(error: any, retryFunc = null): void {
        if (error.status == 401)
            this.router.navigate(['/unauthorized']);
            
        if (this.messenger == null)
            this.messenger = new MessengerBox();
        let me = this;
        let errorMessage = "Beklenmeyen bir hata oluştu.";
        if (error.status == 404)
            errorMessage = "Bulunamadı."
        this.messenger.showErrorMessage(errorMessage, function () {
            if (retryFunc != null)
                retryFunc();
            else
                me.ngOnInit();
        });
    }
}
