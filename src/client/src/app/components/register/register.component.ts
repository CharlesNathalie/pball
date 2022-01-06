import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { AppStateService } from 'src/app/app-state.service';
import { RegisterService } from './register.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit, OnDestroy {

  languageEnum = GetLanguageEnum();

  registerForm = this.formBuilder.group({
    LoginEmail: ['', [Validators.required, Validators.email, Validators.maxLength(100)]],
    FirstName: ['', [Validators.required, Validators.maxLength(50)]],
    Initial: ['', [Validators.maxLength(20)]],
    LastName: ['', [Validators.required, Validators.maxLength(50)]],
    Password: ['', [Validators.required]],
    ConfirmPassword: ['', [Validators.required]],
    PlayerLevel: ['', [Validators.required, Validators.min(1.0), Validators.max(5)]],
  });

  constructor(public state: AppStateService,
    public formBuilder: FormBuilder,
    public registerService: RegisterService) {
  }

  ngOnInit(): void {
    this.registerService.ResetLocals();
  }

  ngOnDestroy(): void {
  }

  GetHasError(fieldName: 'LoginEmail' | 'FirstName' | 'Initial' | 'LastName' | 'Password' | 'ConfirmPassword' | 'PlayerLevel'): boolean {
    return this.registerService.GetHasError(fieldName, this.registerForm);
  }

  GetErrorMessage(fieldName: 'LoginEmail' | 'FirstName' | 'Initial' | 'LastName' | 'Password' | 'ConfirmPassword' | 'PlayerLevel'): string {
    return this.registerService.GetErrorMessage(fieldName, this.registerForm);
  }

  GetFormValid(): boolean {
    return this.registerService.GetFormValid(this.registerForm);
  }

  OnSubmit(): void {
    this.registerService.SubmitForm(this.registerForm);
  }
}
