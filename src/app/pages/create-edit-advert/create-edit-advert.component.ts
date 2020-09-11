import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder, AbstractControl } from '@angular/forms';
import { AdvertService } from '@app/_services';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { ProvinceModel, CityModel } from '@app/_models';
import { Observable } from 'rxjs';
import { DatePipe } from '@angular/common';

@Component({
  templateUrl: './create-edit-advert.component.html',
  providers: [DatePipe]
})
export class CreateEditAdvertComponent implements OnInit {

  advertForm: FormGroup;
  id: number;
  isAddMode: boolean;
  loading = false;
  submitted = false;
  removeWhiteSpace: RegExp;
  _allProvince: ProvinceModel[];
  _allCity: CityModel[];
  currentDate: any = new Date();
  advStatus: string;

  constructor(private fb: FormBuilder, private advertService: AdvertService,
    private route: ActivatedRoute,
    private router: Router, private datePipe: DatePipe) {

    this.currentDate = this.datePipe.transform(this.currentDate, 'dd-MM-yyyy');
    this.advStatus = "live";
  }


  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    this.removeWhiteSpace = new RegExp("\\S");

    this.advertForm = this.fb.group({
      advertHeadlineText: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(100), Validators.pattern(this.removeWhiteSpace)]],
      province: ['', Validators.required],
      city: ['', Validators.required],
      price: ['', [Validators.required,
      (control: AbstractControl) => Validators.min(10000)(control),
      (control: AbstractControl) => Validators.max(100000000)(control)]],
      advertDetails: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(1000), Validators.pattern(this.removeWhiteSpace)]],
      userId: '',
      releaseDate: '',
      advertStatus: ''
    });

    this.FillProvinceDropdown();

    // Read the advert Id from the route parameter
    if (!this.isAddMode) {
      this.advertService.getById(this.id)
        .pipe(first())
        .subscribe(x => this.advertForm.patchValue(x));
    }

  }

  // convenience getter for easy access to form fields
  get f() { return this.advertForm.controls; }


  //populate the province dropdown
  FillProvinceDropdown() {
    this.advertService.getAllProvince()
      .pipe(first()).subscribe(province => {
        this._allProvince = province;
      });
  }

  //populate the city dropdown based on the province selected
  FillCityDropdown(e) {
    var prov = this._allProvince.find(x => x.province == e)
    this.advertService.getCity(prov.id)
      .pipe(first()).subscribe(cities => {
        this._allCity = cities;
      });
  }


  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.advertForm.invalid) {
      return;
    }
    this.advertForm.controls['releaseDate'].setValue(this.currentDate);

    this.loading = true;
    if (this.isAddMode) {
      this.createAdvert();
    } else {
      this.updateAdvert();
    }
  }

  //Add new advert method
  private createAdvert() {
    this.advertForm.controls['advertStatus'].setValue(this.advStatus);
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

  //update advert mthod
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
  removeAdvert(): void {
    if (confirm(`Are you sure you want to remove: ${this.advertForm.value.advertHeadlineText}?`)) {
      this.advStatus = "deleted";
      this.advertForm.controls['advertStatus'].setValue(this.advStatus);
      this.updateAdvert();
      this.router.navigate(['/advert'], { relativeTo: this.route });
    }
  }

  //remove advert from the list and on datadase permanently 
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

  //hide the advert
  hideAdvert(): void {
    if (confirm(`Are you sure you want to hide the advert with title: ${this.advertForm.value.advertHeadlineText}?`)) {
      this.advStatus = "hiden";
      this.advertForm.controls['advertStatus'].setValue(this.advStatus);
      this.updateAdvert();
    }
  }


}
