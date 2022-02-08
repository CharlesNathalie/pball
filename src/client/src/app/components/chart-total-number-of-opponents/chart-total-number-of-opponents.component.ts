import { Component, OnInit, OnDestroy, Input, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { AppStateService } from 'src/app/services/app-state.service';
import { ChartTotalNumberOfOpponentsService } from 'src/app/services/chart-total-number-of-opponents.service';

Chart.register(...registerables);

@Component({
  selector: 'app-chart-total-number-of-opponents',
  templateUrl: './chart-total-number-of-opponents.component.html',
  styleUrls: ['./chart-total-number-of-opponents.component.css'],
})
export class ChartTotalNumberOfOpponentsComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild('chart')
  chartRef: ElementRef = <ElementRef>{};

  myChart: Chart = <Chart>{};
  chartFileName: string = '';

  constructor(public state: AppStateService,
    public chartTotalNumberOfOpponentsService: ChartTotalNumberOfOpponentsService,
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.chartTotalNumberOfOpponentsService.SetChartRef(this.chartRef);
    this.myChart = this.chartTotalNumberOfOpponentsService.DrawTotalNumberOfOpponentsChart();
  }

  ngOnDestroy(): void {
  }
}
