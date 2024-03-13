import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProfileComponent } from '../profile/profile.component';
import { Users } from 'src/app/Models/users.model';
import { SharedService } from 'src/app/services/shared.service';
import { AccountService } from 'src/app/services/account.service';
import { NotificationService } from 'src/app/services/notification.service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-update-profile',
  templateUrl: './update-profile.component.html',
  styleUrls: ['./update-profile.component.css']
})
export class UpdateProfileComponent implements OnInit {
  country: string = '';
  countryCode: any;
  state: string = '';
  stateCode: any;
  district: string = '';
  districtCode: any;
  email: any;
  profile: any;
  profileInfo!: Users;
  UpdateResponse: any;
  districtname: any;
  statename: any;
  countryname: any;


  constructor(
    private formbuilder: FormBuilder,
    private sharedservice: SharedService,
    private accountservice: AccountService,
    private notify: NotificationService,
    private route: Router
  ) { }

  ngOnInit(): void {
    this.email = localStorage.getItem('email');
    this.getuserbyemail();
    this.getallcountrycodes();
  }
  getuserbyemail() {
    this.sharedservice.getuserbyemail(this.email).subscribe(
      (data) => {
        this.profile = data;
        this.profileInfo = this.profile;
        console.log(this.profileInfo);
      }
    )
  }
  getallcountrycodes() {
    this.sharedservice.getallcountrycodes().subscribe(
      data => {
        this.countryCode = data
        console.log(this.countryCode)
      },
      err => {
        console.log(err)
      }
    )
  }
  getstates(id: any) {
    this.sharedservice.getallstates(id).subscribe(
      (data) => {
        this.stateCode = data
      },
      err => {
        console.log(err)
      }
    )
  }
  getDistricts(id: any) {
    this.sharedservice.getallDistricts(id).subscribe(
      (data) => {
        this.districtCode = data
      },
      err => {
        console.log(err)
      }
    )
  }
  UpdateProfile(data: any) {

    for (let countryinfo of this.countryCode) {
      if (countryinfo.id == data.countrycode) {
        this.countryname = countryinfo.name
        break;
      }

    }
    for (let stateinfo of this.stateCode) {
      if (stateinfo.id == data.statecode) {
        this.statename = stateinfo.stateName
        break;
      }

    }
    for (let districtinfo of this.districtCode) {
      if (districtinfo.id == data.districtcode) {
        this.districtname = districtinfo.district
        break;
      }

    }
    const formmodal = {
      id: this.profileInfo.id,
      firstName: data.firstName,
      lastName: data.lastName,
      email: data.email,
      phoneNumber: data.phoneNumber,
      address1: data.address1,
      address2: data.address2,
      state: this.statename,
      country: this.countryname,
      district: this.districtname,
      pincode: data.pincode,
    }
    this.accountservice.updateprofile(formmodal).subscribe(
      (data) => {
        this.UpdateResponse = data
        this.notify.success(this.UpdateResponse.message, 'Success')
        this.route.navigate(['home/profile']);
      },
      (err) => {
        this.notify.fail(err.error.message, 'Error');
      }
    );
  }
}

