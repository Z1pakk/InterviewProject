import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {Injectable, Injector} from "@angular/core";
import {catchError, Observable, throwError} from "rxjs";
import {AccountService} from "../../account/services/account.service";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";

@Injectable()
export class HttpStatusesInterceptor implements HttpInterceptor {
  constructor(private injector: Injector) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((err: HttpErrorResponse) => {
        if(err) {
          const toastrService = this.injector.get(ToastrService);
          let message = '';

          if(err?.error?.errors) {
            const values: string[] = Object.values(err.error.errors);
            message = values.join(";")
          } else {
            message = err.error;
          }

          toastrService.error(`Error during executing request: ${message}`, "Error");
        }

        if (err.status == 401) {
          const accountService = this.injector.get(AccountService);
          const router = this.injector.get(Router);

          if (accountService) {
            if (accountService.isLogged) {
              accountService.logout().subscribe(r => {
                if(router) {
                  router.navigate(["/login"])
                }
              });
            }
          }
        }

        return throwError(() => err);
      }));
  }
}
