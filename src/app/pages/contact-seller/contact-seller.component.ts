import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { SellerService } from '@app/_services';

@Component({
  selector: 'app-contact-seller',
  templateUrl: './contact-seller.component.html',
  styleUrls: ['../../shared/advert.css']
})
export class ContactSellerComponent implements OnInit {
  id: number;
  contactSellerForm:FormGroup;

  constructor(private sellerService: SellerService,
    private route: ActivatedRoute,private fb: FormBuilder) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];

    this.contactSellerForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(1)]],
      lastName: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email, Validators.minLength(6), Validators.maxLength(100)]],
      phoneNumber: ['', [Validators.minLength(6), Validators.maxLength(30)]],
     message:''
    });

    this.sellerService.sellerPerAdvert(this.id)
    .subscribe(x => this.contactSellerForm.patchValue(x));

  }

   // convenience getter for easy access to form fields
   get f() { return this.contactSellerForm.controls; }


}
