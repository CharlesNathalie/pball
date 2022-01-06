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
      LoginEmail: [this.profileService.Contact.LoginEmail, [Validators.required, Validators.email, Validators.maxLength(100)]],
      FirstName: [this.profileService.Contact.FirstName, [Validators.required, Validators.maxLength(50)]],
      Initial: [this.profileService.Contact.Initial, [Validators.maxLength(20)]],
      LastName: [this.profileService.Contact.LastName, [Validators.required, Validators.maxLength(50)]],
      PlayerLevel: [this.profileService.Contact.PlayerLevel, [Validators.required, Validators.min(1.0), Validators.max(5)]],
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
