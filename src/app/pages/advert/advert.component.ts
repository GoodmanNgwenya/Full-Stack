import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { User,Advert } from '@app/_models';

import { UserService, AuthenticationService,AdvertService } from '@app/_services';

@Component({
  selector: 'app-advert',
  templateUrl: './advert.component.html',
  styleUrls: ['./advert.component.css']
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
    this.userService.getAll().pipe(first()).subscribe(users => {
      this.loading = false;
      this.users = users;
    });

    //get all available advert
    this.advertService.getAll().subscribe({
      next: adverts => {
        this.adverts = adverts;
      }
    })

  }
}