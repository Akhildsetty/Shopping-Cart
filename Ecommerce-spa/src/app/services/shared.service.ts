import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor(
    private http: HttpClient
  ) { }

  baserurl=environment.api+'Shared/'

  getallcountrycodes(){
    return this.http.get(this.baserurl+'countryCode');
  }

  getuserbyemail(email:any){
    return this.http.get(this.baserurl+'getuserbyEmail/'+email);
  }

  getallroles(){
    return this.http.get(this.baserurl+'getallroles');
  }
  getallstates(id:any){
    return this.http.get(this.baserurl+'StateCode/'+id);
  }
  getallDistricts(id:any){
    return this.http.get(this.baserurl+'DistrictCode/'+id);
  }
}
