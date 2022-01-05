import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetLanguageEnum, LanguageEnum } from 'src/app/enums/LanguageEnum';
import { Contact } from 'src/app/models/Contact.model';
import { WebContact } from 'src/app/models/WebContact.model';

@Injectable({
  providedIn: 'root'
})
export class AppStateService {
  Version: string[] = ['Version: 1.0.0.0', 'Version: 1.0.0.0'];

  //BaseApiUrl = 'https://pball.azurewebsites.net/'; 
  BaseApiUrl = 'https://localhost:7072/api/';

  languageEnum = GetLanguageEnum();

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

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

  Language: LanguageEnum = LanguageEnum.en;
  LangID: number = 0;
  Culture: string = 'en-CA';

  Contact: Contact = <Contact>{};
  WebContact: WebContact = <WebContact>{};
}
