import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  isLoggedIn = false;
  userRole: string | null = null;
  constructor(private router: Router) {}

  ngOnInit(): void {
    const token = localStorage.getItem('jwtToken');
    const role = localStorage.getItem('userRole');

    if (token && role) {
      this.isLoggedIn = false;
      this.userRole = role;
    }
  }

  goToRegister() {
    this.router.navigate(['/register']);
  }

  goToLogin() {
    this.router.navigate(['/login']);
  }

  goToLekarz() {
  this.router.navigate(['/lekarz']);
  }

  goToRecepcja() {
  this.router.navigate(['/recepcja']);
  }
  goToPacjent(){
    this.router.navigate(['/pacjent']);
  }

  logout() {
    localStorage.clear();
    this.isLoggedIn = false;
    this.userRole = null;
    this.router.navigate(['/home']);
  }
}
