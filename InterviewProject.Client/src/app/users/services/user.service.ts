import {Injectable} from "@angular/core";
import {IFilterCommand} from "../../features/filter/interfaces/filter-command";
import {Observable} from "rxjs";
import {IPageResult} from "../../features/filter/interfaces/page-result";
import {IUser} from "../interfaces/user";
import {HttpClient} from "@angular/common/http";
import {constructHttpParams} from "../../utils/construct-http-params";

@Injectable()
export class UserService {
  baseUrl = "api/user"

  constructor(
    private http: HttpClient
  ) {
  }

  getPage(filterCommand?: IFilterCommand): Observable<IPageResult<IUser>> {
    return this.http.get<IPageResult<IUser>>(`${this.baseUrl}/page`, { params: constructHttpParams(filterCommand) });
  }

  get(id: string): Observable<IUser> {
    return this.http.get<IUser>(`${this.baseUrl}?id=${id}`);
  }

  create(data: IUser): Observable<IUser> {
    return this.http.post<IUser>(`${this.baseUrl}`, data);
  }

  update(id: string, data: IUser): Observable<IUser> {
    return this.http.put<IUser>(`${this.baseUrl}/${id}`, data);
  }

  delete(id: string): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/${id}`);
  }
}
