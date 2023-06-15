import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { ServiceParent } from '../parents/service-parent';

@Injectable({
  providedIn: 'root'
})
export class JourneyService extends ServiceParent {

  constructor(private http: HttpClient) {
    super();
  }

  Get(origin: string, destination: string) {
    // return this.http.get(`${environment.urlApi}/Flight/`, param)
    //   .pipe(map(data => {
    //     return data;
    //   }));
    
    return this.http.get(`${environment.urlApi}/Flight/${origin}/${destination}`)
      .pipe(map(data => {
        return data;
      }));
  }  
}