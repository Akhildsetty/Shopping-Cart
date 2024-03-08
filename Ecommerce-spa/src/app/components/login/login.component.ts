import { AuthService } from './../../services/auth.service';
import { NotificationService } from './../../services/notification.service';
import { SharedService } from 'src/app/services/shared.service';
import { Router } from '@angular/router';
import { AccountService } from './../../services/account.service';
import { Component, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  @ViewChild('closebutton') closebutton: any;
  user = {
    email: '',
    password: '',
    confirmPassword: '',
  };
  loading: boolean = false;
  resetPassword: boolean = false;
  otpValidation: boolean = false;
  validateotp: any;
  result: any;
  constructor(
    private account: AccountService,
    private route: Router,
    private shared: SharedService,
    private notify: NotificationService,
    private auth: AuthService
  ) { }

  ngOnInit(): void { }

  login(form: any) {
    this.loading = true;
    const formmodel = {
      Email: form.email,
      Password: form.password,
    };
    this.account.login(formmodel).subscribe(
      (data) => {
        this.account.localstoragesetinfo(data);

        this.route.navigate(['/home']);

      },
      (err) => {
        console.log(err)
        this.notify.fail(err.error, 'Error');
        this.loading = false;
      }
    );
  }

  forgotpassword() {
    if (!this.otpValidation && !this.resetPassword) {
      var email = this.user.email;
      this.account.sendOtpbyemail(email).subscribe((data) => {
        if (data) {
          this.result = data
          this.otpValidation = true;
          this.notify.success(this.result.message, "Success");
        }
      },
        (err) => {
          console.log(err);
          this.notify.fail(err.error.detail, 'Error');
          this.loading = false;
        });
    }
    else if (this.otpValidation && !this.resetPassword) {
      this.validateOtp();

      // this.otpValidation = false;
      // this.resetPassword = true;
    }
    else {
      this.resetUserPassword();
    }
  }

  validateOtp(): void {
    const otpcheck = {
      otp: this.validateotp,
      email: this.user.email
    }

    this.account.ValidateOTP(otpcheck).subscribe((data) => {
      if (data) {
        this.resetPassword = true
        this.otpValidation = true
      }
    },
      (err) => {
        console.log(err);
        this.notify.fail(err.error.detail, 'Error');
        this.loading = false;
      }
    )

  }
  resetUserPassword(): void {
    if (this.user.password == this.user.confirmPassword) {
      const formmodel = {
        email: this.user.email,
        password: this.user.password,
      };
      this.account.updatepassword(formmodel).subscribe((data) => {

        this.notify.success(data.message, "Success");
        this.otpValidation=false;
        this.resetPassword=false;
        this.user.email='';
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
    
    this.route.navigate(['']);
  }
}
