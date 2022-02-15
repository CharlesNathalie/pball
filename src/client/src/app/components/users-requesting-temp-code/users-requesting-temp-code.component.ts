import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AppStateService } from 'src/app/services/app-state.service';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { UsersRequestingTempCodeService } from 'src/app/services/users-requesting-temp-code.service';

@Component({
  selector: 'app-users-requesting-temp-code',
  templateUrl: './users-requesting-temp-code.component.html',
  styleUrls: ['./users-requesting-temp-code.component.css']
})
export class UsersRequestingTempCodeComponent implements OnInit, OnDestroy {

  forgotPasswordForm = this.formBuilder.group({
    LoginEmail: ['', [Validators.required, Validators.email]],
  });

  constructor(public state: AppStateService,
    public formBuilder: FormBuilder,
    public forgotPasswordService: UsersRequestingTempCodeService) {
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

  SubmitUsersRequestingTempCodeForm(): void {
    this.forgotPasswordService.SubmitUsersRequestingTempCodeForm(this.forgotPasswordForm);
  }
}
