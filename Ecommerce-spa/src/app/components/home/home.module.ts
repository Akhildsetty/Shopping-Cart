import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home/home.component';
import { TopnavbarComponent } from './topnavbar/topnavbar.component';
import { ProductinfoComponent } from './productinfo/productinfo.component';
import { ProfileModule } from './profile/profile.module';


@NgModule({
  declarations: [
    HomeComponent,
    TopnavbarComponent,
    ProductinfoComponent,
    
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    ProfileModule
  ]
})
export class HomeModule { }
