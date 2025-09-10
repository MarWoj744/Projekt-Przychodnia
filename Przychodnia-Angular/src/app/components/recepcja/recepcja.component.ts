import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-recepcja',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './recepcja.component.html',
  styleUrl: './recepcja.component.css'
})
export class RecepcjaComponent {
  constructor(private router: Router) {}
  
  logout() {
    localStorage.clear();
    this.router.navigate(['/home']);

    console.log('Wylogowano (symulacja) - recepcja');
  }
}
