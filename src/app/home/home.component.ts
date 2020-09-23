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
  _searchAdvert: string;
  config: any;


  filteredAdverts: Advert[] = [];

  get listAdvert(): string {
    return this._searchAdvert;
  }
  set listAdvert(value: string) {
    this._searchAdvert = value;
    this.filteredAdverts = this.listAdvert ? this.performFilter(this.listAdvert) : this.adverts;
  }


  constructor(private advertService: AdvertService) { }

  ngOnInit() {
    this.advertService.getAllAdvert()
      .subscribe({
        next: adverts => {
          this.adverts = adverts;
          this.filteredAdverts = adverts;
          this.config = {
            itemsPerPage: 10,
            currentPage: 1
          };
        }
      })
  }

  performFilter(searchBy: string): Advert[] {
    searchBy = searchBy.toLocaleLowerCase();
    return this.adverts.filter((advert: Advert) =>
      advert.advertHeadlineText.toLocaleLowerCase().indexOf(searchBy) !== -1 ||
      advert.advertDetails.toLocaleLowerCase().indexOf(searchBy) !== -1 ||
      advert.province.toLocaleLowerCase().indexOf(searchBy)!==-1);

  }

  // onChangePage(pageOfItems: Array<any>) {
  //   // update current page of items
  //   this.pageOfItems = pageOfItems;
  // }
  pageChanged(event) {
    this.config.currentPage = event;
  }
}