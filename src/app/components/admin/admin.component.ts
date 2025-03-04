import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AdminService } from '../../services/admin.service';

// Import Bootstrap JavaScript
import * as bootstrap from 'bootstrap';


@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  users: any[] = [];
  products: any[] = [];
  salesOrders: any[] = [];
  purchaseOrders: any[] = [];
  selectedUser: any = {};
  isEdit: boolean = false;

  constructor(private adminService: AdminService, private router: Router) {}

  ngOnInit() {
    this.loadData();
  }

  ngAfterViewInit() {
    // Initialize Bootstrap Tabs manually
    const tabElements = document.querySelectorAll('.nav-tabs a');
    tabElements.forEach(tab => {
      tab.addEventListener('click', (event) => {
        event.preventDefault();
        const target = (event.target as HTMLElement).getAttribute('data-bs-target');
        if (target) {
          const tabInstance = new bootstrap.Tab(document.querySelector(target) as HTMLElement);
          tabInstance.show();
        }
      });
    });
  }

  loadData() {
    this.getUsers();
    this.getProducts();
    this.getSalesOrders();
    this.getPurchaseOrders();
  }

  getUsers() {
    this.adminService.getUsers().subscribe({
      next: (data) => this.users = data,
      error: (error) => console.error('Error fetching users:', error)
    });
  }

  getProducts() {
    this.adminService.getProducts().subscribe({
      next: (data) => this.products = data,
      error: (error) => console.error('Error fetching products:', error)
    });
  }

  getSalesOrders() {
    this.adminService.getSalesOrders().subscribe({
      next: (data) => this.salesOrders = data,
      error: (error) => console.error('Error fetching sales orders:', error)
    });
  }

  getPurchaseOrders() {
    this.adminService.getPurchaseOrders().subscribe({
      next: (data) => this.purchaseOrders = data,
      error: (error) => console.error('Error fetching purchase orders:', error)
    });
  }

  addUser() {
    this.isEdit = false;
    this.selectedUser = {};
  }

  editUser(user: any) {
    this.isEdit = true;
    this.selectedUser = { ...user };
  }

  saveUser() {
    const apiKey = localStorage.getItem('apiKey'); // Get API key from local storage
    if (!apiKey) {
      console.error('API Key is missing. Admin authentication required.');
      return;
    }

    if (this.isEdit) {
      this.adminService.updateUser(this.selectedUser.userId, this.selectedUser).subscribe({
        next: () => {
          this.getUsers();
          alert('User updated successfully!');
        },
        error: (error) => console.error('Error updating user:', error)
      });
    } else {
      const userPayload = {
        userId: this.selectedUser.userId,
        username: this.selectedUser.userName,
        userpassword: this.selectedUser.userPassword,
        role: this.selectedUser.role
      };

      this.adminService.addUser(userPayload).subscribe({
        next: () => {
          this.getUsers();
          alert('User added successfully!');
        },
        error: (error) => console.error('Error adding user:', error)
      });
    }
  }  

  deleteUser(userId: number) {
    this.adminService.deleteUser(userId).subscribe({
      next: () => this.getUsers(),
      error: (error) => console.error('Error deleting user:', error)
    });
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    this.router.navigate(['']);
  }
  
}
