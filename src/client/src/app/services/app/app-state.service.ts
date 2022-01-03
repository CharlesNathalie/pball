import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LanguageEnum } from 'src/app/enums/LanguageEnum';

@Injectable({
  providedIn: 'root'
})
export class AppStateService {
  Working?: boolean = false;
  Error?: HttpErrorResponse;
  Status?: string = '';

  BaseApiUrl?: string = '';
  Language: LanguageEnum = LanguageEnum.en;
  LeftSideNavVisible?: boolean = false;
  
  constructor() {
  }
}
