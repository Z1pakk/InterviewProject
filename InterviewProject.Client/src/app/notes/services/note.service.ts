import {Injectable} from "@angular/core";
import {IFilterCommand} from "../../features/filter/interfaces/filter-command";
import {Observable} from "rxjs";
import {IPageResult} from "../../features/filter/interfaces/page-result";
import {constructHttpParams} from "../../utils/construct-http-params";
import {HttpClient} from "@angular/common/http";
import {INote} from "../interfaces/note";

@Injectable()
export class NoteService {
  baseUrl = 'api/note'
  constructor(
    private http: HttpClient
  ) {
  }

  getPage(filterCommand?: IFilterCommand): Observable<IPageResult<INote>> {
    return this.http.get<IPageResult<INote>>(`${this.baseUrl}/page`, { params: constructHttpParams(filterCommand) });
  }

  get(id: number): Observable<INote> {
    return this.http.get<INote>(`${this.baseUrl}?id=${id}`);
  }

  create(data: INote): Observable<INote> {
    return this.http.post<INote>(`${this.baseUrl}`, data);
  }

  update(id: number, data: INote): Observable<INote> {
    return this.http.put<INote>(`${this.baseUrl}/${id}`, data);
  }

  delete(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/${id}`);
  }
}
