import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms'

import { AlertService, UserService } from '@app/_services';
import { Router } from '@angular/router';
import { MustMatch } from '@app/shared/validate-password';

@Component({
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  loading = false;
  submitted = false;
  errorMessage: string;
  removeWhiteSpace: RegExp;

  constructor(private formBuilder: FormBuilder, private router: Router,
    private userService: UserService,private alertService:AlertService) {

  }


  ngOnInit(): void {
    this.removeWhiteSpace = new RegExp("\\S");
    this.registerForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.minLength(1), Validators.pattern(this.removeWhiteSpace)]],
      lastName: ['', [Validators.required, Validators.minLength(3), Validators.pattern(this.removeWhiteSpace)]],
      username: ['', [Validators.required, Validators.email, Validators.minLength(6)]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', Validators.required]
    }, {
      validator: MustMatch('password', 'confirmPassword')
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.registerForm.controls; }

 //Register User
  save(): void {
    this.submitted = true;
    this.alertService.clear();

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      return;
    }

    this.loading = true;
    this.userService.register(this.registerForm.value)
      .subscribe(
        data => {
          this.alertService.success('Registration successful', { keepAfterRouteChange: true });
          this.router.navigate(['/login']);
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
      });
  }


}

