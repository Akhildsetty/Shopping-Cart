import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(
    private http: HttpClient
  ) { }
  baseurl = environment.api + 'Account/'

  adduser(userifo: any) {
    return this.http.post<any>(this.baseurl + 'addUser', userifo)
  }

  login(logininfo: any) {
    return this.http.post<any>(this.baseurl + 'login', logininfo)
  }

  updatepassword(userifo: any) {
    return this.http.post<any>(this.baseurl + 'UpdatePassword', userifo)
  }

localstoragesetinfo(data:any){
  localStorage.setItem('token', data.token);
  localStorage.setItem('Full Name',data.fullName);
  localStorage.setItem('email',data.email)
  localStorage.setItem('Role',data.role)
}

}
