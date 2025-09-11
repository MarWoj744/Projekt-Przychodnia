import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-recepcja-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './recepcja-home.component.html',
  styleUrl: './recepcja-home.component.css'
})
export class RecepcjaHomeComponent {
  isLoggedIn = true;
  userRole: string | null = null;
  userId: number | null = null;
  userName: string | null = null;
  userEmail: string | null = null;

  constructor(private router: Router) {}

  ngOnInit(): void {
    const token = localStorage.getItem('jwtToken');
    const role = localStorage.getItem('userRole');
    const idStr = localStorage.getItem('userId');
    const name = localStorage.getItem('userName');
    const email = localStorage.getItem('userEmail');

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
