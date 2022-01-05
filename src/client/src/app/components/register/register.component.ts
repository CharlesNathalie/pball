import { Component, OnInit, OnDestroy, Input } from '@angular/core';
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
    LoginEmail: ['', Validators.required],
    FirstName: ['', Validators.required],
    Initial: [''],
    LastName: ['', Validators.required],
    Password: ['', Validators.required],
    ConfirmPassword: ['', Validators.required],
    PlayerLevel: ['', Validators.required],
  });

  constructor(public state: AppStateService,
    public formBuilder: FormBuilder,
    public registerService: RegisterService) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  getHasError(fieldName: string): boolean {
    return this.registerService.getHasError(fieldName, this.registerForm);
  }

  getErrorMessage(fieldName: string): string {
    return this.registerService.getErrorMessage(fieldName, this.registerForm);
  }

  getFormValid(): boolean {
    return this.registerService.getFormValid(this.registerForm);
  }

  onSubmit(): void {
    this.registerService.submitForm(this.registerForm);
    }
}
