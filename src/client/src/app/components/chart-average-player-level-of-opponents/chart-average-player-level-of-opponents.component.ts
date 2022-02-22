import { Component, OnInit, OnDestroy, Input, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { AppStateService } from 'src/app/services/app-state.service';
import { ChartAveragePlayerLevelOfOpponentsService } from 'src/app/services/chart-average-player-level-of-opponents.service';

Chart.register(...registerables);

@Component({
  selector: 'app-chart-average-player-level-of-opponents',
  templateUrl: './chart-average-player-level-of-opponents.component.html',
  styleUrls: ['./chart-average-player-level-of-opponents.component.css'],
})
export class ChartAveragePlayerLevelOfOpponentsComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild('chart')
  chartRef: ElementRef = <ElementRef>{};

  myChart: Chart = <Chart>{};
  chartFileName: string = '';

  constructor(public state: AppStateService,
    public chartAveragePlayerLevelOfOpponentsService: ChartAveragePlayerLevelOfOpponentsService,
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.myChart = this.chartAveragePlayerLevelOfOpponentsService.SetChartRef(this.chartRef);
    }, 0);
  }

  ngOnDestroy(): void {
  }
}
