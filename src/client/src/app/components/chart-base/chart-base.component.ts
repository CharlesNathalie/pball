import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { AppStateService } from 'src/app/app-state.service';
import { ChartBaseService } from './chart-base.service';

Chart.register(...registerables);

@Component({
  selector: 'app-chart-base',
  templateUrl: './chart-base.component.html',
  styleUrls: ['./chart-base.component.css'],
})
export class ChartBaseComponent implements OnInit, OnDestroy {

  buttonText: string = '';

  constructor(public state: AppStateService,
    public chartBaseService: ChartBaseService,
  ) { }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }
}
