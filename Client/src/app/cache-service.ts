import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CacheService {
  private apiUrl = 'https://localhost:7238/Cache/Add';

  constructor(private http: HttpClient) {}

  addUser(name: string, surname: string): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      accept: '*/*',
    });

    const body = { name: name, surname: surname };

    return this.http.post<any>(this.apiUrl, body, { headers });
  }
}
