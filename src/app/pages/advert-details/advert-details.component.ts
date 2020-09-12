import { Component, OnInit } from '@angular/core';
import { AdvertService } from '@app/_services';
import { first } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { Advert } from '@app/_models';
import '@app/shared/advert.css';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-advert-details',
  templateUrl: './advert-details.component.html',
  styleUrls: ['../../shared/advert.css']
})
export class AdvertDetailsComponent implements OnInit {
  id: number;
  advert: Advert;
  advertForm: FormGroup;

  constructor(private fb: FormBuilder, private advertService: AdvertService,
    private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];

    this.advertForm = this.fb.group({
      advertHeadlineText: '',
      province: '',
      city: '',
      price: '',
      advertDetails: '',
      userId: '',
      releaseDate: '',
      advertStatus: ''
    });

    this.advertService.getAdvertById(this.id)
      .subscribe(x => this.advertForm.patchValue(x));


  }

  // convenience getter for easy access to form fields
  get f() { return this.advertForm.controls; }

  getControlLabel(type: string){
    return this.advertForm.controls[type].value;
   }

}
