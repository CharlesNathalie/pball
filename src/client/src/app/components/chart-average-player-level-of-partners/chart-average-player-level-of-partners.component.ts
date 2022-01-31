import { Component, OnInit, OnDestroy, Input, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { AppStateService } from 'src/app/services/app-state.service';
import { ChartAveragePlayerLevelOfPartnersService } from 'src/app/services/chart-average-player-level-of-partners.service';

Chart.register(...registerables);

@Component({
  selector: 'app-chart-average-player-level-of-partners',
  templateUrl: './chart-average-player-level-of-partners.component.html',
  styleUrls: ['./chart-average-player-level-of-partners.component.css'],
})
export class ChartAveragePlayerLevelOfPartnersComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild('chart')
  chartRef: ElementRef = <ElementRef>{};

  myChart: Chart = <Chart>{};
  chartFileName: string = '';

  constructor(public state: AppStateService,
    public chartAveragePlayerLevelOfPartnersService: ChartAveragePlayerLevelOfPartnersService,
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.myChart = this.chartAveragePlayerLevelOfPartnersService.DrawAveragePlayerLevelOfPartnersChart(this.chartRef);
  }

  ngOnDestroy(): void {
  }
}
