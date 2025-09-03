import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-recepcja',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './recepcja.component.html',
  styleUrl: './recepcja.component.css'
})
export class RecepcjaComponent {
  logout() {
    console.log('Wylogowano (symulacja) - recepcja');
  }
}
