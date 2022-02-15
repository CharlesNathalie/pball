import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AppStateService } from 'src/app/services/app-state.service';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { ChangePasswordService } from 'src/app/services/change-password.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit, OnDestroy {

  changePasswordForm = this.formBuilder.group({
    LoginEmail: ['', [Validators.required, Validators.email]],
    Password: ['', [Validators.required]],
    TempCode: ['', [Validators.required]],
  });

  constructor(public state: AppStateService,
    public formBuilder: FormBuilder,
    public changePasswordService: ChangePasswordService) {
  }

  ngOnInit(): void {
    this.changePasswordService.ResetLocals();
  }

  ngOnDestroy(): void {
  }

  GetHasError(fieldName: 'LoginEmail' | 'Password' | 'TempCode'): boolean {
    return this.changePasswordService.GetHasError(fieldName, this.changePasswordForm);
  }

  GetErrorMessage(fieldName: 'LoginEmail' | 'Password' | 'TempCode'): string {
    return this.changePasswordService.GetErrorMessage(fieldName, this.changePasswordForm);
  }

  GetFormValid(): boolean {
    return this.changePasswordService.GetFormValid(this.changePasswordForm);
  }

  SubmitChangePasswordForm(): void {
    this.changePasswordService.SubmitChangePasswordForm(this.changePasswordForm);
  }
}
