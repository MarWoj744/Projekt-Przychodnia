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
  constructor(private router: Router) {}

  ngOnInit(): void {
    const token = localStorage.getItem('jwtToken');
    const role = localStorage.getItem('userRole');
    const idStr = localStorage.getItem('userId'); // string | null

    // konwersja na number (albo null jeśli brak/NaN)
    const id = idStr !== null && idStr !== '' ? Number(idStr) : null;

    if (token && role && id !== null && !Number.isNaN(id)) {
      this.isLoggedIn = true;
      this.userRole = role;
      this.userId = id; // <-- poprawnie przypisujemy do userId
    } else {
      this.isLoggedIn = false;
      this.userRole = null;
      this.userId = null;
    }
  }

  logout(): void {
    localStorage.clear();
    this.isLoggedIn = false;
    this.userRole = null;
    this.userId = null; // <-- też userId, nie id
    this.router.navigate(['/home']);
  }
}