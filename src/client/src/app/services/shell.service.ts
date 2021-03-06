import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { LanguageEnum } from 'src/app/enums/LanguageEnum';
import { AppStateService } from 'src/app/services/app-state.service';
import { ResultsService } from './results.service';

@Injectable({
  providedIn: 'root'
})
export class ShellService {
  ApplicationName: string[] = ['Pickleball addicts', 'Mordus de Pickleball'];
  ChangeProfile: string[] = ['Change profile', 'Modifier votre profil'];
  English: string[] = ['English', "English"];
  Francais: string[] = ['Français', 'Français'];
  Login: string[] = ['Login', 'Connexion'];
  Logoff: string[] = ['Logoff', 'Déconnexion'];
  Profile: string[] = ['Profile', 'Profil'];
  Register: string[] = ['Register', 'S\'inscrire'];
  ShellMenu: string[] = ['Shell Menu', 'Menu Shell'];

  constructor(public state: AppStateService,
    public resultsService: ResultsService) {
  }

  Init(title: Title, router: Router) {
    this.resultsService.LoadLocalStorage();
    this.resultsService.LoadLocalStorage();
    title.setTitle(this.ApplicationName[this.state.LangID ?? 0]);
    router.url.startsWith("/fr-CA") ?
      this.state.SetLanguage(LanguageEnum.fr) :
      this.state.SetLanguage(LanguageEnum.en);
  }

  GetLastPartOfUrl(router: Router): string {
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

  GetFirstLetters(name: string, numberOfLetters: number): string {
    if (name.length == 0) return '';

    return name.substring(0, numberOfLetters);
  }

}
