import { Component, OnInit, OnDestroy, Input, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { AppStateService } from 'src/app/app-state.service';
import { ChartAllService } from './chart-all.service';

Chart.register(...registerables);

@Component({
  selector: 'app-chart-all',
  templateUrl: './chart-all.component.html',
  styleUrls: ['./chart-all.component.css'],
})
export class ChartAllComponent implements OnInit, AfterViewInit, OnDestroy {
  //@ViewChild('chart')
  //chartRef: ElementRef;

  //myChart: Chart;
  //chartFileName: string;

  constructor(public state: AppStateService,
    public chartAllService: ChartAllService,
  ) { }

  ngOnInit(): void {
    //this.chartFileName = "bonjour ChartAll"; //this.chartService.GetChartFileName();
  }

  ngAfterViewInit(): void {
    //this.myChart = this.chartService.DrawChart(this.chartRef, this.TVItemModel, this.webChartAndTableType);
  }

  ngOnDestroy(): void {
  }
}
