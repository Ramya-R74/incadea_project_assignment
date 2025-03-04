import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  role: string = '';
  apiKey: string = '';
  isAdmin = false;
  errorMessage: string = '';
  successMessage: string = '';

  constructor(private loginService: LoginService, private router: Router) { }

  onLogin(event: Event) {
    event.preventDefault();

    if (!this.username || !this.password || !this.role) {
      this.errorMessage = 'All fields are required!';
      this.successMessage = '';
      return;
    }

    const loginData: any = { username: this.username, password: this.password };
    if (this.role === 'Admin' && !this.apiKey) {
      this.errorMessage = 'API Key is required for Admins!';
      this.successMessage = '';
      return;
    }
    if (this.apiKey) {
      loginData.apiKey = this.apiKey;
    }

    this.loginService.login(this.username, this.password, this.apiKey).subscribe({
      next: (response: any) => {
        localStorage.setItem('token', response.token);
        localStorage.setItem('role', this.role);
        this.successMessage = 'Login successful! Redirecting...';
        this.errorMessage = '';

        setTimeout(() => {
          if (this.role === 'Admin') {
            this.router.navigate(['admin']);
          } else if (this.role === 'Customer') {
            this.router.navigate(['customer']);
          } else if (this.role === 'Sales') {
            this.router.navigate(['sales']);
          } else if (this.role === 'Purchase') {
            this.router.navigate(['purchase']);
          } else {
            this.router.navigate(['']);
          }
        }, 1000);
      },
      error: () => {
        this.errorMessage = 'Invalid credentials!';
        this.successMessage = '';
      }
    });
  }
}
