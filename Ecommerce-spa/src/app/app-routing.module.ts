import { AuthGuard } from './services/auth.guard';
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
    path:"home",loadChildren :()=>import("../app/components/home/home.module").then(x=>x.HomeModule),canActivate:[AuthGuard]
  },
  {
    path:"profile",loadChildren:()=>import('../app/components/profile/profile.module').then(x=>x.ProfileModule),canActivate:[AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
