import { Component,ViewChildren,OnDestroy, ElementRef } from '@angular/core';
import { FormControlName, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Advert } from '@app/_models';
import { AdvertService } from '@app/_services';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-create-edit-advert',
  templateUrl: './create-edit-advert.component.html'
})
export class CreateEditAdvertComponent implements OnDestroy {
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  advertForm: FormGroup;
  private sub: Subscription;
  errorMessage: string;
  advert: Advert;
  pageTitle = 'Create New Advert';

  constructor(private fb: FormBuilder, private advertService: AdvertService,
    private route: ActivatedRoute,
    private router: Router) {

  }


  ngOnInit(): void {
    this.advertForm = this.fb.group({
      advertHeadlineText: ['', [Validators.required,
      Validators.minLength(3),
      Validators.maxLength(50)]],
      province: '',
      city:'',
      price: ['', Validators.required],
      advertDetails: '',
      userId: '',
      releaseDate:''
    });

    // Read the advert Id from the route parameter
    this.sub = this.route.paramMap.subscribe(
      params => {
        const id = +params.get('id');
        this.getAdvert(id);
      }
    );

  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  /** 
  * get advert by Id from the server 
  */
  getAdvert(id: number): void {
    this.advertService.getById(id)
      .subscribe({
        next: (advert: Advert) => this.displayAdvert(advert),
        error: err => this.errorMessage = err
      });
  }


  displayAdvert(advert:Advert): void {
    if (this.advertForm) {
      this.advertForm.reset();
    }
    this.advert = advert;

    if (this.advert.id === 0) {
      this.pageTitle = 'Create Advert';
    } else {
      this.pageTitle = `Edit Advert: ${this.advert.advertHeadlineText}`;
    }

    // Update the data on the form
    this.advertForm.patchValue({
      advertHeadlineText: this.advert.advertHeadlineText,
      province: this.advert.province,
      city:this.advert.city,
      price: this.advert.price,
      advertDetails:this.advert.advertDetails
    });

  }

  /** 
   * remove advert from the list 
   */
  deleteAdvert(): void {
    if (this.advert.id === 0) {
      // Don't delete, it was never saved.
      this.onSaveComplete();
    } else {
      if (confirm(`Are you sure you want to remove: ${this.advert.advertHeadlineText}?`)) {
        this.advertService.delete(this.advert.id)
          .subscribe({
            next: () => this.onSaveComplete(),
            error: err => this.errorMessage = err
          });
      }
    }
  }

  /** 
   * create new advert and save it to Db 
   */
  saveAdvert(): void {
    if (this.advertForm.valid) {
      if (this.advertForm.dirty) {
        let currUser = JSON.parse(localStorage.getItem("currentUser"));
        this.advertForm.controls['userId'].setValue(currUser["id"]);
        const p = { ...this.advert, ...this.advertForm.value };

        if (p.id === 0) {
          this.advertService.create(p)
            .subscribe({
              next: () => this.onSaveComplete(),
              error: err => this.errorMessage = err
            });
        } else {
          this.advertService.update(p.id,p)
            .subscribe({
              next: () => this.onSaveComplete(),
              error: err => this.errorMessage = err
            });
        }
      } else {
        this.onSaveComplete();
      }
    } else {
      this.errorMessage = 'Please correct the validation errors.';
    }
  }

  /** 
   * navigate back to advert list if saved or updated successful 
   */
  onSaveComplete(): void {
    // Reset the form to clear the flags
    this.advertForm.reset();
    alert("Success");
    this.router.navigate(['/advert']);
  }

}
