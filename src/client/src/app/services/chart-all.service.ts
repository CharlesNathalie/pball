import { Injectable } from '@angular/core';
import { Chart, registerables}  from 'chart.js';
import { AppStateService } from 'src/app/services/app-state.service';

Chart.register(...registerables);

@Injectable({
  providedIn: 'root'
})
export class ChartAllService {
  ChartAllSomething: string[] = ['Chart all something', 'Chart all something (fr)'];

  constructor(public state: AppStateService) {
  }

}
