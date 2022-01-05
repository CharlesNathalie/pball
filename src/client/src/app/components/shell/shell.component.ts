import { Component, OnInit, OnDestroy } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { Router } from '@angular/router';
import { AppStateService } from 'src/app/app-state.service';
import { ShellService } from './shell.service';

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
    public shellService: ShellService) {
  }

  ngOnInit(): void {
    this.shellService.init(this.title, this.router);
  }

  ngOnDestroy(): void {
  }

  getLastPartOfUrl(): string {
    return this.shellService.getLastPartOfUrl(this.router);
  }
}
