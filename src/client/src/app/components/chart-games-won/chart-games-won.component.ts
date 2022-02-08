import { Component, OnInit, OnDestroy, Input, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { AppStateService } from 'src/app/services/app-state.service';
import { ChartGamesWonService } from 'src/app/services/chart-games-won.service';

Chart.register(...registerables);

@Component({
  selector: 'app-chart-games-won',
  templateUrl: './chart-games-won.component.html',
  styleUrls: ['./chart-games-won.component.css'],
})
export class ChartGamesWonComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild('chart')
  chartRef: ElementRef = <ElementRef>{};

  myChart: Chart = <Chart>{};
  chartFileName: string = '';

  constructor(public state: AppStateService,
    public chartWonService: ChartGamesWonService,
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.chartWonService.SetChartRef(this.chartRef);
    this.myChart = this.chartWonService.DrawGamesWonChart();
  }

  ngOnDestroy(): void {
  }
}
