import { Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'notfound',
  styleUrls: [ './notfound.style.scss' ],
  templateUrl: './notfound.template.html',
  encapsulation: ViewEncapsulation.None,
  host: {
    class: 'notfound-page app'
  },
})
export class NotFoundComponent {
  router: Router;

  constructor(router: Router) {
    this.router = router;
  }
}
