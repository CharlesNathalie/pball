import { Component, OnInit, OnDestroy, Input, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { ChartGamesPlayedService } from 'src/app/services/chart-games-played.service';

import { Chart, registerables } from 'chart.js';
Chart.register(...registerables);

@Component({
  selector: 'app-chart-games-played',
  templateUrl: './chart-games-played.component.html',
  styleUrls: ['./chart-games-played.component.css'],
})
export class ChartGamesPlayedComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild('chart')
  chartRef: ElementRef = <ElementRef>{};

  myChart: Chart = <Chart>{};
  chartFileName: string = '';

  constructor(public state: AppStateService,
    public chartGamesPlayedService: ChartGamesPlayedService,
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.myChart = this.chartGamesPlayedService.DrawGamesPlayedChart(this.chartRef);
  }

  ngOnDestroy(): void {
  }
}
