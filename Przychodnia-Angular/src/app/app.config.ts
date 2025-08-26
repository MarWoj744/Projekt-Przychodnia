import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

export const AppConfig = {
  apiUrl: 'https://localhost:7132/api' 
};


export const API = {
  lekarze: `${AppConfig.apiUrl}/Lekarz`,
  wizyty: `${AppConfig.apiUrl}/Wizyta`,
  badania: `${AppConfig.apiUrl}/Badanie`,
  harmonogram: `${AppConfig.apiUrl}/Harmonogram` 
};
export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
     provideHttpClient(withInterceptorsFromDi())
  ],
};
