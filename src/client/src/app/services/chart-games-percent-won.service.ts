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
export class ChartGamesPercentWonService {
  GamesPercentWon: string[] = ['Games % won', 'Parties % gagnées'];

  constructor(public state: AppStateService,
    public sortService: SortService,
    public dataHelperService: DataHelperService) {
  }

  public DrawGamesPercentWonChart(chartRef: ElementRef): Chart {
    let chartTitle = this.GamesPercentWon[this.state.LangID];

    let labelList: string[] = [];

    let data: any;
    let config: any;

    if (this.state.DatePlayerStatModelList.length == 0) return <Chart>{};

    let GamesPercentWonList: ChartXYTextNumberModel[] = [];
    let Percentile75List: ChartXYTextNumberModel[] = [];
    let Percentile25List: ChartXYTextNumberModel[] = [];

    for (let i = 0, count = this.state.DatePlayerStatModelList.length; i < count; i++) {
      labelList.push(moment(this.state.DatePlayerStatModelList[i].Date).format('yyyy-MM-DD'));
    }

    for (let i = 0, count = this.state.DatePlayerStatModelList.length - 1; i < count; i++) {
      let Dt = moment(this.state.DatePlayerStatModelList[i].Date).format('yyyy-MM-DD');
      if (this.state.DemoVisible) {
        let gamePlayed: number = this.state.DatePlayerStatModelList[i].PlayerStatModelList.find(c => c.PlayerID = this.state.DemoUser.ContactID)?.GamesPlayed ?? 1;
        let gameWon: number = this.state.DatePlayerStatModelList[i].PlayerStatModelList.find(c => c.PlayerID = this.state.DemoUser.ContactID)?.GamesWon ?? 0;
        // if (i < 5)
        // {
        //   GamesPercentWonList.push({ x: Dt, y: 50 });
        // }
        // else{
          GamesPercentWonList.push({ x: Dt, y: (100 * gameWon / gamePlayed) });
        //}
      }
      else {
        let gamePlayed: number = this.state.DatePlayerStatModelList[i].PlayerStatModelList.find(c => c.PlayerID = this.state.User.ContactID)?.GamesPlayed ?? 0;
        let gameWon: number = this.state.DatePlayerStatModelList[i].PlayerStatModelList.find(c => c.PlayerID = this.state.User.ContactID)?.GamesWon ?? 0;
        GamesPercentWonList.push({ x: Dt, y: (100 * gameWon / gamePlayed) });
      }

      this.state.PlayerStatModelSortProp = 'WinningPercentage';
      this.state.PlayerStatModelSortAscDesc = AscDescEnum.Ascending;
      let playerStatModelList: PlayerStatModel[] = this.sortService.SortPlayerStatModelList(this.state.DatePlayerStatModelList[i].PlayerStatModelList);
      let p75: number = 100 * playerStatModelList[Math.floor(75 / 100 * playerStatModelList.length - 1)].GamesWon / playerStatModelList[Math.floor(75 / 100 * playerStatModelList.length - 1)].GamesPlayed;
      let p25: number = 100 * playerStatModelList[Math.floor(25 / 100 * playerStatModelList.length - 1)].GamesWon / playerStatModelList[Math.floor(25 / 100 * playerStatModelList.length - 1)].GamesPlayed;

      // if (i < 5)
      // {
      //   Percentile75List.push({ x: Dt, y: 50 });
      //   Percentile25List.push({ x: Dt, y: 50 });
      //   }
      // else{
        Percentile75List.push({ x: Dt, y: p75 });
        Percentile25List.push({ x: Dt, y: p25 });
        //}
    }

    data = this.GetGamesPercentWonData(labelList, GamesPercentWonList, Percentile75List, Percentile25List);
    config = this.GetGamesPercentWonConfig(data, chartTitle);

    return new Chart(<HTMLCanvasElement>chartRef.nativeElement, <ChartConfiguration>config);
  }

  private GetGamesPercentWonConfig(data: any, chartTitle: string) {
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

  private GetGamesPercentWonData(labelList: string[],
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
