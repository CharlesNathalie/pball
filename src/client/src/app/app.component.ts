import { Component, OnDestroy, OnInit } from '@angular/core';
import { AppStateService } from './app-state.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {

  constructor(private state: AppStateService) { }


  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }


}
