import {ActivatedRouteSnapshot, CanActivateChildFn, CanActivateFn, Router, RouterStateSnapshot} from "@angular/router";
import {inject} from "@angular/core";
import {AccountService} from "../../account/services/account.service";

export const canAuthenticatedActivate: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const authService = inject(AccountService);
  const router = inject(Router);

  if (!authService.isLogged) {
    router.navigate(['/login']);
    return false;
  }

  return true;
};

export const canAuthenticatedActivateChild: CanActivateChildFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => canAuthenticatedActivate(route, state);
