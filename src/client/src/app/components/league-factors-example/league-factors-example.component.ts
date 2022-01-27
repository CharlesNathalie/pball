import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { LeaguePointExampleModel } from 'src/app/models/LeaguePointExampleModel.model';
import { AppStateService } from 'src/app/services/app-state.service';
import { LeagueFactorsExampleService } from 'src/app/services/league-factors-example.service';

@Component({
  selector: 'app-league-factors-example',
  templateUrl: './league-factors-example.component.html',
  styleUrls: ['./league-factors-example.component.css']
})
export class LeagueFactorsExampleComponent implements OnInit, OnDestroy {

  Team1Player1Level: number = 4;
  Team1Player2Level: number = 3;
  Team2Player1Level: number = 2;
  Team2Player2Level: number = 1;
  PointsToWinners: number = 3;
  PointsToLosers: number = 1;
  PlayerLevelFactor: number = 0.5;
  PercentPointsFactor: number = 0.5;

  constructor(public state: AppStateService,
    public leagueFactorsExampleService: LeagueFactorsExampleService,
    public router: Router) {
  }

  ngOnInit(): void {
    this.Recalculate();
  }

  ngOnDestroy(): void {
  }

  ReturnToPreviousPage() {
    this.router.navigate([this.state.ReturnToPage2]);
  }

  ValueChanged(changedValue: 'Team1Player1Level' | 'Team1Player2Level' | 'Team2Player1Level' | 'Team2Player2Level' | 'PointsToWinners' | 'PointsToLosers' | 'PlayerLevelFactor' | 'PercentPointsFactor', event: any) {
    let value: number = event.target.value;

    switch (changedValue) {
      case 'Team1Player1Level':
        {
          this.Team1Player1Level = value;
          this.Recalculate();
        }
        break;
      case 'Team1Player2Level':
        {
          this.Team1Player2Level = value;
          this.Recalculate();
        }
        break;
      case 'Team2Player1Level':
        {
          this.Team2Player1Level = value;
          this.Recalculate();
        }
        break;
      case 'Team2Player2Level':
        {
          this.Team2Player2Level = value;
          this.Recalculate();
        }
        break;
      case 'PointsToWinners':
        {
          this.PointsToWinners = value;
          this.Recalculate();
        }
        break;
      case 'PointsToLosers':
        {
          this.PointsToLosers = value;
          this.Recalculate();
        }
        break;
      case 'PlayerLevelFactor':
        {
          this.PlayerLevelFactor = value;
          this.Recalculate();
        }
        break;
      case 'PercentPointsFactor':
        {
          this.PercentPointsFactor = value;
          this.Recalculate();
        }
        break;
      default:
        break;
    }

  }

  private Recalculate() {
    this.state.LeaguePointExampleModelList = [];
    for (let team2scores = 0; team2scores < 10; team2scores++) {
      let leaguePointExampleModel: LeaguePointExampleModel = {
        Team1Scores: 11,
        Team2Scores: team2scores,
        Team1Player1Points: this.PointsToWinners + this.GetPointsFromPlayerLevelFactor('T1P1') + this.GetPointsFromPercentPointsFactor('T1P1', 11, team2scores),
        Team1Player2Points: this.PointsToWinners + this.GetPointsFromPlayerLevelFactor('T1P2') + this.GetPointsFromPercentPointsFactor('T1P2', 11, team2scores),
        Team2Player1Points: this.PointsToLosers + this.GetPointsFromPlayerLevelFactor('T2P1') + this.GetPointsFromPercentPointsFactor('T2P1', 11, team2scores),
        Team2Player2Points: this.PointsToLosers + this.GetPointsFromPlayerLevelFactor('T2P2') + this.GetPointsFromPercentPointsFactor('T2P2', 11, team2scores),
      };

      this.state.LeaguePointExampleModelList.push(leaguePointExampleModel);
    }

    for (let team1scores = 9; team1scores > -1; team1scores--) {
      let leaguePointExampleModel: LeaguePointExampleModel = {
        Team1Scores: team1scores,
        Team2Scores: 11,
        Team1Player1Points: this.PointsToLosers + this.GetPointsFromPlayerLevelFactor('T1P1') + this.GetPointsFromPercentPointsFactor('T1P1', team1scores, 11),
        Team1Player2Points: this.PointsToLosers + this.GetPointsFromPlayerLevelFactor('T1P2') + this.GetPointsFromPercentPointsFactor('T1P2', team1scores, 11),
        Team2Player1Points: this.PointsToWinners + this.GetPointsFromPlayerLevelFactor('T2P1') + this.GetPointsFromPercentPointsFactor('T2P1', team1scores, 11),
        Team2Player2Points: this.PointsToWinners + this.GetPointsFromPlayerLevelFactor('T2P2') + this.GetPointsFromPercentPointsFactor('T2P2', team1scores, 11),
      };

      this.state.LeaguePointExampleModelList.push(leaguePointExampleModel);
    }
  }

  private GetPointsFromPlayerLevelFactor(player: 'T1P1' | 'T1P2' | 'T2P1' | 'T2P2'): number {
    let Team1PlayerAvg: number = (+this.Team1Player1Level + +this.Team1Player2Level) / 2.0;
    let Team2PlayerAvg: number = (+this.Team2Player1Level + +this.Team2Player2Level) / 2.0;

    switch (player) {
      case 'T1P1':
        {
          let points: number = (5 - Team1PlayerAvg) * this.PlayerLevelFactor;
          return points;
        }
      case 'T1P2':
        {
          let points: number = (5 - Team1PlayerAvg) * this.PlayerLevelFactor;
          return points;
        }
      case 'T2P1':
        {
          let points: number = (5 - Team2PlayerAvg) * this.PlayerLevelFactor;
          return points;
        }
      case 'T2P2':
        {
          let points: number = (5 - Team2PlayerAvg) * this.PlayerLevelFactor;
          return points;
        }
      default:
        {
          return 0;
        }
    }
  }

  private GetPointsFromPercentPointsFactor(player: 'T1P1' | 'T1P2' | 'T2P1' | 'T2P2', T1Scores: number, T2Scores: number): number {
    let Team1PercentagePoint: number = T1Scores / (T1Scores + T2Scores);
    let Team2PercentagePoint: number = T2Scores / (T1Scores + T2Scores);

    switch (player) {
      case 'T1P1':
        {
          let points: number = Team1PercentagePoint * this.PercentPointsFactor;
          return points;
        }
      case 'T1P2':
        {
          let points: number = Team1PercentagePoint * this.PercentPointsFactor;
          return points;
        }
      case 'T2P1':
        {
          let points: number = Team2PercentagePoint * this.PercentPointsFactor;
          return points;
        }
      case 'T2P2':
        {
          let points: number = Team2PercentagePoint * this.PercentPointsFactor;
          return points;
        }
      default:
        {
          return 0;
        }
    }
  }
}
