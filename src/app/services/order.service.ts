import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl = 'http://localhost:5000/api/orders';

  constructor(private http: HttpClient) { }

  // Fetch all sales orders
  getSalesOrders(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/sales`);
  }

  // Fetch all purchase orders
  getPurchaseOrders(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/purchases`);
  }

  // Add a new sales order
  addSalesOrder(order: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/sales/create`, order);
  }

  // Add a new purchase order
  addPurchaseOrder(order: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/purchases/create`, order);
  }

  // Update an existing sales order
  updateSalesOrder(orderId: number, order: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/sales/update/${orderId}`, order);
  }

  // Update an existing purchase order
  updatePurchaseOrder(orderId: number, order: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/purchases/update/${orderId}`, order);
  }

  // Delete a sales order
  deleteSalesOrder(orderId: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/sales/delete/${orderId}`);
  }

  // Delete a purchase order
  deletePurchaseOrder(orderId: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/purchases/delete/${orderId}`);
  }
}
