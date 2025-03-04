import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private baseUrl = 'https://localhost:7056/api';
   private apiKey = '9a1vg8hdh2iy7wqx3opzny64jhxuy5vcg'

  constructor(private http: HttpClient) { }

  private getHeaders() {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'apiKey': this.apiKey  // Include the API key in headers
    });
  }

 // Fetch all users
 getUsers(): Observable<any[]> {
  return this.http.get<any[]>(`${this.baseUrl}/users/GetAllUsers`, { headers: this.getHeaders() });
}

// Fetch all customers
getCustomers(): Observable<any[]> {
  return this.http.get<any[]>(`${this.baseUrl}/customers/GetAllCustomer`);
}

// Fetch all products
getProducts(): Observable<any[]> {
  return this.http.get<any[]>(`${this.baseUrl}/products/GetAllProducts`);
}

// Fetch all sales orders
getSalesOrders(): Observable<any[]> {
  return this.http.get<any[]>(`${this.baseUrl}/sales-order/GetAll`);
}

// Fetch all purchase orders
getPurchaseOrders(): Observable<any[]> {
  return this.http.get<any[]>(`${this.baseUrl}/purchase-order/GetAll`);
}

addUser(user: any): Observable<any> {
  return this.http.post(`${this.baseUrl}/users/AddUser`, user);
}

updateUser(userId: number, user: any): Observable<any> {
  return this.http.put(`${this.baseUrl}/users/UpdateUser/${userId}`, user);
}

deleteUser(userId: number): Observable<any> {
  return this.http.delete(`${this.baseUrl}/users/DeleteUser/${userId}`);
}

}
