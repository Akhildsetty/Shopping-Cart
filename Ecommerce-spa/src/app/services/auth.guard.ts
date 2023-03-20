import { AuthService } from './auth.service';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private auth: AuthService,
    private route: Router,
    private notify: NotificationService
  ) {}
  canActivate() {
    if (this.auth.isLoggedin()) {
      return true;
    }else{
      this.notify.warn("You are not logged in","warning");
      this.route.navigate([''])
      return false;
    }
  }
}
