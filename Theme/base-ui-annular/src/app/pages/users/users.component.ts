import { Component, OnInit, ViewEncapsulation, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material';
import { AppSettings } from '../../app.settings';
import { Settings } from '../../app.settings.model';
import { User } from './user.model';
import { UsersService } from './users.service';
import { UserDialogComponent } from './user-dialog/user-dialog.component';
import { DatatableComponent } from '@swimlane/ngx-datatable/release/components/datatable.component';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [UsersService]
})
export class UsersComponent implements OnInit {
    @ViewChild(DatatableComponent) table: DatatableComponent;
    public selected = [];
    public users: User[];
    public searchText: string;
    public page: any;
    public settings: Settings;
    public showSearch = false;
    public viewType = 'grid';
    constructor(
    public appSettings: AppSettings,
    public dialog: MatDialog,
    public usersService: UsersService
  ) {
    this.settings = this.appSettings.settings;
  }

  ngOnInit() {
    this.getUsers();
  }

  public getUsers(): void {
    // for show spinner each time
    this.users = null;
    this.usersService.getUsers().subscribe(users => (this.users = users));
  }
  public addUser(user: User) {
    this.usersService.addUser(user).subscribe(() => this.getUsers());
  }
  public updateUser(user: User) {
    this.usersService.updateUser(user).subscribe(() => this.getUsers());
  }
  public deleteUser(user: User) {
    this.usersService.deleteUser(user.id).subscribe(() => this.getUsers());
  }

  public changeView(viewType) {
    this.viewType = viewType;
    this.showSearch = false;
  }

  public onPageChanged(event) {
    this.page = event;
    this.getUsers();
    document.getElementById('main').scrollTop = 0;
  }

  public openUserDialog(user) {
    const dialogRef = this.dialog.open(UserDialogComponent, {
      data: user
    });
    dialogRef.afterClosed().subscribe(u => {
      if (u) {
        u.id ? this.updateUser(u) : this.addUser(u);
      }
    });
    this.showSearch = false;
  }
}
