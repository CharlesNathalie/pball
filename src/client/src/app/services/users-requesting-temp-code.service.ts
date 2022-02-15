import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { catchError, map, of, Subscription } from 'rxjs';
import { AppStateService } from 'src/app/services/app-state.service';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { LoginEmailModel } from 'src/app/models/LoginEmailModel.model';

@Injectable({
  providedIn: 'root'
})
export class UsersRequestingTempCodeService {
  allowingYouToReceiveYourCodeWithoutDelay: string[] = ['allowing you to receive your code without delay', 'pour recevoir votre code sans délai'];
  AnEmailWillBeSentTo: string[] = ['An email will be sent to', 'Un courriel sera envoyé à'];
  AtThisTimeYourCodeWillBeSentByTheAdministrator: string[] = ['At this time your code will be sent by the administrator', 'En ce moment, votre code sera envoyé par le gestionnaire'];
  ReturnToRegister: string[] = ['Return to register', 'Retour à s\'inscrire'];
  ReturnHome: string[] = ['Return home', 'Retour à la page d\'accueil'];
  ReturnToLogin: string[] = ['Return to login', 'Retour à connexion'];
  IfThisIsOfUrgentMatersYouCanAlwaysEmailOneOfYourLeagueAdministrators: string[] = ['If this is of urgent maters, you can always email one of your league administrators.', 'S\'il s\'agit d\'une question urgente, vous pouvez toujours envoyer un e-mail à l\'un des administrateurs de votre ligue.'];
  InTheFutureAllTheseRequestsWillBeDoneAutomatically: string[] = ['In the future, all these requests will be done automatically', "Dans le future, tous ces demandes seront faitent automatiquement"];
  InvalidLoginEmail: string[] = ['Invalid login email', 'Courriel de connexion est invalide'];
  LoginEmail: string[] = ['Login email', 'Courriel de connexion'];
  LoginEmailIsRequired: string[] = ['Login email is required', 'Courriel de connexion est requis'];
  Note: string[] = ['Note', 'Notez'];
  PleaseBePatientAsItCouldTakeAFewHoursOrDays: string[] = ['Please be patient as it could take a few hours or days', 'SVP soyez patient puisque cette demande peut prendre quelques heures ou jours'];
  Please_enter_required_information: string[] = ['Please enter required information', 'SVP entrer l\'information requise'];
  SendInYourEmailAndACodeWillBeSentToYouToChangeYourPassword: string[] = ['Send in your email and a code will be sent to you to change your password', 'Envoyez-nous votre courriel et un code vous sera acheminé afin que vous puissiez changer votre mot de passe'];
  SendingEmailToCreateNewPassword: string[] = ['Sending email to create new password', 'Envoyer courriel afin de créer un nouveau mot de passe'];
  SendMeTheCode: string[] = ['Send me the code', 'Envoyer moi un code'];
  YourRequestForACodeWasSuccessful: string[] = ['Your request for a code was successful', "Votre demande de code a été faite avec succès"]
  required: string[] = ['required', 'requis'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  forgotPasswordSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  constructor(public state: AppStateService,
    public httpClient: HttpClient) {
  }

  GenerateTempCode(loginEmailModel: LoginEmailModel) {
    this.Status = `${this.SendingEmailToCreateNewPassword[this.state.LangID]} - ${loginEmailModel.LoginEmail}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoGenerateTempCode(loginEmailModel).subscribe();
  }

  GetErrorMessage(fieldName: 'LoginEmail', form: FormGroup): string {
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
      default:
        return '';
    }
  }

  GetFormValid(form: FormGroup): boolean {
    return form.valid ? true : false;
  }

  GetHasError(fieldName: 'LoginEmail', form: FormGroup): boolean {
    return this.GetErrorMessage(fieldName, form) == '' ? false : true;
  }

  ResetLocals() {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.forgotPasswordSuccess = false;
  }

  SubmitUsersRequestingTempCodeForm(form: FormGroup) {
    if (form.valid) {
      let loginEmailModel: LoginEmailModel = <LoginEmailModel>{ LoginEmail: form.controls['LoginEmail'].value };
      this.GenerateTempCode(loginEmailModel);
    }
  }

  private DoGenerateTempCode(loginEmailModel: LoginEmailModel) {
    let languageEnum = GetLanguageEnum();

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/contact/generatetempcode`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    return this.httpClient.post<boolean>(url,
      JSON.stringify(loginEmailModel), httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForGenerateTempCode(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorGenerateTempCode(e); }))));
  }

  private DoUpdateForGenerateTempCode(boolRet: boolean) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.forgotPasswordSuccess = true;
    console.debug(boolRet);
  }

  private DoErrorGenerateTempCode(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;
    this.forgotPasswordSuccess = false;
    console.debug(e);
  }
}
