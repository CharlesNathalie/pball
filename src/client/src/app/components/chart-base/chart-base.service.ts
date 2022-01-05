import { Injectable } from '@angular/core';
import { Chart, registerables}  from 'chart.js';
import { AppStateService } from 'src/app/app-state.service';

Chart.register(...registerables);
@Injectable({
  providedIn: 'root'
})
export class ChartBaseService {
  ChartBaseSomething: string[] = ['Chart Base something', 'Chart base something (fr)'];

  constructor(public state: AppStateService) {
  }

}
