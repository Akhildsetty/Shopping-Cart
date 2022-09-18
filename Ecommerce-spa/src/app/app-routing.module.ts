import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { LogoutComponent } from './components/logout/logout.component';
import { SigninComponent } from './components/signin/signin.component';

const routes: Routes = [
  {
    path:"login",component:LoginComponent
  },
  {
    path:"signin",component:SigninComponent
  },
  {
    path:"logout",component:LogoutComponent
  },
  
  {
    path:"",redirectTo:"login",pathMatch:"full"
  },
  {
    path:"home",loadChildren :()=>import("../app/components/home/home.module").then(x=>x.HomeModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
