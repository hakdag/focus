import { Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'unauthorized',
  styleUrls: [ './unauthorized.style.scss' ],
  templateUrl: './unauthorized.template.html',
  encapsulation: ViewEncapsulation.None,
  host: {
    class: 'unauthorized-page app'
  },
})
export class UnAuthorizedComponent {
  router: Router;

  constructor(router: Router) {
    this.router = router;
  }
}
