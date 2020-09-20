import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationDialogComponent } from '@app/shared/confirmation-dialog/confirmation-dialog.component';
import { User } from '@app/_models';
import { AlertService, AuthenticationService, UserService, SellerService } from '@app/_services';
import { first } from 'rxjs/operators';

@Component({
  templateUrl: './seller-profile.component.html'
})
export class SellerProfileComponent implements OnInit {
  currentUser: User;
  sellerForm: FormGroup;
  loading = false;
  submitted = false;

  constructor(private formBuilder: FormBuilder, private userService: UserService,
    private authenticationService: AuthenticationService, private router: Router, private route: ActivatedRoute,
    public dialog: MatDialog,
    private sellerService: SellerService, private alertService: AlertService) {
    this.currentUser = this.authenticationService.currentUserValue;
  }

  ngOnInit(): void {
    this.sellerForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.minLength(1)]],
      lastName: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email, Validators.minLength(6), Validators.maxLength(100)]],
      phoneNumber: ['', [Validators.minLength(6), Validators.maxLength(30)]]
    });


    this.sellerService.getSellerByUserId(this.currentUser.id)
      .pipe(first())
      .subscribe(x => this.sellerForm.patchValue({
        firstName: x.firstName,
        lastName: x.lastName,
        email: x.email,
        phoneNumber: x.phoneNumber
      }));

  }
  // convenience getter for easy access to form fields
  get f() { return this.sellerForm.controls; }

  onSubmit() {
    this.submitted = true;
    this.alertService.clear();
    if (this.sellerForm.invalid) {
      return;
    }

    this.loading = true;
    this.updateSeller();
  }



  private updateSeller() {

    const confirmDialog = this.dialog.open(ConfirmationDialogComponent, {
      data: {
        title: 'Confirm Update User',
        message: 'Are you sure you want to make some changes on your profile?'
      }
    });
    confirmDialog.afterClosed().subscribe(result => {
      if (result) {
        this.sellerService.updateSeller(this.currentUser.id, this.sellerForm.value)
          .pipe(first())
          .subscribe({
            next: () => {
              this.alertService.success('Seller profile updated successful', { keepAfterRouteChange: true });
              this.router.navigate(['/sellerProfile'], { relativeTo: this.route });
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
