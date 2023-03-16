import { NotificationService } from './../../services/notification.service';
import { SharedService } from 'src/app/services/shared.service';
import { Router } from '@angular/router';
import { AccountService } from './../../services/account.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  user = {
    email: '',
    password: '',
    confirmPassword: '',
  };
  loading:boolean=false;
  resetPassword: boolean = false;
  constructor(
    private account: AccountService,
    private route: Router,
    private shared: SharedService,
    private notify: NotificationService
  ) {}

  ngOnInit(): void {}

  login(form: any) {
    this.loading=true;
    const formmodel = {
      Email: form.email,
      Password: form.password,
    };
    this.account.login(formmodel).subscribe(
      (data) => {
        this.account.localstoragesetinfo(data);
        
        this.route.navigate(['/dashboard']);
      },
      (err) => {
        console.log(err)
        this.notify.fail(err.error, 'Error');
        this.loading=false;
      }
    );
  }

  forgotpassword() {
    if (!this.resetPassword) {
      var email = this.user.email;
      this.shared.getuserbyemail(email).subscribe((data) => {
        if (data) {
          this.resetPassword = true;
        } 
      },
      (err)=>{
        console.log(err);
        this.notify.fail(err.error.detail, 'Error');
        this.loading=false;
      });
    } else {
      if (this.user.password == this.user.confirmPassword) {
        const formmodel = {
          email: this.user.email,
          password: this.user.password,
        };
        this.account.updatepassword(formmodel).subscribe((data) => {
          
            this.notify.success(data.message,"Success");
        },
        (err) => {
          console.log(err);
          this.notify.fail(err.error.detail, 'Error');
          this.loading=false;
        });
      } else {
        this.notify.warn("Password mismatch", 'warning');
      }
      this.resetPassword=false;
      this.route.navigate(['']); 
      
    }
  }
}
