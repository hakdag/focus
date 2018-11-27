import { Component, Input } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { AppConfig } from '../../app.config';
declare var jQuery: any;

import { FlotChart, FlotChartItem } from '../dashboard';
import { DashboardService } from '../dashboard.service';

@Component({
  selector: '[flot-chart-widget]',
  templateUrl: './flot-chart-widget.template.html',
})
export class FlotChartWidget {
  configFn: any;
  config: any;
  @Input()
  protected model: FlotChart;

  constructor(private service: DashboardService, config: AppConfig, protected router: Router) {
    this.configFn = config;
    this.config = config.getConfig();
  }

  ngOnInit(): void {
    this.service.getCharts()
      .then(response => {
        this.model = response;
      })
      .catch(err => {
        this.router.navigate(['/error']);
      });
    this.generateRandomData([{
      label: 'Visitors', color: this.configFn.darkenColor(this.config.settings.colors['gray-lighter'], .05)
    }, {
      label: 'Charts', color: this.config.settings.colors['brand-danger']
    }]);
  }

  generateRandomData(labels): Array<any> {
    function random(): number {
      return (Math.floor(Math.random() * 30)) + 10;
    }

    let data = [],
      maxValueIndex = 5;

    for (let i = 0; i < labels.length; i++) {
      let randomSeries = [];
      for (let j = 0; j < 25; j++) {
        randomSeries.push([j, Math.floor(maxValueIndex * j) + random()]);
      }
      maxValueIndex--;
      data.push({
        data: randomSeries,
        showLabels: true,
        label: labels[i].label,
        labelPlacement: 'below',
        canvasRender: true,
        cColor: 'red',
        color: labels[i].color
      });
    }
    //console.log(data);
    
    return data;
  };
}
