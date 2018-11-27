import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';
import { RouterModule } from '@angular/router';

import 'jquery.animate-number/jquery.animateNumber.js';
import 'jQuery-Mapael/js/jquery.mapael.js';
import 'jQuery-Mapael/js/maps/usa_states';
import 'bootstrap_calendar/bootstrap_calendar/js/bootstrap_calendar.js';

import { DashboardComponent } from './dashboard.component.ts';
import { WidgetModule } from '../layout/widget/widget.module';
import { UtilsModule } from '../layout/utils/utils.module';
import { FlotChartModule } from '../components/flot/flot.module';
import { FlotChartWidget } from './flot-chart-widget/flot-chart-widget.component';
import { LiveTileModule } from '../components/tile/tile.module';
import { Nvd3ChartModule } from '../components/nvd3/nvd3.module';
// import { RickshawChartModule } from '../components/rickshaw/rickshaw.module';
// import { GeoLocationsWidget } from './geo-locations-widget/geo-locations-widget.directive';
// import { MarketStatsWidget } from './market-stats-widget/market-stats-widget.component';
// import { BootstrapCalendar } from './bootstrap-calendar/bootstrap-calendar.component';

export const routes = [
  { path: '', component: DashboardComponent, pathMatch: 'full' }
];


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    WidgetModule,
    UtilsModule,
    LiveTileModule,
    Nvd3ChartModule,
    FlotChartModule
    // RickshawChartModule
  ],
  declarations: [
    DashboardComponent,
    FlotChartWidget
    // GeoLocationsWidget,
    // BootstrapCalendar,
    // MarketStatsWidget
  ]
})
export class DashboardModule {
  static routes = routes;
}
