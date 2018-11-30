import { Component, OnInit, ElementRef } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { Location } from '@angular/common';
import { AppConfig } from '../../app.config';
declare var jQuery: any;

@Component({
  selector: '[sidebar]',
  templateUrl: './sidebar.template.html'
})

export class Sidebar implements OnInit {
  $el: any;
  config: any;
  router: Router;
  location: Location;
  token: any;

  constructor(config: AppConfig, el: ElementRef, router: Router, location: Location) {
    this.$el = jQuery(el.nativeElement);
    this.config = config.getConfig();
    this.router = router;
    this.location = location;
    this.token = JSON.parse(localStorage.getItem('token'));
  }

  initSidebarScroll(): void {
    let $sidebarContent = this.$el.find('.js-sidebar-content');
    if (this.$el.find('.slimScrollDiv').length !== 0) {
      $sidebarContent.slimscroll({
        destroy: true
      });
    }
    $sidebarContent.slimscroll({
      height: window.innerHeight,
      size: '4px'
    });
  }

  hasRole(role: String): boolean {
    if (this.token != null && this.token.roles != null) {
      var roles = JSON.parse(this.token.roles);
      return this.containsRole(roles, "Admin") || this.containsRole(roles, role);
    }

    return false;
  }

  containsRole(roles: any, role: String): boolean {
    return roles.filter(r => r == role).length > 0;
  }

  changeActiveNavigationItem(location): void {
    let p = location.path().split('?')[0];
    let parts:Array<string> = p.split('/');
    let finalPart = parts[parts.length - 1];
    let id = parseInt(finalPart);
    
    if (!isNaN(id))
      p = parts.splice(0, (parts.length - 1)).join("/");
    let $newActiveLink = this.$el.find('a[href*="#' + p + '"]');

    // collapse .collapse only if new and old active links belong to different .collapse
    if (!$newActiveLink.is('.active > .collapse > li > a')) {
      this.$el.find('.active .active').closest('.collapse').collapse('hide');
    }
    this.$el.find('.sidebar-nav .active').removeClass('active');

    $newActiveLink.closest('li').addClass('active')
      .parents('li').addClass('active');

    // uncollapse parent
    $newActiveLink.closest('.collapse').addClass('in').css('height', '')
      .siblings('a[data-toggle=collapse]').removeClass('collapsed');
  }

  ngAfterViewInit(): void {
    this.changeActiveNavigationItem(this.location);
  }

  ngOnInit(): void {
    jQuery(window).on('sn:resize', this.initSidebarScroll.bind(this));
    this.initSidebarScroll();

    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.changeActiveNavigationItem(this.location);
      }
    });
  }
}
