import { Injectable } from '@angular/core';
import { GetLanguageEnum, LanguageEnum } from 'src/app/enums/LanguageEnum';

@Injectable({
  providedIn: 'root'
})
export class AppLanguageService {

  Version: string[] = ['Version: 1.0.0.0', 'Version: 1.0.0.0'];

  languageEnum = GetLanguageEnum();

  constructor() {
  }

  SetLanguage(Language: LanguageEnum) {
    if (Language == LanguageEnum.fr) {
      this.Language = LanguageEnum.fr;
      this.LangID = LanguageEnum.fr - 1;
      this.Culture = 'fr-CA';
    }
    else {
      this.Language = LanguageEnum.en;
      this.LangID = LanguageEnum.en - 1;
      this.Culture = 'en-CA';
    }
  }

  Language?: LanguageEnum = LanguageEnum.en;
  LangID?: number = 0;
  Culture?: string = 'en-CA';

  English: string[] = ['English', "English"];
  Francais: string[]  = ['Français', 'Français'];
  ShellMenu: string[] = ['Shell Menu', 'Menu Shell'];
  ShellApplicationName: string[] = ['Pickleball Participation', 'Pickleball Participation (FR)'];
}
