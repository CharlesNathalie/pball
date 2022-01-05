import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/app-state.service';
import { TablePlayerService } from './table-player.service';

@Component({
  selector: 'app-table-player',
  templateUrl: './table-player.component.html',
  styleUrls: ['./table-player.component.css']
})
export class TablePlayerComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public tablePlayerService: TablePlayerService) {
  }

  ngOnInit(): void {
 }

  ngOnDestroy(): void {
  }

}
