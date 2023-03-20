import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sidenavbar',
  templateUrl: './sidenavbar.component.html',
  styleUrls: ['./sidenavbar.component.css']
})
export class SidenavbarComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  fields:any[]= [
    {
      Name: 'Home',
      url:'/home',
    },
    {
      Name: 'Pinchange',
      url:'/Pinchage',
    },
    {
      Name: 'Transactions',
      url:'/transactions',
    },
    {
      Name: 'MiniStatement',
      url:'/ministatement',
    }
  ] ;
}
