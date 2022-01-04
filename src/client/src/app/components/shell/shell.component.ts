import { Component, OnInit, OnDestroy } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { AppLanguageService } from 'src/app/services/app/app-language.service';
import { AppLoadedService } from 'src/app/services/app/app-loaded.service';
import { AppStateService } from 'src/app/services/app/app-state.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-shell',
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.css']
})
export class ShellComponent implements OnInit, OnDestroy {
  year: number = new Date().getFullYear();

  languageEnum = GetLanguageEnum();

  constructor(public appStateService: AppStateService,
    public appLanguageService: AppLanguageService,
    public appLoadedService: AppLoadedService,
    private title: Title,
    private router: Router) {
  }

  ngOnInit(): void {
    this.title.setTitle(this.appLanguageService.ShellApplicationName[this.appLanguageService.LangID ?? 0]);
    if (this.router.url.startsWith("/fr-CA"))
    {
      this.appLanguageService.SetLanguage(this.languageEnum.fr);
    }
    else
    {
      this.appLanguageService.SetLanguage(this.languageEnum.en);
    }
 }

  ngOnDestroy(): void {
  }

}
