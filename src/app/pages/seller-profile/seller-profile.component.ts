import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '@app/_models';
import { AuthenticationService, UserService } from '@app/_services';
import { SellerService } from '@app/_services/seller.service';
import { first } from 'rxjs/operators';

@Component({
  templateUrl: './seller-profile.component.html'
})
export class SellerProfileComponent implements OnInit {
  currentUser: User;
  sellerForm: FormGroup;
  loading = false;
  submitted = false;
  isDisabled = true;

  constructor(private formBuilder: FormBuilder, private userService: UserService,
    private authenticationService: AuthenticationService,private router: Router,private route: ActivatedRoute,
    private sellerService: SellerService) {
    this.currentUser = this.authenticationService.currentUserValue;
  }
  
  ngOnInit(): void {
    this.sellerForm = this.formBuilder.group({
      // firstName: [{ value: '', disabled: this.isDisabled }, [Validators.required, Validators.minLength(1)]],
      // lastName: [{ value: '', disabled: this.isDisabled }, [Validators.required, Validators.minLength(3)]],
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
    if (this.sellerForm.invalid) {
      return;
    }

    this.loading = true;
    this.updateSeller();
  }

  private updateSeller() {
    if (confirm(`Are you sure you want to make some changes on your profile?`)) {
      this.sellerService.updateSeller(this.currentUser.id, this.sellerForm.value)
        .pipe(first())
        .subscribe({
          next: () => {
            alert('Seller profile updated successful');
            this.router.navigate(['/sellerProfile'], { relativeTo: this.route });
            this.loading = false;
          },
          error: error => {
            alert(error);
            this.loading = false;
          }
        });
    }
    this.loading = false;
  }

}
