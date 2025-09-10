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
  constructor(private router: Router) {}

  ngOnInit(): void {
    const token = localStorage.getItem('jwtToken');
    const role = localStorage.getItem('userRole');

    if (token && role) {
      this.isLoggedIn = true;
      this.userRole = role;
    }
  }
  
logout() {
    localStorage.clear();
    this.isLoggedIn = false;
    this.userRole = null;
    this.router.navigate(['/home']);
  }
}
