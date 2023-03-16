import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PinchangeRoutingModule } from './pinchange-routing.module';
import { ChangepinComponent } from './changepin/changepin.component';


@NgModule({
  declarations: [
    ChangepinComponent
  ],
  imports: [
    CommonModule,
    PinchangeRoutingModule
  ]
})
export class PinchangeModule { }
