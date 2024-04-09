import {HttpParams} from "@angular/common/http";
import {flatten} from "./flatten";

export const constructHttpParams = (obj: any): HttpParams => {
  let params = new HttpParams();
  const flattenBody = flatten(obj);

  Object.keys(flattenBody).forEach(prop => {
    params = params.set(prop, flattenBody[prop]);
  });

  return params;
}
