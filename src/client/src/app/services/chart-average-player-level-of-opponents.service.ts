import { ElementRef, Injectable } from '@angular/core';
import { Chart, ChartConfiguration, registerables } from 'chart.js';
import { AppStateService } from 'src/app/services/app-state.service';
import { ChartXYTextNumberModel } from '../models/ChartXYTextNumberModel.model';
import * as moment from 'moment';
import { PlayerStatModel } from '../models/PlayerStatModel.model';
import { SortService } from './sort.service';
import { AscDescEnum } from '../enums/AscDescEnum';
import { DataHelperService } from './data-helper.service';

Chart.register(...registerables);

@Injectable({
  providedIn: 'root'
})
export class ChartAveragePlayerLevelOfOpponentsService {
  AveragePlayerLevelOfOpponents: string[] = ['Average player level of opponents', 'Moyenne du niveau de joueur comme opposants'];

  constructor(public state: AppStateService,
    public sortService: SortService,
    public dataHelperService: DataHelperService) {
  }

  public DrawAveragePlayerLevelOfOpponentsChart(chartRef: ElementRef): Chart {
    let chartTitle = this.AveragePlayerLevelOfOpponents[this.state.LangID];

    let labelList: string[] = [];

    let data: any;
    let config: any;

    if (this.state.DatePlayerStatModelList.length == 0) return <Chart>{};

    let AveragePlayerLevelOfOpponentsList: ChartXYTextNumberModel[] = [];
    let Percentile75List: ChartXYTextNumberModel[] = [];
    let Percentile25List: ChartXYTextNumberModel[] = [];

    for (let i = 0, count = this.state.DatePlayerStatModelList.length; i < count; i++) {
      labelList.push(moment(this.state.DatePlayerStatModelList[i].Date).format('yyyy-MM-DD'));
    }

    for (let i = 0, count = this.state.DatePlayerStatModelList.length - 1; i < count; i++) {
      let Dt = moment(this.state.DatePlayerStatModelList[i].Date).format('yyyy-MM-DD');
      if (this.state.DemoVisible) {
        AveragePlayerLevelOfOpponentsList.push({ x: Dt, y: this.state.DatePlayerStatModelList[i].PlayerStatModelList.find(c => c.PlayerID = this.state.DemoUser.ContactID)?.AveragePlayerLevelOfOpponents ?? 0 });
      }
      else {
        AveragePlayerLevelOfOpponentsList.push({ x: Dt, y: this.state.DatePlayerStatModelList[i].PlayerStatModelList.find(c => c.PlayerID = this.state.User.ContactID)?.AveragePlayerLevelOfOpponents ?? 0 });
      }

      this.state.PlayerStatModelSortProp = 'AveragePlayerLevelOfOpponents';
      this.state.PlayerStatModelSortAscDesc = AscDescEnum.Ascending;
      let playerStatModelList: PlayerStatModel[] = this.sortService.SortPlayerStatModelList(this.state.DatePlayerStatModelList[i].PlayerStatModelList);
      let p75: number = playerStatModelList[Math.floor(75 / 100 * playerStatModelList.length - 1)].AveragePlayerLevelOfOpponents;
      let p25: number = playerStatModelList[Math.floor(25 / 100 * playerStatModelList.length - 1)].AveragePlayerLevelOfOpponents;

      Percentile75List.push({ x: Dt, y: p75 });
      Percentile25List.push({ x: Dt, y: p25 });
    }

    data = this.GetAveragePlayerLevelOfOpponentsData(labelList, AveragePlayerLevelOfOpponentsList, Percentile75List, Percentile25List);
    config = this.GetAveragePlayerLevelOfOpponentsConfig(data, chartTitle);

    return new Chart(<HTMLCanvasElement>chartRef.nativeElement, <ChartConfiguration>config);
  }

  private GetAveragePlayerLevelOfOpponentsConfig(data: any, chartTitle: string) {
    return {
      type: 'bar',
      data,
      options: {
        plugins: {
          title: {
            display: true,
            text: chartTitle,
          },
        },
        responsive: true,
        maintainAspectRatio: false,
        scales: {
          y: {
            type: 'linear',
            display: true,
            position: 'left',
          },
          xAxes: [{
            type: 'time',
          }]
        }
      }
    };
  }

  private GetAveragePlayerLevelOfOpponentsData(labelList: string[],
    GamesPlayedList: ChartXYTextNumberModel[],
    Percentile75List: ChartXYTextNumberModel[],
    Percentile25List: ChartXYTextNumberModel[]) {
    let fullName: string = '';
    if (this.state.DemoVisible) {
      fullName = this.state.DemoUser.LastName;
    }
    else {
      fullName = this.state.User.LastName;
    }
    return {
      labels: labelList,
      datasets: [{
        label: fullName,
        backgroundColor: 'rgb(0, 255, 0)',
        borderColor: 'rgb(0, 255, 0)',
        data: GamesPlayedList,
        yAxisID: 'y',
        stack: 'combined',
        type: 'line',
      },
      {
        label: '75 %',
        backgroundColor: 'rgb(0, 0, 255)',
        borderColor: 'rgb(0, 0, 255)',
        data: Percentile75List,
        yAxisID: 'y',
        stack: 'combined',
        type: 'line',
      },
      {
        label: '25%',
        backgroundColor: 'rgb(255, 0, 0)',
        borderColor: 'rgb(255, 0, 0)',
        data: Percentile25List,
        yAxisID: 'y',
        stack: 'combined',
        type: 'line',
      }]
    };
  }
}
