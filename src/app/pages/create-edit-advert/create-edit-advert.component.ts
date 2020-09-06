import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { AdvertService } from '@app/_services';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';

@Component({
  // selector: 'app-create-edit-advert',
  templateUrl: './create-edit-advert.component.html'
})
export class CreateEditAdvertComponent implements OnInit {

  advertForm: FormGroup;
  id: number;
  isAddMode: boolean;
  loading = false;
  submitted = false;
  removeWhiteSpace: RegExp;
  // City Names
  //City: any = ['Florida', 'South Dakota', 'Tennessee', 'Michigan', 'New York']

  constructor(private fb: FormBuilder, private advertService: AdvertService,
    private route: ActivatedRoute,
    private router: Router) {

  }


  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    this.removeWhiteSpace = new RegExp("\\S");

    this.advertForm = this.fb.group({
      advertHeadlineText:['', [Validators.required, Validators.minLength(10),Validators.maxLength(100), Validators.pattern(this.removeWhiteSpace)]],
      province:['', Validators.required],
      city: ['', Validators.required],
      price:  ['', [Validators.required, Validators.minLength(10000),Validators.maxLength(100000000)]],
      advertDetails: '',
      userId: '',
      releaseDate:''//  ['', [Validators.required,Validators.minLength(10), Validators.maxLength(1000)]],
    });

    // Read the advert Id from the route parameter
    if (!this.isAddMode) {
      this.advertService.getById(this.id)
        .pipe(first())
        .subscribe(x => this.advertForm.patchValue(x));
    }

  }

  // convenience getter for easy access to form fields
  get f() { return this.advertForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.advertForm.invalid) {
      return;
    }

    this.loading = true;
    if (this.isAddMode) {
      this.createAdvert();
    } else {
      this.updateAdvert();
    }
  }

  private createAdvert() {
    let currUser = JSON.parse(localStorage.getItem("currentUser"));
    this.advertForm.controls['userId'].setValue(currUser["id"]);
    this.advertService.create(this.advertForm.value)
      .pipe(first())
      .subscribe({
        next: () => {
          alert('Advert added successfuly');
          this.router.navigate(['/advert'], { relativeTo: this.route });
        },
        error: error => {
          alert(error);
          this.loading = false;
        }
      });
  }

  private updateAdvert() {
    this.advertService.update(this.id, this.advertForm.value)
      .pipe(first())
      .subscribe({
        next: () => {
          alert('Advert updated successfuly');
          this.router.navigate(['/advert'], { relativeTo: this.route });
        },
        error: error => {
          alert(error);
          this.loading = false;
        }
      });
  }

  //remove advert from the list 
  deleteAdvert(): void {
    if (confirm(`Are you sure you want to remove: ${this.advertForm.value.advertHeadlineText}?`)) {
      this.advertService.delete(this.id)
        .pipe(first())
        .subscribe({
          next: () => {
            alert('Advert deleted successfuly');
            this.router.navigate(['/advert'], { relativeTo: this.route });
          },
          error: error => {
            alert(error);
            this.loading = false;
          }
        });
    }
  }


}
