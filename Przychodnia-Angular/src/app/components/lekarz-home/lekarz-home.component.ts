import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
@Component({
  selector: 'app-lekarz-home',
 standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './lekarz-home.component.html',
  styleUrl: './lekarz-home.component.css'
})
export class LekarzHomeComponent {
  isLoggedIn = true;
  userRole: string | null = null;
  userId: number | null=null;
  userName: string | null = null;
  userEmail: string | null = null;

  constructor(private router: Router) {}

  ngOnInit(): void {
    const token = localStorage.getItem('jwtToken');
    const role = localStorage.getItem('userRole');
    const idStr = localStorage.getItem('userId'); // string | null
    const name = localStorage.getItem('userName');
    const email = localStorage.getItem('userEmail');

    // konwersja na number (albo null jeśli brak/NaN)
    const id = idStr !== null && idStr !== '' ? Number(idStr) : null;

    if (token && role && id !== null && !Number.isNaN(id)) {
      this.isLoggedIn = true;
      this.userRole = role;
      this.userId = id;
      this.userName = name;
      this.userEmail = email;
    }
  }

  logout(): void {
    localStorage.clear();
    this.isLoggedIn = false;
    this.userRole = null;
    this.userId = null;
    this.userName = null;
    this.userEmail = null;
    this.router.navigate(['/home']);
  }
}