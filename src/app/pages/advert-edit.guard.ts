import { Injectable } from '@angular/core';
import { CanDeactivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { CreateEditAdvertComponent } from '.';

@Injectable({
  providedIn: 'root'
})
// export class AdvertEditGuard implements CanActivate, CanDeactivate<unknown> {
//   canActivate(
//     next: ActivatedRouteSnapshot,
//     state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
//     return true;
//   }
//   canDeactivate(
//     component: unknown,
//     currentRoute: ActivatedRouteSnapshot,
//     currentState: RouterStateSnapshot,
//     nextState?: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
//     return true;
//   }

export class AdvertEditGuard implements CanDeactivate<CreateEditAdvertComponent> {
  canDeactivate(component: CreateEditAdvertComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.advertForm.dirty) {
      const advertHeadlineText = component.advertForm.get('advertHeadlineText').value || 'New Advert';
      return confirm(`Navigate away and lose all changes to ${advertHeadlineText}?`);
    }
    return true;
  }

}
