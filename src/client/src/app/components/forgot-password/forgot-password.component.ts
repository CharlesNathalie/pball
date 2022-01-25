import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AppStateService } from 'src/app/services/app-state.service';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { ForgotPasswordService } from 'src/app/services/forgot-password.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit, OnDestroy {

  forgotPasswordForm = this.formBuilder.group({
    LoginEmail: ['', [Validators.required, Validators.email]],
  });

  constructor(public state: AppStateService,
    public formBuilder: FormBuilder,
    public forgotPasswordService: ForgotPasswordService) {
  }

  ngOnInit(): void {
    this.forgotPasswordService.ResetLocals();
  }

  ngOnDestroy(): void {
  }

  GetHasError(fieldName: 'LoginEmail'): boolean {
    return this.forgotPasswordService.GetHasError(fieldName, this.forgotPasswordForm);
  }

  GetErrorMessage(fieldName: 'LoginEmail'): string {
    return this.forgotPasswordService.GetErrorMessage(fieldName, this.forgotPasswordForm);
  }

  GetFormValid(): boolean {
    return this.forgotPasswordService.GetFormValid(this.forgotPasswordForm);
  }

  SubmitForgotPasswordForm(): void {
    this.forgotPasswordService.SubmitForgotPasswordForm(this.forgotPasswordForm);
  }
}
