import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService, AuthenticationService, AlertService } from '@app/_services';
import { MustMatch } from '@app/shared/validate-password';
import { first } from 'rxjs/operators';
import { User } from '@app/_models';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '@app/shared/confirmation-dialog/confirmation-dialog.component';

@Component({
  templateUrl: './manage-account.component.html'
})
export class ManageAccountComponent implements OnInit {
  currentUser: User;
  userForm: FormGroup;
  loading = false;
  submitted = false;
  removeWhiteSpace: RegExp;

  constructor(private formBuilder: FormBuilder, private router: Router, private authenticationService: AuthenticationService,
    private route: ActivatedRoute, private alertService: AlertService,
    private userService: UserService, public dialog: MatDialog) {

    this.currentUser = this.authenticationService.currentUserValue;

  }



  ngOnInit(): void {

    this.removeWhiteSpace = new RegExp("\\S");

    this.userForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.minLength(1), Validators.pattern(this.removeWhiteSpace)]],
      lastName: ['', [Validators.required, Validators.minLength(3), Validators.pattern(this.removeWhiteSpace)]],
      username: ['', [Validators.email, Validators.minLength(6)]],
      OldPassword: ['', [Validators.minLength(8)]],
      password: ['', [Validators.minLength(8)]],
      confirmPassword: ['']
    }, {
      validator: MustMatch('password', 'confirmPassword')
    });

    this.userService.getUserById(this.currentUser.id)
      .pipe(first())
      .subscribe(x => this.userForm.patchValue(x));

  }


  // convenience getter for easy access to form fields
  get f() { return this.userForm.controls; }

  onSubmit() {
    this.submitted = true;

    this.alertService.clear();

    // stop here if form is invalid
    if (this.userForm.invalid) {
      return;
    }

    //stop here if old and new password are the same
    // if (this.userForm.controls["OldPassword"].value == this.userForm.controls["password"].value
    //   && this.userForm.controls["OldPassword"].value == null) {
    //   alert("Old password can not be the same as new password");
    //   return;
    // }

    this.loading = true;
    this.updateUser();
  }

  private updateUser() {
    const confirmDialog = this.dialog.open(ConfirmationDialogComponent, {
      data: {
        title: 'Confirm Update User',
        message: 'Are you sure you want to make some changes on your profile?'
      }
    });
    confirmDialog.afterClosed().subscribe(result => {
      if (result) {
        this.userService.updateUser(this.currentUser.id, this.userForm.value)
          .pipe(first())
          .subscribe({
            next: () => {
              this.alertService.success('User updated successful', { keepAfterRouteChange: true });
              this.router.navigate(['/manageUser'], { relativeTo: this.route });
              this.loading = false;
            },
            error: error => {
              this.alertService.error(error);
              this.loading = false;
            }
          });
      }
      else {
        this.loading = false;
      }
    });

  }
}
