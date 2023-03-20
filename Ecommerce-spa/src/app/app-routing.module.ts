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
    path:"Pinchage",loadChildren :()=>import("../app/components/pinchange/pinchange.module").then(x=>x.PinchangeModule)
  },
  {
    path:"ministatement",loadChildren :()=>import("../app/components/ministatementinfo/ministatementinfo.module").then(x=>x.MinistatementinfoModule)
  },
  {
    path:"transactions",loadChildren :()=>import("../app/components/transactions/transactions.module").then(x=>x.TransactionsModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
