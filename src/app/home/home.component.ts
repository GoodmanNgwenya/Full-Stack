import { Component } from '@angular/core';
import { first } from 'rxjs/operators';

import { User, Advert } from '@app/_models';
import { UserService, AuthenticationService, AdvertService } from '@app/_services';

@Component({
    templateUrl: 'home.component.html',
    styleUrls:['home.component.css']
})
export class HomeComponent {
    adverts: Advert[];
    
    constructor(private advertService: AdvertService) { }

    ngOnInit() {
        this.advertService.getAll()
      .subscribe({
        next: adverts => {
          this.adverts = adverts;
        }
      })
    }
}