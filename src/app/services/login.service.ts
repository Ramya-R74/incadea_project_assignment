import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders  } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private apiUrl = 'https://localhost:7056/api/auth/login';

  // constructor(private http: HttpClient) { }
  // login(username: string, password: string, apiKey?: string): Observable<any> {
  //   return this.http.post(this.apiUrl, { username, password, apiKey });
  // }

  constructor(private http: HttpClient) {}

  login(username: string, password: string, apiKey?: string): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    console.log('Making API call to:', this.apiUrl); // ✅ Debugging
    // Include API Key only if the user is an Admin
    const loginData: any = { username, password };
    if (apiKey) {
      loginData.apiKey = apiKey;
    }

    return this.http.post<any>(this.apiUrl, loginData, { headers });
  }
}
