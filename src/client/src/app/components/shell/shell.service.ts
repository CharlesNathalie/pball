import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { LanguageEnum } from 'src/app/enums/LanguageEnum';
import { AppStateService } from 'src/app/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class ShellService {

  English: string[] = ['English', "English"];
  Francais: string[] = ['Français', 'Français'];
  Profile: string[] = ['Profile', 'Profile'];
  Register: string[] = ['Register', 'Register (fr)'];
  ShellMenu: string[] = ['Shell Menu', 'Menu Shell'];
  ShellApplicationName: string[] = ['Pickleball Participation', 'Pickleball Participation (FR)'];

  constructor(public state: AppStateService) {
  }

  init(title: Title, router: Router) {
    title.setTitle(this.ShellApplicationName[this.state.LangID ?? 0]);
    router.url.startsWith("/fr-CA") ?
      this.state.SetLanguage(LanguageEnum.fr) :
      this.state.SetLanguage(LanguageEnum.en);
  }

  getLastPartOfUrl(router: Router): string {
    let lastPartOfUrl: string = router.url;
    let pos: number = lastPartOfUrl.indexOf('en-CA');
    if (pos < 0) {
      pos = lastPartOfUrl.indexOf('fr-CA');
    }

    if (pos < 0) {
      return '';
    }

    lastPartOfUrl = lastPartOfUrl.substring(pos + 5);

    return lastPartOfUrl;
  }

}
