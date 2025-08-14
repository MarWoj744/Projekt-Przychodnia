import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from "@angular/router";
import { LekarzService } from '../../services/lekarz.service';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-lekarz',
  templateUrl: './lekarz.component.html',
  styleUrls: ['./lekarz.component.css'],
  standalone: true,
  imports: [RouterModule, CommonModule],
})
export class LekarzComponent {
  logout() {
  
  console.log('Wylogowano (symulacja)');
}

}
