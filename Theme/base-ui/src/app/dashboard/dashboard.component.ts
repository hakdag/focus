import { Component, ViewEncapsulation, Input } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { AppConfig } from 'app/app.config';

import { Dashboard } from './dashboard';
import { DashboardService } from './dashboard.service';

declare var jQuery: any;
declare var Rickshaw: any;
declare var d3: any;
declare var nv: any;

@Component({
  selector: 'dashboard',
  templateUrl: './dashboard.template.html',
  styleUrls: ['./dashboard.style.scss', './widgets.style.scss', './charts.style.scss'],
  providers: [DashboardService],
  encapsulation: ViewEncapsulation.None
})
export class DashboardComponent {
  config: any;
  month: any;
  year: any;
  nvd31Chart: any;
  nvd31Data: any;
  nvd32Chart: any;
  nvd32Data: any;
  @Input()
  protected model: Dashboard;

  constructor(private service: DashboardService, config: AppConfig, protected router: Router) {
    this.config = config.getConfig();
  }

  applyNvd3Data(): void {
    /* Inspired by Lee Byron's test data generator. */
    function _stream_layers(n, m, o): Array<any> {
      if (arguments.length < 3) { o = 0; }
      function bump(a): void {
        let x = 1 / (.1 + Math.random()),
          y = 2 * Math.random() - .5,
          z = 10 / (.1 + Math.random());
        for (let i = 0; i < m; i++) {
          let w = (i / m - y) * z;
          a[i] += x * Math.exp(-w * w);
        }
      }
      return d3.range(n).map(function (): Array<Object> {
        let a = [], i;
        for (i = 0; i < m; i++) { a[i] = o + o * Math.random(); }
        for (i = 0; i < 5; i++) { bump(a); }
        return a.map(function (d, iItem): Object {
          return { x: iItem, y: Math.max(0, d) };
        });
      });
    }

    function testData(streamNames, pointCount): Array<any> {
      let now = new Date().getTime(),
        day = 1000 * 60 * 60 * 24, // milliseconds
        daysAgoCount = 60,
        daysAgo = daysAgoCount * day,
        daysAgoDate = now - daysAgo,
        pointsCount = pointCount || 45, // less for better performance
        daysPerPoint = daysAgoCount / pointsCount;
      return _stream_layers(streamNames.length, pointsCount, .1).map(function (data, i): Object {
        return {
          key: streamNames[i],
          values: data.map(function (d, j): Object {
            return {
              x: daysAgoDate + d.x * day * daysPerPoint,
              y: Math.floor(d.y * 100) // just a coefficient,
            };
          })
        };
      });
    }

    this.nvd31Chart = nv.models.lineChart()
      .useInteractiveGuideline(true)
      .margin({ left: 28, bottom: 30, right: 0 })
      .color(['#82DFD6', '#ddd']);

    this.nvd31Chart.xAxis
      .showMaxMin(false)
      .tickFormat(function (d): Object { return d3.time.format('%b %d')(new Date(d)); });

    this.nvd31Chart.yAxis
      .showMaxMin(false)
      .tickFormat(d3.format(',f'));

    this.nvd31Data = testData(['Search', 'Referral'], 50).map(function (el, i): boolean {
      el.area = true;
      return el;
    });

    this.nvd32Chart = nv.models.multiBarChart()
      .margin({ left: 28, bottom: 30, right: 0 })
      .color(['#F7653F', '#ddd']);

    this.nvd32Chart.xAxis
      .showMaxMin(false)
      .tickFormat(function (d): Object { return d3.time.format('%b %d')(new Date(d)); });

    this.nvd32Chart.yAxis
      .showMaxMin(false)
      .tickFormat(d3.format(',f'));

    this.nvd32Data = testData(['Uploads', 'Downloads'], 10).map(function (el, i): boolean {
      el.area = true;
      return el;
    });
  };

  ngOnInit(): void {
    let now = new Date();
    this.month = now.getMonth() + 1;
    this.year = now.getFullYear();

    this.applyNvd3Data();

    this.service.get()
      .then(response => this.model = response)
      .catch(err => {
        this.router.navigate(['/error']);
      });
  }
}
