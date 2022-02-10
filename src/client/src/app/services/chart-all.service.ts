import { Injectable } from '@angular/core';
import { AppStateService } from './app-state.service';
import { ChartAveragePlayerLevelOfOpponentsService } from './chart-average-player-level-of-opponents.service';
import { ChartAveragePlayerLevelOfPartnersService } from './chart-average-player-level-of-partners.service';
import { ChartGamesPercentWonService } from './chart-games-percent-won.service';
import { ChartGamesPlayedService } from './chart-games-played.service';
import { ChartGamesWonService } from './chart-games-won.service';
import { ChartPointsService } from './chart-points.service';
import { ChartTotalNumberOfOpponentsService } from './chart-total-number-of-opponents.service';
import { ChartTotalNumberOfPartnersService } from './chart-total-number-of-partners.service';

@Injectable({
  providedIn: 'root'
})
export class ChartAllService {

  constructor(public state: AppStateService,
    public chartAveragePlayerLevelOfOpponentsService: ChartAveragePlayerLevelOfOpponentsService,
    public chartAveragePlayerLevelOfPartnersService: ChartAveragePlayerLevelOfPartnersService,
    public chartGamesPercentWonService: ChartGamesPercentWonService,
    public chartGamesPlayedService: ChartGamesPlayedService,
    public chartGamesWon: ChartGamesWonService,
    public chartPointsService: ChartPointsService,
    public chartTotalNumberOfOpponentsService: ChartTotalNumberOfOpponentsService,
    public chartTotalNumberOfPartnersService: ChartTotalNumberOfPartnersService) {
  }

  RedrawDrawAllCharts()
  {
    if (Object.keys(this.chartAveragePlayerLevelOfOpponentsService.chartRef).length) {
      this.chartAveragePlayerLevelOfOpponentsService.DrawAveragePlayerLevelOfOpponentsChart();
    }

    if (Object.keys(this.chartAveragePlayerLevelOfPartnersService.chartRef).length) {
      this.chartAveragePlayerLevelOfPartnersService.DrawAveragePlayerLevelOfPartnersChart()
    }

    if (Object.keys(this.chartGamesPercentWonService.chartRef).length) {
      this.chartGamesPercentWonService.DrawGamesPercentWonChart()
    }

    if (Object.keys(this.chartGamesPlayedService.chartRef).length) {
      this.chartGamesPlayedService.DrawGamesPlayedChart()
    }

    if (Object.keys(this.chartGamesWon.chartRef).length) {
      this.chartGamesWon.DrawGamesWonChart()
    }

    if (Object.keys(this.chartPointsService.chartRef).length) {
      this.chartPointsService.DrawPointsChart()
    }

    if (Object.keys(this.chartTotalNumberOfOpponentsService.chartRef).length) {
      this.chartTotalNumberOfOpponentsService.DrawTotalNumberOfOpponentsChart()
    }

    if (Object.keys(this.chartTotalNumberOfPartnersService.chartRef).length) {
      this.chartTotalNumberOfPartnersService.DrawTotalNumberOfPartnersChart()
    }
  }
}
