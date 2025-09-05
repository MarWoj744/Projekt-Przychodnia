import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


export interface RegisterDTO {
  imie: string;
  nazwisko: string;
  pesel: string;
  adres: string;
  telefon: string;
  email: string;
  login: string;
  haslo: string;
  potwierdzHaslo: string;
  rola: string; 
}

export interface LoginDto {
  login:string;
  haslo:string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5120/api/auth'; 

  constructor(private http: HttpClient) {}

  register(userData: RegisterDTO): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, userData);
  }

  login(userData: LoginDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, userData);
  }
}
