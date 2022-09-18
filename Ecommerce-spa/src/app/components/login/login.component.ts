import  Swal  from 'sweetalert2';
import { SharedService } from 'src/app/services/shared.service';
import { Router } from '@angular/router';
import { AccountService } from './../../services/account.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user={
    email:"",
    password:'',
    confirmPassword:''
  };
  resetPassword:boolean=false;
  constructor(
    private account :AccountService,
    private route:Router,
    private shared:SharedService
  ) { }

  ngOnInit(): void {
  }

  login(form:any){
    const formmodel={
      Email:form.email,
      Password:form.password
    }
    this.account.login(formmodel).subscribe(
      data=>{
        if(data){
          Swal.fire("Login Successfull");
            this.route.navigate(['/home'])
        }
      },
      err=>{console.log(err)}
    )
  }

  forgotpassword(){
    if(!this.resetPassword){
      var email=this.user.email;
    this.shared.getuserbyemail(email).subscribe(
      data=>{
        if(data){
          this.resetPassword=true;
          
        }
      }
    )
    }else{
      if(this.user.password==this.user.confirmPassword){
        const formmodel={
          email:this.user.email,
          password:this.user.password
        }
        this.account.updatepassword(formmodel).subscribe(
          data=>{
            Swal.fire(data?"Password updated successfully":"Password updatation Failed")
          }
        )
      }else{
        Swal.fire("Password mismatch")
      }
      
    }
    
  }
}
