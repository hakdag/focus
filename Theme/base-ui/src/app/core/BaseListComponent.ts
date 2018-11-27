import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { BaseService } from './BaseService';
import { BaseModel } from './BaseModel';
import { MessengerBox } from './messenger.directive';

@Component({
})
export class BaseListComponent<TModel extends BaseModel> implements OnInit {
  protected data: TModel[] = [];
  public filterQuery = "";
  public rowsOnPage = 10;
  public sortBy = "";
  public sortOrder = "desc";
  public activePage = 1;
  public itemsTotal = 0;
  protected deleteConfirmMessage = "Silmek istediğinizden emin misiniz?";
  private messenger: MessengerBox = null;

  constructor(protected service: BaseService<TModel>, protected route: ActivatedRoute, protected router: Router) { }

  ngOnInit(): void {
    this.loadData();
  }

  public loadData(): void {
    let startIndex: number = (this.activePage - 1) * this.rowsOnPage;
    this.service.getList(this.filterQuery, startIndex, this.rowsOnPage, this.sortBy, this.sortOrder).then(pr => {
      this.data = pr.Items;
      this.itemsTotal = pr.AllItems;
    }).catch(err => this.handleError(err));
  }

  delete(model: TModel): void {
    if (confirm(this.deleteConfirmMessage)) {
      this.service.delete(model).then(res => {
        if (!res.Success)
          this.showError(<string>res.Messages[0]);
        else
          this.loadData();
      }).catch(err => this.handleError(err));
    }
  }

  public onSortOrder(event) {
    this.loadData();
  }

  public filter(event): void {
    if (this.filterQuery.length == 0 || this.filterQuery.length > 2) {
      this.loadData();
    }
  }

  public onPageChange(event) {
    this.rowsOnPage = event.rowsOnPage;
    this.activePage = event.activePage;
    this.loadData();
  }

  public showError(message: string): void {
    if (this.messenger == null)
      this.messenger = new MessengerBox();
    this.messenger.showErrorMessage(message, null);
  }

  public handleError(error: any): void {
    if (error.status == 401)
      this.router.navigate(['/unauthorized']);
    else {
      if (this.messenger == null)
        this.messenger = new MessengerBox();
      let me = this;
      let errorMessage = "Beklenmeyen bir hata oluştu.";
      if (error.status == 404)
        errorMessage = "Bulunamadı."
      this.messenger.showErrorMessage(errorMessage, function () { me.ngOnInit(); });
    }
  }
}