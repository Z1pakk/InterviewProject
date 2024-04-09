import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {IUser} from "../../users/interfaces/user";
import {ILoginInput} from "../login/interfaces/login-input";
import {catchError, map, Observable, tap, throwError} from "rxjs";
import {Injectable} from "@angular/core";
import {AppStorage} from "../../utils/app-storage";

@Injectable()
export class AccountService {
  baseUrl = "api/account";

  private _currentUser: IUser | null = AppStorage.getItem<IUser>(AppStorage.CurrentUser);

  constructor(
    private http: HttpClient
  ) {
  }

  public get isLogged(): boolean {
    return !!this._currentUser;
  }

  public get isAdmin(): boolean {
    return !!this._currentUser && this.currentUser.isAdmin;
  }

  public get currentUser(): IUser {
    return <IUser>this._currentUser;
  }

  signIn(data: ILoginInput): Observable<IUser> {
    return this.http.post<IUser>(`${this.baseUrl}/login`, data).pipe(
        tap(user => {
          if (user) {
            this._currentUser = user;
            AppStorage.setItem(AppStorage.CurrentUser, user);
          }
        })
      );
  }

  me(): Observable<IUser> {
    return this.http.get<IUser>(`${this.baseUrl}/me`);
  }

  hasLoggedInUser(){
    return this.http.get<boolean>(`${this.baseUrl}/has-logged-in-user`);
  }

  logout(): Observable<any> {
    return this.http.post(`${this.baseUrl}/logout`, {}).pipe(
      map(r => {
          this._currentUser = null;
          AppStorage.setItem(AppStorage.CurrentUser, null);

          return r;
      })
    );
  }
}
