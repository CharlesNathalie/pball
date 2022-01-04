import { Component, OnDestroy, OnInit } from '@angular/core';
import { AppLanguageService } from './services/app/app-language.service';
import { AppLoadedService } from './services/app/app-loaded.service';
import { AppStateService } from './services/app/app-state.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {

  constructor(private appStateService: AppStateService,
    private appLanguageService: AppLanguageService,
    private appLoadedService: AppLoadedService) { }


  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }


}
