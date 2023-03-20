import { AccountService } from './../../services/account.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/services/shared.service';
import Swal from 'sweetalert2';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {
  Role:string='';
  Code:string='';
  countryCode:any;
  result:string='';
  roles:any;
  
  constructor(
    private account :AccountService,
    private route:Router,
    private shared:SharedService,
    private notify:NotificationService
    
  ) { }

  ngOnInit(): void {
    this.getallcountrycodes();
    this.getallroles();
  }

  getallcountrycodes(){
    this.shared.getallcountrycodes().subscribe(
      data=>{
        this.countryCode=data
        console.log(this.countryCode)
      },
      err=>{
        console.log(err)
      }
    )
  }
  getallroles(){
    this.shared.getallroles().subscribe(
      data=>{
        this.roles=data
        console.log(this.roles)
      },
      err=>{
        console.log(err)
      }
    )
  }
  register(form:any){
    if(form.passWord==form.ConfirmPassword){
      const formmodel={
        FirstName:form.firstName,
        LastName:form.lastName,
        Email:form.email,
        PhoneNumber:form.countrycode+' '+form.phoneNumber,
        Password:form.passWord,
        Role:form.role
      }
      this.account.adduser(formmodel).subscribe(
        data=>{
          this.notify.success(data.message,"success");
            this.route.navigate(['/login'])
        },
        err=>{console.log(err);
          this.notify.fail(err.error.detail,"Error")}
      )
    }
    else{
      this.notify.warn("Pass Word Miss match","waring")
    }
    
  }
}
