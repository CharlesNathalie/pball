import { Component, OnInit, OnDestroy, Input, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { AppStateService } from 'src/app/services/app-state.service';
import { ChartPointsService } from 'src/app/services/chart-points.service';

Chart.register(...registerables);

@Component({
  selector: 'app-chart-points',
  templateUrl: './chart-points.component.html',
  styleUrls: ['./chart-points.component.css'],
})
export class ChartPointsComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild('chart')
  chartRef: ElementRef = <ElementRef>{};

  myChart: Chart = <Chart>{};
  chartFileName: string = '';

  constructor(public state: AppStateService,
    public chartPointsService: ChartPointsService,
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.myChart = this.chartPointsService.SetChartRef(this.chartRef);
    }, 0);
  }

  ngOnDestroy(): void {
  }
}
