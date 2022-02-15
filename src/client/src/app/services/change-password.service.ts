import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { catchError, map, of, Subscription } from 'rxjs';
import { AppStateService } from 'src/app/services/app-state.service';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { ChangePasswordModel } from '../models/ChangePasswordModel.model';

@Injectable({
  providedIn: 'root'
})
export class ChangePasswordService {
  ChangePasswordTxt: string[] = ['Change Password', 'Changer mot de passe'];
  ChangeYourPasswordUsingTheTemporaryCode: string[] = ['Change your password using the temporary code', 'Changez votre mot de passe en utilisant le code temporaire'];
  ChangingYourPasswordUsingTheTemporaryCode: string[] = ['Changing your password using the temporary code', 'Changement de votre mot de passe en utilisant le code temporaire en cour'];
  // allowingYouToReceiveYourCodeWithoutDelay: string[] = ['allowing you to receive your code without delay', 'pour recevoir votre code sans délai'];
  // AnEmailWillBeSentTo: string[] = ['An email will be sent to', 'Un courriel sera envoyé à'];
  // AtThisTimeYourCodeWillBeSentByTheAdministrator: string[] = ['At this time your code will be sent by the administrator', 'En ce moment, votre code sera envoyé par le gestionnaire'];
  // IfThisIsOfUrgentMatersYouCanAlwaysEmailOneOfYourLeagueAdministrators: string[] = ['If this is of urgent maters, you can always email one of your league administrators.', 'S\'il s\'agit d\'une question urgente, vous pouvez toujours envoyer un e-mail à l\'un des administrateurs de votre ligue.'];
  // InTheFutureAllTheseRequestsWillBeDoneAutomatically: string[] = ['In the future, all these requests will be done automatically', "Dans le future, tous ces demandes seront faitent automatiquement"];
  InvalidLoginEmail: string[] = ['Invalid login email', 'Courriel de connexion est invalide'];
  LoginEmail: string[] = ['Login email', 'Courriel de connexion'];
  LoginEmailIsRequired: string[] = ['Login email is required', 'Courriel de connexion est requis'];
  // Note: string[] = ['Note', 'Notez'];
  // PleaseBePatientAsItCouldTakeAFewHoursOrDays: string[] = ['Please be patient as it could take a few hours or days', 'SVP soyez patient puisque cette demande peut prendre quelques heures ou jours'];
  Please_enter_required_information: string[] = ['Please enter required information', 'SVP entrer l\'information requise'];
  // SendInYourEmailAndACodeWillBeSentToYouToChangeYourPassword: string[] = ['Send in your email and a code will be sent to you to change your password', 'Envoyez-nous votre courriel et un code vous sera acheminé afin que vous puissiez changer votre mot de passe'];
  // SendingEmailToCreateNewPassword: string[] = ['Sending email to create new password', 'Envoyer courriel afin de créer un nouveau mot de passe'];
  // SendMeTheCode: string[] = ['Send me the code', 'Envoyer moi un code'];
  YourPasswordChangeWasSuccessful: string[] = ['Your password change was successful', "Votre mot de pass a été changé avec succès"];
  YouCanNowLoginWithYourNewPassword: string[] = ['You can now login with your new password', "Vous pouvez maintenant faire la connexion avec votre nouveau mot de passe"];

  NewPassword: string[] = ['New password', 'Nouveau mot de passe'];
  NewPasswordIsRequired: string[] = ['New password is required', 'Nouveau mot de passe de connexion est requis'];
  ReturnToRegister: string[] = ['Return to register', 'Retour à s\'inscrire'];
  ReturnHome: string[] = ['Return home', 'Retour à la page d\'accueil'];
  ReturnToLogin: string[] = ['Return to login', 'Retour à connexion'];
  TemporaryCode: string[] = ['Temporary code', 'Code temporaire'];
  TemporaryCodeIsRequired: string[] = ['Temporary code is required', 'Code temporaire de connexion est requis'];
  required: string[] = ['required', 'requis'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  changePasswordSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient) {
  }

  ChangePassword(changePasswordModel: ChangePasswordModel) {
    this.Status = `${this.ChangingYourPasswordUsingTheTemporaryCode[this.state.LangID]} - ${changePasswordModel.LoginEmail}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoChangePassword(changePasswordModel).subscribe();
  }

  GetErrorMessage(fieldName: 'LoginEmail' | 'Password' | 'TempCode', form: FormGroup): string {
    switch (fieldName) {
      case 'LoginEmail':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.LoginEmailIsRequired[this.state.LangID];
          }
          if (form.controls[fieldName].hasError('email')) {
            return this.InvalidLoginEmail[this.state.LangID];
          }

          return '';
        }
      case 'Password':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.NewPasswordIsRequired[this.state.LangID];
          }

          return '';
        }
      case 'TempCode':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.TemporaryCodeIsRequired[this.state.LangID];
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

  GetHasError(fieldName: 'LoginEmail' | 'Password' | 'TempCode', form: FormGroup): boolean {
    return this.GetErrorMessage(fieldName, form) == '' ? false : true;
  }

  ResetLocals() {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.changePasswordSuccess = false;
  }

  SubmitChangePasswordForm(form: FormGroup) {
    if (form.valid) {
      let changePasswordModel: ChangePasswordModel = <ChangePasswordModel>{
        LoginEmail: form.controls['LoginEmail'].value,
        Password: form.controls['Password'].value,
        TempCode: form.controls['TempCode'].value,
      };
      this.ChangePassword(changePasswordModel);
    }
  }

  private DoChangePassword(changePasswordModel: ChangePasswordModel) {
    let languageEnum = GetLanguageEnum();

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/contact/changepassword`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    return this.httpClient.post<boolean>(url,
      JSON.stringify(changePasswordModel), httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForChangePassword(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorChangePassword(e); }))));
  }

  private DoUpdateForChangePassword(boolRet: boolean) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.changePasswordSuccess = true;
    console.debug(boolRet);
  }

  private DoErrorChangePassword(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.changePasswordSuccess = false;
    console.debug(e);
  }
}
