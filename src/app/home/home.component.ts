import { Component } from '@angular/core';
import { first } from 'rxjs/operators';

import { User, Advert } from '@app/_models';
import { UserService, AuthenticationService, AdvertService } from '@app/_services';
import '@app/shared/advert.css';

@Component({
  templateUrl: 'home.component.html',
  styleUrls: ['../shared/advert.css']
})
export class HomeComponent {
  adverts: Advert[];
  pageOfItems: Array<any>;

  constructor(private advertService: AdvertService) { }

  ngOnInit() {
    this.advertService.getAllAdvert()
      .subscribe({
        next: adverts => {
          this.adverts = adverts;
        }
      })
  }

  onChangePage(pageOfItems: Array<any>) {
    // update current page of items
    this.pageOfItems = pageOfItems;
  }
}