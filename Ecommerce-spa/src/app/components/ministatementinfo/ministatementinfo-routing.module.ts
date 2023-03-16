import { MinistatementinfoComponent } from './ministatementinfo/ministatementinfo.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [{
  path:"",component:MinistatementinfoComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MinistatementinfoRoutingModule { }
