import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { error } from 'console';

import { Users } from 'src/app/Models/users.model';
import { AccountService } from 'src/app/services/account.service';
import { NotificationService } from 'src/app/services/notification.service';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  @ViewChild('closebutton') closebutton: any;
  user: any;

  email: any;
  role: any;
  updateprofile: boolean = false;
  loading: boolean = false;
  profile = {
    password: '',
    confirmPassword: ''
  };

  constructor(
    private accountservice: AccountService,
    private sharedservice: SharedService,
    private notify: NotificationService,
    private formbuilder: FormBuilder,
    private route: Router

  ) { }

  ngOnInit(): void {
    this.email = localStorage.getItem('email');
    this.role = localStorage.getItem('Role');
    this.getuserbyemail(this.email);
  }
  getuserbyemail(email: any) {
    this.sharedservice.getuserbyemail(email).subscribe(
      (data) => {
        this.user = data;
      }
    )
  }
  DeleteAccount() {
    this.accountservice.deletebyemail(this.email).subscribe(
      (data) => {
        this.user = data;
        this.notify.success(this.user.message, "success")
        this.route.navigate(['']);
      },
      (err) => {
        this.notify.fail(err.error.detail, "Error")
      }
    )
  }

  ResetPassword() {
    if (this.profile.password == this.profile.confirmPassword) {
      const formmodel = {
        email: this.email,
        password: this.profile.password,
      };
      this.accountservice.updatepassword(formmodel).subscribe((data) => {

        this.notify.success(data.message, "Success");
        this.closebutton.nativeElement.click();
      },
        (err) => {
          console.log(err);
          this.notify.fail(err.error.detail, 'Error');
          this.loading = false;
        });
    } else {
      this.notify.warn("Password mismatch", 'warning');

    }
  }

  update() {
    this.route.navigate(['home/profile/UpdateProfile'])
  }

}
