import { Observable, BehaviorSubject } from 'rxjs';
import { map, tap, filter } from 'rxjs/operators';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree } from '@angular/router'
import { AuthorizeService, } from './../authorize.service';
import { ApplicationPaths, QueryParameterNames } from './../api-authorization.constants';
import { Inject } from '@angular/core';
import * as decode from 'jwt-decode';
import { name } from './../../../package.json';

export interface IUser {
  name?: string;
}

@Inject({
  providedIn: 'root'
})
export class AuthorizeRoleGuard implements CanActivate {
  private userSubject: BehaviorSubject<IUser | null> = new BehaviorSubject(null);
  private key = 'oidc.user:';
  private adminRoleName: string = 'Admin';

  constructor(private authorize: AuthorizeService, private router: Router) { }
    
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    this.key += window.location.origin + ':' + name;
    var data = JSON.parse(sessionStorage.getItem(this.key));
    this.handleAuthorization(!!data);
    this.handleAuthorization(!!data.access_token);
    var user = decode(data.access_token);
    if (!user || !user.role || !user.role.length || user.role.indexOf(this.adminRoleName) == -1)
      this.handleAuthorization(false);

    return true;
  }

  private handleAuthorization(isAuthenticated: boolean) {
    if (!isAuthenticated)
      window.location.href = 'Identity/Account/Login?' + QueryParameterNames.ReturnUrl + "=/";
  }
}






















