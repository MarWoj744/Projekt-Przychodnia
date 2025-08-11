import { Routes } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { BadanieEditComponent } from './components/badanie-edit/badanie-edit.component';
import { HarmonogramComponent } from './components/harmonogram/harmonogram.component';
import { WizytyAnulowaneComponent } from './components/wizyty-anulowane/wizyty-anulowane.component';
import { LekarzComponent } from './components/lekarz/lekarz.component';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'lekarz', component: LekarzComponent },
  { path: '**', redirectTo: '/home' },
  { path: 'badanie-edytuj/:id', component: BadanieEditComponent },
  { path: 'harmonogram', component: HarmonogramComponent },
  { path: 'wizyty-anulowane', component: WizytyAnulowaneComponent },
  { path: '', redirectTo: '/harmonogram', pathMatch: 'full' },
  { path: '**', redirectTo: '/harmonogram' }
];