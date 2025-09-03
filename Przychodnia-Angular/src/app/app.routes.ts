import { Routes } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { BadanieEditComponent } from './components/badanie-edit/badanie-edit.component';
import { HarmonogramComponent } from './components/harmonogram/harmonogram.component';
import { WizytyAnulowaneComponent } from './components/wizyty-anulowane/wizyty-anulowane.component';
import { LekarzComponent } from './components/lekarz/lekarz.component';
import { WizytyComponent } from './components/wizyty/wizyty.component';
import { BadaniaComponent } from './components/badania/badania.component';
import { LekarzHomeComponent } from './components/lekarz-home/lekarz-home.component';
import { HistoriaBadanComponent } from './components/historia-badan/historia-badan.component';
import { LoginComponent } from './login/login.component';
import { WizytaAddComponent } from './components/recepcja/wizyta-add.component';
import { RecepcjaComponent } from './components/recepcja/recepcja.component';
import { RecepcjaHomeComponent } from './components/recepcja-home/recepcja-home.component';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },

 {
  path: 'recepcja',
  component: RecepcjaComponent,
  children: [
    { path: '', redirectTo: 'strona-glowna', pathMatch: 'full' },
      { path: 'strona-glowna', component: RecepcjaHomeComponent },
      { path: 'wizyty', component: WizytyComponent },
      { path: 'wizyty/dodaj', component: WizytaAddComponent },
      { path: 'wizyty-anulowane', component: WizytyAnulowaneComponent },
  ]
},

 {
  path: 'lekarz',
  component: LekarzComponent,
  children: [
     { path: '', redirectTo: 'strona-glowna', pathMatch: 'full' },
     { path: 'strona-glowna', component: LekarzHomeComponent },
    { path: 'harmonogram', component: HarmonogramComponent },
    { path: 'wizyty', component: WizytyComponent },
    { path: 'wizyty-anulowane', component: WizytyAnulowaneComponent },
    { path: 'badania', component: BadaniaComponent },
    { path: 'historia-badan', component: HistoriaBadanComponent }
  ]
},
{ path: '**', redirectTo: '/home' }

];
