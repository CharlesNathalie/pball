import { Component, OnInit, OnDestroy, Input, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { AppStateService } from 'src/app/services/app-state.service';
import { ChartGamesPercentWonService } from 'src/app/services/chart-games-percent-won.service';

Chart.register(...registerables);

@Component({
  selector: 'app-chart-games-percent-won',
  templateUrl: './chart-games-percent-won.component.html',
  styleUrls: ['./chart-games-percent-won.component.css'],
})
export class ChartGamesPercentWonComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild('chart')
  chartRef: ElementRef = <ElementRef>{};

  myChart: Chart = <Chart>{};
  chartFileName: string = '';

  constructor(public state: AppStateService,
    public chartGamesPercentWonService: ChartGamesPercentWonService,
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
this.myChart = this.chartGamesPercentWonService.SetChartRef(this.chartRef);
  }

  ngOnDestroy(): void {
  }
}
