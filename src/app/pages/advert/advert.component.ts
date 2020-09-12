import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { User,Advert } from '@app/_models';

import { UserService, AuthenticationService,AdvertService } from '@app/_services';
import '@app/shared/advert.css';

@Component({
  selector: 'app-advert',
  templateUrl: './advert.component.html',
  styleUrls:['../../shared/advert.css']
})
export class AdvertComponent implements OnInit {
  loading = false;
  users: User[];
  currentUser: User;
  adverts: Advert[];

  constructor(private userService: UserService, private authenticationService: AuthenticationService,
    private advertService: AdvertService) {
    this.currentUser = this.authenticationService.currentUserValue;
  }
  ngOnInit() {
    this.loading = true;
    this.userService.getAllUsers().pipe(first()).subscribe(users => {
      this.loading = false;
      this.users = users;
    });

    //get all available advert
    // this.advertService.getAll().subscribe({
    //   next: adverts => {
    //     this.adverts = adverts;
    //   }
    // })

     /** 
     * Return all advert posted by a specific user 
     */
    this.advertService.getAdvertsByUserId(this.currentUser.id)
      .subscribe({
        next: adverts => {
          this.adverts = adverts;
        }
      })

  }
}