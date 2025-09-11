import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-pacjent-home',
  imports: [CommonModule],
  templateUrl: './pacjent-home.component.html',
  styleUrl: './pacjent-home.component.css'
})
export class PacjentHomeComponent {

    isLoggedIn = true;
  userRole: string | null = null;
  userId: string | null = null;
  constructor(private router: Router) {}

  ngOnInit(): void {
    const token = localStorage.getItem('jwtToken');
    const role = localStorage.getItem('userRole');
    const id= localStorage.getItem('userId');
    if (token && role) {
      this.isLoggedIn = true;
      this.userRole = role;
      this.userId=id
    }
  }
  
logout() {
    localStorage.clear();
    this.isLoggedIn = false;
    this.userRole = null;
    this.userId=null;
    this.router.navigate(['/home']);
  }
}
