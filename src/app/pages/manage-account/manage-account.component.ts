import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService, AuthenticationService } from '@app/_services';
import { MustMatch } from '@app/shared/validate-password';
import { first } from 'rxjs/operators';
import { User } from '@app/_models';

@Component({
  templateUrl: './manage-account.component.html'
})
export class ManageAccountComponent implements OnInit {
  currentUser: User;
  userForm: FormGroup;
  loading = false;
  submitted = false;
  errorMessage: string;
  removeWhiteSpace: RegExp;

  constructor(private formBuilder: FormBuilder, private router: Router,private authenticationService:AuthenticationService,
    private route: ActivatedRoute,
    private userService: UserService) {
    
    this.currentUser = this.authenticationService.currentUserValue;
    
  }
  


  ngOnInit(): void {

    this.removeWhiteSpace = new RegExp("\\S");
    
    this.userForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.minLength(1), Validators.pattern(this.removeWhiteSpace)]],
      lastName: ['', [Validators.required, Validators.minLength(3), Validators.pattern(this.removeWhiteSpace)]],
      username: ['', [Validators.email, Validators.minLength(6)]],
      password: ['', [ Validators.minLength(8)]],
      confirmPassword: ['']
    }, {
      validator: MustMatch('password', 'confirmPassword')
    });
    
    this.userService.getById(this.currentUser.id)
    .pipe(first())
      .subscribe(x => this.userForm.patchValue(x));
    
      
  }

  
  // convenience getter for easy access to form fields
  get f() { return this.userForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.userForm.invalid) {
      return;
    }

    this.loading = true;
    this.updateUser();
  }

  private updateUser() {
    this.userService.update(this.currentUser.id, this.userForm.value)
      .pipe(first())
      .subscribe({
        next: () => {
          alert('User updated successful');
          this.router.navigate(['/manageUser'], { relativeTo: this.route });
          this.loading = false;
        },
        error: error => {
          alert(error);
          this.loading = false;
        }
      });
  }
}
