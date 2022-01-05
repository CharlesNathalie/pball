import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  Register: string[] = ['Register', 'Register (fr)'];

  constructor() {
  }

  getErrorMessage(fieldName: string, registerForm: FormGroup): string {
    switch (fieldName) {
      case "LoginEmail":
        {
          if (registerForm.controls[fieldName].hasError('required')) {
            return 'You must enter a valueaaaaaaaaaaa';
          }

          return registerForm.controls[fieldName].hasError('email') ? 'Not a valid email' : '';
        }
      default:
        return '';
    }
  }

  getFormValid(registerForm: FormGroup): boolean {
    return registerForm.valid ? true : false;
  }

  getHasError(fieldName: string, registerForm: FormGroup): boolean {
    return this.getErrorMessage(fieldName, registerForm) == '' ? false : true;
  }

  submitForm(registerForm: FormGroup): boolean {
    if (registerForm.valid) {
      return true;
    }
    return false;
  }

}
