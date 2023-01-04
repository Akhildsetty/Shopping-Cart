import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-topnavbar',
  templateUrl: './topnavbar.component.html',
  styleUrls: ['./topnavbar.component.css']
})
export class TopnavbarComponent implements OnInit {

  user:any;
  constructor(private route:Router) { }

  ngOnInit(): void {
    this.user=localStorage.getItem('Full Name');
  }
  logout(){
    localStorage.clear();
    this.route.navigate(['login']);

  }

}
