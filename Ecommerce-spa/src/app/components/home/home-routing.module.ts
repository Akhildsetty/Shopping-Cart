import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductinfoComponent } from './productinfo/productinfo.component';
import { ProfileComponent } from '../profile/profile/profile.component';

const routes: Routes = [
  
  {
    path:'',component:HomeComponent,
    children:[
      {
        path:'getproducts', component:ProductinfoComponent
      },
       {
         path:'profile', component:ProfileComponent
       },
      
    ]
  }
  
 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
