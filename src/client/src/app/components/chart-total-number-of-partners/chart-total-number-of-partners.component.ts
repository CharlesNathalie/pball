import { Component, OnInit, OnDestroy, Input, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { AppStateService } from 'src/app/services/app-state.service';
import { ChartTotalNumberOfPartnersService } from 'src/app/services/chart-total-number-of-partners.service';

Chart.register(...registerables);

@Component({
  selector: 'app-chart-total-number-of-partners',
  templateUrl: './chart-total-number-of-partners.component.html',
  styleUrls: ['./chart-total-number-of-partners.component.css'],
})
export class ChartTotalNumberOfPartnersComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild('chart')
  chartRef: ElementRef = <ElementRef>{};

  myChart: Chart = <Chart>{};
  chartFileName: string = '';

  constructor(public state: AppStateService,
    public chartTotalNumberOfPartnersService: ChartTotalNumberOfPartnersService,
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.myChart = this.chartTotalNumberOfPartnersService.SetChartRef(this.chartRef);
  }

  ngOnDestroy(): void {
  }
}
