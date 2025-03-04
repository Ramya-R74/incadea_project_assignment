import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const token = localStorage.getItem('token');
    const role = localStorage.getItem('role');

    if (!token || !role) {
      this.router.navigate(['/login']); // Redirect if user is not logged in
      return false;
    }

    const allowedRole = route.data['role']; // Role from route definition

    if (allowedRole && role !== allowedRole) {
      this.router.navigate(['/login']); // Redirect if user does not have access
      return false;
    }

    return true; // Allow access
  }
}
