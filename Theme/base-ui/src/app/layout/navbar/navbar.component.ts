import { Component, EventEmitter, OnInit, ElementRef, Output } from '@angular/core';
import { Router } from '@angular/router';
import { AppConfig } from '../../app.config';
import { AuthenticationService } from 'app/core/auth/authentication.service';
import { MessengerBox } from 'app/core/messenger.directive';
declare var jQuery: any;

@Component({
  selector: '[navbar]',
  templateUrl: './navbar.template.html'
})
export class Navbar implements OnInit {
  @Output() toggleSidebarEvent: EventEmitter<any> = new EventEmitter();
  @Output() toggleChatEvent: EventEmitter<any> = new EventEmitter();
  $el: any;
  config: any;
  router: Router;
  private token = null;
  private userName: String;
  private messenger: MessengerBox = null;

  constructor(
    el: ElementRef,
    config: AppConfig,
    private authenticationService: AuthenticationService,
    router: Router) {
    this.$el = jQuery(el.nativeElement);
    this.config = config.getConfig();
    this.router = router;
    this.token = JSON.parse(localStorage.getItem('token'));
    if (this.token != null && this.token.userName != null)
      this.userName = this.token.userName;
  }

  toggleSidebar(state): void {
    this.toggleSidebarEvent.emit(state);
  }

  onDashboardSearch(f): void {
    this.router.navigate(['/app', 'extra', 'search'], { queryParams: { search: f.value.search } });
  }

  logout(): void {
    this.authenticationService.logout().then(res => {
      if (!res.Success)
          this.showError(res.Messages[0]);
        else
          this.router.navigate(['/login']);
    }).catch(err => this.handleError(err));
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
      this.messenger.showErrorMessage(errorMessage, function () { me.logout(); });
    }
  }

  public showError(message: string): void {
    if (this.messenger == null)
      this.messenger = new MessengerBox();
    this.messenger.showErrorMessage(message, null);
  }

  ngOnInit(): void {

    this.$el.find('.input-group-addon + .form-control').on('blur focus', function (e): void {
      jQuery(this).parents('.input-group')
      [e.type === 'focus' ? 'addClass' : 'removeClass']('focus');
    });
  }
}
