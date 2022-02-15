import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of, Subscription } from 'rxjs';
import { AppStateService } from 'src/app/services/app-state.service';
import { GetLanguageEnum } from '../enums/LanguageEnum';
import { ChangePasswordModel } from '../models/ChangePasswordModel.model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  Hello: string[] = ['Hello', 'Bonjour'];
  GettingChangePasswordRequestList: string[] = ['Getting change password request list', 'Téléchargement de la liste de demande de changement de mot de passe'];
  PBallEmail: string[] = ['PBall email', 'Courriel de PBall'];
  UserNotLoggedIn: string[] = ['User not logged in', 'L\'utilisateur n\'a aucune connexion'];
  WelcomeToTheAdminManagementPage: string[] = ['Welcome to the administrator management page', 'Bienvenue à la page de gestion pour l\'administrateur'];
  YourTemporaryCodeIs: string[] = ['Your temporary code is', 'Votre code temporaire est le'];
  
  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  GetChangePasswordRequestListSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient) {
  }

  GetChangePasswordRequestList() {
    if (this.state.DemoVisible || this.state.User.ContactID == 0) {
      this.Error = new HttpErrorResponse({ error: this.UserNotLoggedIn });
      return;
    }

    this.Status = `${this.GettingChangePasswordRequestList[this.state.LangID]}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoGetChangePasswordRequestList().subscribe();
  }

  ResetLocals() {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.GetChangePasswordRequestListSuccess = false;
  }

  private DoGetChangePasswordRequestList() {
    let languageEnum = GetLanguageEnum();

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/Contact/GetChangePasswordRequestList`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.state.User.Token}`,
      })
    };

    return this.httpClient.get<ChangePasswordModel[]>(url, httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForGetChangePasswordRequestList(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForGetChangePasswordRequestList(e); }))));
  }

  private DoUpdateForGetChangePasswordRequestList(changePasswordRequestList: ChangePasswordModel[]) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.GetChangePasswordRequestListSuccess = true;
    this.state.ChangePasswordRequestList = changePasswordRequestList;
    console.debug(changePasswordRequestList);
  }

  private DoErrorForGetChangePasswordRequestList(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.state.ChangePasswordRequestList = [];
    this.GetChangePasswordRequestListSuccess = false;
    console.debug(e);
  }
}
