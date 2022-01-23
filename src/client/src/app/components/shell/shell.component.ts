import { Component, OnInit, OnDestroy } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { Router } from '@angular/router';
import { AppStateService } from 'src/app/app-state.service';
import { ShellService } from './shell.service';
import { LogoffService } from '../logoff/logoff.service';
import { DataHelperService } from 'src/app/services/data/data-helper.service';

@Component({
  selector: 'app-shell',
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.css']
})
export class ShellComponent implements OnInit, OnDestroy {
  year: number = new Date().getFullYear();

  languageEnum = GetLanguageEnum();

  constructor(public state: AppStateService,
    private title: Title,
    private router: Router,
    public shellService: ShellService,
    public logoffService: LogoffService) {
  }

  ngOnInit(): void {
    this.shellService.Init(this.title, this.router);
  }

  ngOnDestroy(): void {
  }

  GetFirstLetters(name: string, numberOfLetters: number): string {
    return this.shellService.GetFirstLetters(name, numberOfLetters);
  }

  Logoff() {
    this.logoffService.Logoff();
  }

  GetContactID() {
    return this.state.User.ContactID;
  }

  NavigateToEn(lang: 'en' | 'fr') {
    this.state.ClearDemoData();
    this.state.ClearDemoLocalStorage();
    this.router.navigate([`/${lang}-CA${this.shellService.GetLastPartOfUrl(this.router)}`])
  }
}
