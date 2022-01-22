import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppStateService } from 'src/app/app-state.service';
import { NoDataForUserService } from './no-data-for-user.service';

@Component({
  selector: 'app-no-data-for-user',
  templateUrl: './no-data-for-user.component.html',
  styleUrls: ['./no-data-for-user.component.css']
})
export class NoDataForUserComponent implements OnInit, OnDestroy {

  constructor(public state: AppStateService,
    public noDataForUserService: NoDataForUserService) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }
}
