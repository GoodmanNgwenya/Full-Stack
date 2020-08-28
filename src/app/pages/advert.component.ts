import { Component, OnInit } from '@angular/core';

import { first } from 'rxjs/operators';
import { User } from '@app/_models';
import { UserService, AuthenticationService } from '@app/_services';

@Component({
  selector:"advert",
  templateUrl: './advert.component.html'
})
export class AdvertComponent implements OnInit {
  loading = false;
  users: User[];
  currentUser: User;

  constructor(private userService: UserService, private authenticationService: AuthenticationService) {
    this.currentUser = this.authenticationService.currentUserValue;
   }

  ngOnInit() {
    //this.userService.getById(this.currentUser.id);
    }

}
