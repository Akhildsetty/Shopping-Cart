import { AccountService } from './../../services/account.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/services/shared.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {
  
  countryCode:any;
  result:string='';
  constructor(
    private account :AccountService,
    private route:Router,
    private shared:SharedService,
    
  ) { }

  ngOnInit(): void {
    this.getallcountrycodes()
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
  register(form:any){
    if(form.passWord==form.ConfirmPassword){
      const formmodel={
        FirstName:form.firstName,
        LastName:form.lastName,
        Email:form.email,
        PhoneNumber:form.countrycode+' '+form.phoneNumber,
        Password:form.passWord
      }
      this.account.adduser(formmodel).subscribe(
        data=>{
          if(data){
            Swal.fire("Regitration Successfull")
            this.route.navigate(['/login'])
          }
          else{
            Swal.fire("Regitration failed")
          }
        },
        err=>{console.log(err)}
      )
    }
    else{
      Swal.fire("Pass Word Miss match")
    }
    
  }
}
