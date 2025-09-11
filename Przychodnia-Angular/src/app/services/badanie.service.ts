import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Badanie } from '../models/badanie.model';

@Injectable({
  providedIn: 'root'
})
export class BadanieService {
  private apiUrl = 'https://przychodniaap-ghhbcfgzgtbjgdgt.polandcentral-01.azurewebsites.net/api/Badanie'; 

  constructor(private http: HttpClient) {}
 getAll(): Observable<Badanie[]> {
    return this.http.get<Badanie[]>(this.apiUrl);
  }
  getById(id: number): Observable<Badanie> {
    return this.http.get<Badanie>(`${this.apiUrl}/${id}`);
  }

  update(badanie: Badanie): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${badanie.id}`, badanie);
  }
  
  create(badanie: Badanie): Observable<Badanie> {
    return this.http.post<Badanie>(this.apiUrl, badanie);
  }
   delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
