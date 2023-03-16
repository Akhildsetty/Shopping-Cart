import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MinistatementinfoRoutingModule } from './ministatementinfo-routing.module';
import { MinistatementinfoComponent } from './ministatementinfo/ministatementinfo.component';


@NgModule({
  declarations: [
    MinistatementinfoComponent
  ],
  imports: [
    CommonModule,
    MinistatementinfoRoutingModule
  ]
})
export class MinistatementinfoModule { }
