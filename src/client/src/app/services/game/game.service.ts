import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { catchError, map, of, Subscription } from 'rxjs';
import { AppStateService } from 'src/app/app-state.service';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { Game } from 'src/app/models/Game.model';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  GettingGames: string[] = ['Getting all leagues', 'Getting all league (fr)'];
  ContactIDIsRequired: string[] = ['ContactID is required', 'ContactID est requis'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  getAllGamesForContactSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient) {
  }

  GetAllGamesForContact(contactID: number) {
    this.Status = `${this.GettingGames[this.state.LangID]}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoGetAllGamesForContact(contactID).subscribe();
  }

  GetErrorMessage(fieldName: 'ContactID', form: FormGroup): string {
    switch (fieldName) {
      case 'ContactID':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.ContactIDIsRequired[this.state.LangID];
          }

          return '';
        }
      default:
        return '';
    }
  }

  GetFormValid(form: FormGroup): boolean {
    return form.valid ? true : false;
  }

  GetHasError(fieldName: 'ContactID', form: FormGroup): boolean {
    return this.GetErrorMessage(fieldName, form) == '' ? false : true;
  }

  ResetLocals()
  {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{}; 
    this.getAllGamesForContactSuccess = false;

  }

  SubmitForm(form: FormGroup) {
    if (form.valid) {
      let ContactID: number = form.controls['ContactID'].value;
      this.GetAllGamesForContact(ContactID);
    }
  }

  private DoGetAllGamesForContact(contactID: number) {
    let languageEnum = GetLanguageEnum();

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/game/getallgamesforcontact/${ contactID } }`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    return this.httpClient.get<Game[]>(url, httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForGetAllGamesForContact(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForGetAllGamesForContact(e); }))));
  }

  private DoUpdateForGetAllGamesForContact(gameList: Game[]) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.getAllGamesForContactSuccess = true;

    this.state.ContactGameList = gameList;

    console.debug(gameList);
  }

  private DoErrorForGetAllGamesForContact(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;

    this.getAllGamesForContactSuccess = false;
    console.debug(e);
  }
}
