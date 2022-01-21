import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GetLanguageEnum } from 'src/app/enums/LanguageEnum';
import { AppStateService } from 'src/app/app-state.service';
import { ProfileService } from './profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit, OnDestroy {

  languageEnum = GetLanguageEnum();

  profileForm: FormGroup = this.formBuilder.group({});

  constructor(public state: AppStateService,
    public formBuilder: FormBuilder,
    public profileService: ProfileService) {
  }

  ngOnInit(): void {
    this.profileService.ResetLocals();

    this.profileForm = this.formBuilder.group({
      LoginEmail: [this.state.User.LoginEmail, [Validators.required, Validators.email, Validators.maxLength(100)]],
      FirstName: [this.state.User.FirstName, [Validators.required, Validators.maxLength(50)]],
      Initial: [this.state.User.Initial, [Validators.maxLength(20)]],
      LastName: [this.state.User.LastName, [Validators.required, Validators.maxLength(50)]],
      PlayerLevel: [this.state.User.PlayerLevel.toFixed(1), [Validators.required, Validators.min(1.0), Validators.max(5)]],
    });
  }

  ngOnDestroy(): void {
  }

  GetHasError(fieldName: 'LoginEmail' | 'FirstName' | 'Initial' | 'LastName' | 'PlayerLevel'): boolean {
    return this.profileService.GetHasError(fieldName, this.profileForm);
  }

  GetErrorMessage(fieldName: 'LoginEmail' | 'FirstName' | 'Initial' | 'LastName' | 'PlayerLevel'): string {
    return this.profileService.GetErrorMessage(fieldName, this.profileForm);
  }

  GetFormValid(): boolean {
    return this.profileService.GetFormValid(this.profileForm);
  }

  OnSubmit(): void {
    this.profileService.SubmitForm(this.profileForm);
  }
}
