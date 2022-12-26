import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private toaster:ToastrService) { }
  success(message:any,title:any){
    this.toaster.success(message,title);
  }
  warn(message:any,title:any){
    this.toaster.warning(message,title);
  }
  fail(message:any,title:any){
    this.toaster.error(message,title);
  }
}
