import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { catchError, map, of, Subscription } from 'rxjs';
import { AppStateService } from 'src/app/app-state.service';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { Contact } from 'src/app/models/Contact.model';
import { RegisterModel } from 'src/app/models/RegisterModel.model';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  AccountCreatedSuccessfully: string[] = ['Account created successfully', 'Inscription complétée avec succès'];
  AlreadyHaveAnAccount: string[] = ['Already have an account', 'Déjà inscrit'];
  ConfirmPassword: string[] = ['Confirm password', 'Confirmation de mot de passe'];
  ConfirmPasswordIsRequired: string[] = ['Confirm password is required', 'La confirmation du mot de passe est requise'];
  CreateAccount: string[] = ['Create account', 'S\'inscrire'];
  FirstName: string[] = ['First name', 'Prénom'];
  FirstNameIsRequired: string[] = ['First name is required', 'Le prénom est requis'];
  Initial: string[] = ['Initial', 'Initiale'];
  InitialIsRequired: string[] = ['Initial is required', 'L\initiale est requise'];
  InvalidLoginEmail: string[] = ['Invalid login email', 'Courriel de connexion est invalide'];
  LastName: string[] = ['Last name', 'Nom de famille'];
  LastNameIsRequired: string[] = ['Last name is required', 'Le nom de famille est requis'];
  LoginEmail: string[] = ['Login email', 'Courriel d\'inscription'];
  LoginEmailIsRequired: string[] = ['Login email is required', 'Courriel de connexion est requis'];
  MaximumLengthForLoginEmailIs100: string[] = ['Maximum length for login email is 100', 'La longueur maximale pour le courriel de connexion est 100'];
  MaximumLengthForFirstNameIs50: string[] = ['Maximum length for first name is 50', 'La longueur maximale pour le prénom est 50'];
  MaximumLengthForInitialIs20: string[] = ['Maximum length for initial is 20', 'La longueur maximale pour l\'initiale est 20'];
  MaximumLengthForLastNameIs50: string[] = ['Maximum length for last name is 50', 'La longueur maximale pour le nom de famille est 50'];
  MaximumValueForPlayerLevelIs5: string[] = ['Maximum value for player level is 5', 'La valeure maximale pour le niveau du joueur est 5'];
  MinimumValueForPlayerLevelIs1: string[] = ['Minimum value for player level is 1', 'La valeure minimale pour le niveau du joueur est 1'];
  Password: string[] = ['Password', 'Mot de passe'];
  PasswordAndConfirmPasswordAreNotIdentical: string[] = ['Password and confirm password are not identical', 'Le mot de passe et sa confirmation ne sont pas identiques'];
  PasswordIsRequired: string[] = ['Password is required', 'Le mot de passe est requis'];
  PlayerLevel: string[] = ['Player level', 'Niveau du joueur'];
  PlayerLevelIsRequired: string[] = ['Player level is required', 'Le niveau du joueur est requis'];
  PleaseEnterRequiredInformation: string[] = ['Please enter required information', 'SVP entrer l\'information requise'];
  Registering: string[] = ['Registering', 'Inscription en cours']
  required: string[] = ['required', 'requis'];
  ReturnToLogin: string[] = ['Return to login', 'Retour à la page de connexion'];

  Status: string = '';
  Working: boolean = false;
  Error: HttpErrorResponse = <HttpErrorResponse>{};

  registerSuccess: boolean = false;

  private sub: Subscription = new Subscription();

  Contact: Contact = <Contact>{};

  constructor(public state: AppStateService,
    public httpClient: HttpClient) {
  }

  Register(registerModel: RegisterModel) {
    this.Status = `${this.Registering[this.state.LangID]} - ${registerModel.LoginEmail}`;
    this.Working = true;
    this.Error = <HttpErrorResponse>{};

    this.sub ? this.sub.unsubscribe() : null;
    this.sub = this.DoRegister(registerModel).subscribe();
  }

  GetErrorMessage(fieldName: 'LoginEmail' | 'FirstName' | 'Initial' | 'LastName' | 'Password' | 'ConfirmPassword' | 'PlayerLevel', form: FormGroup): string {
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
      case 'Password':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.PasswordIsRequired[this.state.LangID];
          }

          return '';
        }
      case 'ConfirmPassword':
        {
          if (form.controls[fieldName].hasError('required')) {
            return this.ConfirmPasswordIsRequired[this.state.LangID];
          }
          if (form.controls['Password'].value != form.controls[fieldName].value) {
            return this.PasswordAndConfirmPasswordAreNotIdentical[this.state.LangID];
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

  GetHasError(fieldName: 'LoginEmail' | 'FirstName' | 'Initial' | 'LastName' | 'Password' | 'ConfirmPassword' | 'PlayerLevel', form: FormGroup): boolean {
    return this.GetErrorMessage(fieldName, form) == '' ? false : true;
  }

  ResetLocals()
  {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{}; 
    this.registerSuccess = false;

  }

  SubmitForm(form: FormGroup) {
    if (form.valid) {
      let registerModel: RegisterModel = <RegisterModel>{
        LoginEmail: form.controls['LoginEmail'].value,
        FirstName: form.controls['FirstName'].value,
        Initial: form.controls['Initial'].value,
        LastName: form.controls['LastName'].value,
        Password: form.controls['Password'].value,
        PlayerLevel: form.controls['PlayerLevel'].value
      };
      this.Register(registerModel);
    }
  }

  private DoRegister(registerModel: RegisterModel) {
    let languageEnum = GetLanguageEnum();

    localStorage.setItem('currentContact', '');
    this.state.Contact = <Contact>{};

    const url: string = `${this.state.BaseApiUrl}${languageEnum[this.state.Language]}-CA/contact/register`;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    };

    return this.httpClient.post<Contact>(url,
      JSON.stringify(registerModel), httpOptions)
      .pipe(map((x: any) => { this.DoUpdateForRegister(x); }),
        catchError(e => of(e).pipe(map(e => { this.DoErrorForRegister(e); }))));
  }

  private DoUpdateForRegister(contact: Contact) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>{};
    this.registerSuccess = true;
    this.Contact = contact;
    console.debug(contact);
  }

  private DoErrorForRegister(e: HttpErrorResponse) {
    this.Status = '';
    this.Working = false;
    this.Error = <HttpErrorResponse>e;

    this.registerSuccess = false;
    this.Contact = <Contact>{};
    console.debug(e);
  }

}
