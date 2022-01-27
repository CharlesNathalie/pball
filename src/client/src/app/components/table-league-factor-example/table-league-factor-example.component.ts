import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/services/app-state.service';
import { TableLeagueFactorExampleService } from '../../services/table-league-factor-example.service';

@Component({
  selector: 'app-table-league-factor-example',
  templateUrl: './table-league-factor-example.component.html',
  styleUrls: ['./table-league-factor-example.component.css']
})
export class TableLeagueFactorExampleComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public tableLeagueFactorExampleService: TableLeagueFactorExampleService) {
  }

  displayedColumns: string[] = ['Team1Scores', 'Team2Scores', 'Team1Player1Points', 'Team1Player2Points', 'Team2Player1Points', 'Team2Player2Points'];

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }
}
