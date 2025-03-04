import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
   private apiUrl = 'http://localhost:5000/api/users';

  constructor(private http: HttpClient) { }
  // Fetch all users
  getUsers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/all`);
  }

  // Add a new user
  addUser(user: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/create`, user);
  }

  // Update an existing user
  updateUser(userId: number, user: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/update/${userId}`, user);
  }

  // Delete a user
  deleteUser(userId: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/delete/${userId}`);
  }
}
