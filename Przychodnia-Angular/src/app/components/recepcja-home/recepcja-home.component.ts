import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-recepcja-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './recepcja-home.component.html',
  styleUrl: './recepcja-home.component.css'
})
export class RecepcjaHomeComponent {

}
