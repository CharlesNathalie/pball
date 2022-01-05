import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/app-state.service';
import { TableAllService } from './table-all.service';

@Component({
  selector: 'app-table-all',
  templateUrl: './table-all.component.html',
  styleUrls: ['./table-all.component.css']
})
export class TableAllComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public tableAllService: TableAllService) {
  }

  ngOnInit(): void {
 }

  ngOnDestroy(): void {
  }

}
