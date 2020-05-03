import { Injectable } from '@angular/core';
import * as decode from 'jwt-decode';

@Injectable()
export class RoleService {
  private key = 'oidc.user:';
  private adminRoleName: string = 'Admin';

  hasAdminRole() {
    this.key += window.location.origin + ':test2';
    var data = JSON.parse(sessionStorage.getItem(this.key));
    if (!data)
      return false;
    else if (!data.access_token)
      return false;
    var user = decode(data.access_token);
    if (!user || !user.role || !user.role.length || user.role.indexOf(this.adminRoleName) == -1)
      return false;

    return true;
  }

  //isAuthorized() {
  //  this.key += window.location.origin + ':test2';
  //  var data = JSON.parse(sessionStorage.getItem(this.key));
  //  if (!data)
  //    return false;
  //  else if (!data.access_token)
  //    return false;
  //  var user = decode(data.access_token);
  //  return !!user
  //}

}


