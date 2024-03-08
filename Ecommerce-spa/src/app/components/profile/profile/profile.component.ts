import { Component, OnInit } from '@angular/core';

import { Users } from 'src/app/Models/users.model';
import { AccountService } from 'src/app/services/account.service';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: any;
  Userinfo!: Users;
  email: any;
  role: any;
  updateprofile:boolean=false;
  constructor(
    private accountservice: AccountService,
    private sharedservice: SharedService

  ) { }

  ngOnInit(): void {
    this.email = localStorage.getItem('email');
    this.role = localStorage.getItem('Role');
    this.getuserbyid();
  }
  getuserbyid() {
    this.sharedservice.getuserbyemail(this.email).subscribe(
      (data) => {
        this.user = data;
        this.Userinfo = this.user;
        console.log(this.user);
      }
    )
  }
  DeleteAccount(){

  }
}
