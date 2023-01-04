import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
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
