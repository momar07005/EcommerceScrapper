import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from './../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class RepositoryService {

  constructor(private http: HttpClient) { }

  public getAllReviews(route: string): Observable<any> {
    return this.http.get(this.createCompleteRequest(route, environment.webApiAdress));
  }

  public getAllRequests(route: string): Observable<any>  {
    return this.http.get(this.createCompleteRequest(route, environment.webApiAdress));
  }

  public createIndexationRequest = (route: string, body) => {
    return this.http.post(this.createCompleteRequest(route, environment.webApiAdress), body, this.generateHeaders());
  }

  private createCompleteRequest = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }

  private generateHeaders = () => {
    return {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }
  }
}
