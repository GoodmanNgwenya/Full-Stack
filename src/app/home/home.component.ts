import { Component } from '@angular/core';
import { first } from 'rxjs/operators';

import { User, Advert, ProvinceModel, CityModel } from '@app/_models';
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
  _allProvince: ProvinceModel[];
  _allCity: CityModel[];



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

    this.FillProvinceDropdown();
  }


  performFilter(searchBy: string): Advert[] {
    searchBy = searchBy.toLocaleLowerCase();
    return this.adverts.filter((advert: Advert) =>
      advert.advertHeadlineText.toLocaleLowerCase().indexOf(searchBy) !== -1 ||
      advert.advertDetails.toLocaleLowerCase().indexOf(searchBy) !== -1);

  }

  //populate the province dropdown
  FillProvinceDropdown() {
    this.advertService.getAllProvince()
      .pipe(first()).subscribe(province => {
        this._allProvince = province;
      });
  }

  //filter by province
  public selectedProvince;
  public valueSelected(e) {
    this.advertService.searchAdvert(e)
      .subscribe({
        next: adverts => {
          this.adverts = adverts.filter(advert => advert.province === this.selectedProvince);
        }
      })

    this.FillCityDropdown(e);
  }

  //filter by city
  public selectedCity;
  public citySelected() {
    this.advertService.getAllAdvert()
      .subscribe({
        next: adverts => {
          this.adverts = adverts.filter(advert => advert.city === this.selectedCity);
        }
      })
  }

  //populate the city dropdown based on the province selected
  FillCityDropdown(e) {
    var prov = this._allProvince.find(x => x.province == e)
    this.advertService.getCity(prov.id)
      .pipe(first()).subscribe(cities => {
        this._allCity = cities;
      });

  }

  //sorting by high and low price
  sort(event: any) {
    switch (event.target.value) {
      case "Low":
        {
          this.advertService.getAllAdvert()
            .subscribe({
              next: adverts => {
                this.adverts = adverts.sort((low, high) => low.price - high.price);
              }
            });
          break;
        }

      case "High":
        {
          this.advertService.getAllAdvert()
            .subscribe({
              next: adverts => {
                this.adverts = adverts.sort((low, high) => high.price - low.price);
              }
            });
          break;
        }
      default: {
        this.advertService.getAllAdvert()
          .subscribe({
            next: adverts => {
              this.adverts = adverts.sort((low, high) => low.price - high.price);
            }
          });
        break;
      }

    }
    return this.adverts;

  }

  //filter by price
  searchByPrice(event: any) {
    switch (event.target.value) {
      case "10K":
        {
          this.advertService.getAllAdvert()
            .subscribe({
              next: adverts => {
                this.adverts = adverts.filter(advert => advert.price >= 10000 && advert.price < 50000);
              }
            });
          break;
        }

      case "50K":
        {
          this.advertService.getAllAdvert()
            .subscribe({
              next: adverts => {
                this.adverts = adverts.filter(advert => advert.price >= 50000 && advert.price < 100000);
              }
            });
          break;
        }
      case "100K":
        {
          this.advertService.getAllAdvert()
            .subscribe({
              next: adverts => {
                this.adverts = adverts.filter(advert => advert.price >= 100000 && advert.price < 150000);
              }
            });
          break;
        }
      case "150K":
        {
          this.advertService.getAllAdvert()
            .subscribe({
              next: adverts => {
                this.adverts = adverts.filter(advert => advert.price >= 150000 && advert.price < 200000);
              }
            });
          break;
        }
      case "200K":
        {
          this.advertService.getAllAdvert()
            .subscribe({
              next: adverts => {
                this.adverts = adverts.filter(advert => advert.price >= 200000 && advert.price < 250000);
              }
            });
          break;
        }
      case "250K":
        {
          this.advertService.getAllAdvert()
            .subscribe({
              next: adverts => {
                this.adverts = adverts.filter(advert => advert.price >= 250000 && advert.price < 300000);
              }
            });
          break;
        }
      case "300K":
        {
          this.advertService.getAllAdvert()
            .subscribe({
              next: adverts => {
                this.adverts = adverts.filter(advert => advert.price >= 300000 && advert.price < 1000000);
              }
            });
          break;
        }
      case "1M":
        {
          this.advertService.getAllAdvert()
            .subscribe({
              next: adverts => {
                this.adverts = adverts.filter(advert => advert.price >= 1000000 && advert.price < 50000000);
              }
            });
          break;
        }
      case "50M":
        {
          this.advertService.getAllAdvert()
            .subscribe({
              next: adverts => {
                this.adverts = adverts.filter(advert => advert.price >= 50000000 && advert.price < 150000);
              }
            });
          break;
        }
      case "100M":
        {
          this.advertService.getAllAdvert()
            .subscribe({
              next: adverts => {
                this.adverts = adverts.filter(advert => advert.price >= 50000000 && advert.price < 100000000);
              }
            });
          break;
        }
      default: {
        this.advertService.getAllAdvert()
          .subscribe({
            next: adverts => {
              this.adverts = adverts.filter(advert => advert.price >= 10000 && advert.price < 50000);
            }
          });
        break;
      }

    }
    return this.adverts;

  }

  //paging
  pageChanged(event) {
    this.config.currentPage = event;
  }

  //reseting all field
  onReset(): void {
    window.location.reload();
  }

}