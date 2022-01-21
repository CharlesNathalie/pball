import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { catchError, map, of, Subscription } from 'rxjs';
import { AppStateService } from 'src/app/app-state.service';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { Player } from 'src/app/models/Player.model';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  ChangingProfile: string[] = ['Changing profile', 'Modifier votre profil'];
  FirstName: string[] = ['First name', 'Prénom'];
  FirstNameIsRequired: string[] = ['First name is required', 'Le prénom est requis'];
  Initial: string[] = ['Initial', 'Initiale'];
  InitialIsRequired: string[] = ['Initial is required', 'L\initiale est requise'];
  InvalidLoginEmail: string[] = ['Invalid login email', 'Courriel de connexion est invalide'];
  LastName: string[] = ['Last name', 'Nom de famille'];
  LastNameIsRequired: string[] = ['Last name is required', 'Le nom de famille est requis'];
  LoginEmail: string[] = ['Login email', 'Courriel de connexion'];
  LoginEmailIsRequired: string[] = ['Login email is required', 'Courriel de connexion est requis'];
  MaximumLengthForLoginEmailIs100: string[] = ['Maximum length for login email is 100', 'La longueur maximale pour le courriel de connexion est 100'];
  MaximumLengthForFirstNameIs50: string[] = ['Maximum length for first name is 50', 'La longueur maximale pour le prénom est 50'];
  MaximumLengthForInitialIs20: string[] = ['Maximum length for initial is 20', 'La longueur maximale pour l\'initiale est 20'];
  MaximumLengthForLastNameIs50: string[] = ['Maximum length for last name is 50', 'La longueur maximale pour le nom de famille est 50'];
  MaximumValueForPlayerLevelIs5: string[] = ['Maximum value for player level is 5', 'La valeure maximale pour le niveau du joueur est 5'];
  MinimumValueForPlayerLevelIs1: string[] = ['Minimum value for player level is 1', 'La valeure minimale pour le niveau du joueur est 1'];
  PlayerLevel: string[] = ['Player level', 'Niveau du joueur'];
  PlayerLevelIsRequired: string[] = ['Player level is required', 'Le niveau du joueur est requis'];
  ProfileUpdatedSuccessfully: string[] = ['Profile updated successfully', 'Mise à jour du profil complété avec succès'];
  PleaseEnterRequiredInformation: string[] = ['Please enter required information', 'SVP entrer l\'information requise'];
  required: string[] = ['required', 'requis'];
  ReturnToHomePage: string[] = ['Return to home page', 'Retour à la page d\'accueil'];
  UpdateProfile: string[] = ['Update profile', 'Mise à jour du profil'];
  UpdatingProfile: string[] = ['Updating profile', 'Mise à jour du profil'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  profileSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  Player: Player = <Player>{};

  constructor(public state: AppStateService,
    public httpClient: HttpClient) {
  }

  Profile() {
    this.Status = `${this.UpdatingProfile[this.state.LangID]} - ${this.state.User.LoginEmail}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoProfile().subscribe();
  }

  GetErrorMessage(fieldName: 'LoginEmail' | 'FirstName' | 'Initial' | 'LastName' | 'PlayerLevel', form: FormGroup): string {
    switch (fieldName) {
      case 'LoginEmail':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.LoginEmailIsRequired[this.state.LangID];
          }
          if (form.controls[fieldName].hasError('email')) {
            return this.InvalidLoginEmail[this.state.LangID];
          }
          if (form.controls[fieldName].hasError('maxlength')) {
            return this.MaximumLengthForLoginEmailIs100[this.state.LangID];
          }

          return '';
        }
      case 'FirstName':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.FirstNameIsRequired[this.state.LangID];
          }
          if (form.controls[fieldName].hasError('maxlength')) {
            return this.MaximumLengthForFirstNameIs50[this.state.LangID];
          }

          return '';
        }
      case 'Initial':
        {
          if (form.controls[fieldName].hasError('maxlength')) {
            return this.MaximumLengthForInitialIs20[this.state.LangID];
          }

          return '';
        }
      case 'LastName':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.LastNameIsRequired[this.state.LangID];
          }
          if (form.controls[fieldName].hasError('maxlength')) {
            return this.MaximumLengthForLastNameIs50[this.state.LangID];
          }

          return '';
        }
      case 'PlayerLevel':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.PlayerLevelIsRequired[this.state.LangID];
          }
          if (form.controls[fieldName].hasError('min')) {
            return this.MinimumValueForPlayerLevelIs1[this.state.LangID];
          }
          if (form.controls[fieldName].hasError('max')) {
            return this.MaximumValueForPlayerLevelIs5[this.state.LangID];
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

  GetHasError(fieldName: 'LoginEmail' | 'FirstName' | 'Initial' | 'LastName' | 'PlayerLevel', form: FormGroup): boolean {
    return this.GetErrorMessage(fieldName, form) == '' ? false : true;
  }

  ResetLocals() {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.profileSuccess = false;
  }

  SubmitForm(form: FormGroup) {
    if (form.valid) {
      this.Player.LoginEmail = form.controls['LoginEmail'].value;
      this.Player.FirstName = form.controls['FirstName'].value;
      this.Player.Initial = form.controls['Initial'].value;
      this.Player.LastName = form.controls['LastName'].value;
      this.Player.PlayerLevel = form.controls['PlayerLevel'].value;
      this.Profile();
    };
  }

  private DoProfile() {
    let languageEnum = GetLanguageEnum();

    localStorage.setItem('User', '');

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/contact`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.state.User.Token}`,
     })
    };

    return this.httpClient.put<Player>(url,
      JSON.stringify(this.Player), httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForProfile(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForProfile(e); }))));
  }

  private DoUpdateForProfile(player: Player) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.profileSuccess = true;
    this.Player = player;
    this.state.User = JSON.parse(JSON.stringify(player));
    console.debug(player);
  }

  private DoErrorForProfile(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;

    this.profileSuccess = false;
    this.Player = <Player>{};
    console.debug(e);
  }

}
