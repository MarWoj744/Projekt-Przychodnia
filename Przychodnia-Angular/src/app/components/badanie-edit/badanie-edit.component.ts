import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Badanie } from '../../models/badanie.model';
import { BadanieService } from '../../services/badanie.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-badanie-edit',
  imports :[CommonModule, FormsModule],
  templateUrl: './badanie-edit.component.html',
  styleUrls: ['./badanie-edit.component.css']
})
export class BadanieEditComponent implements OnInit {
  
 @Input() badanie!: Badanie;
  @Output() save = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();

  constructor(private badanieService: BadanieService) {}
  ngOnInit(): void {
    
     if (this.badanie.id) {
    this.badanieService.getById(this.badanie.id).subscribe(
      data => {
        this.badanie = data;  
      },
      error => {
        console.error('Błąd podczas ładowania badania', error);
      }
    );
  }
  }



submitForm(): void {
  console.log('Dane do wysłania przed modyfikacją: ', this.badanie);

 
  const { wykonane, ...dataDoWyslania } = this.badanie;  

  console.log('Dane po usunięciu wykonanego: ', dataDoWyslania);

 
  if (this.badanie.id && this.badanie.id > 0) {
    this.badanieService.update(dataDoWyslania)  
      .pipe(
        catchError(error => {
          console.error('Błąd odpowiedzi z serwera:', error); 
          if (error.error && error.error.errors) {
            console.error('Błędy walidacji:', error.error.errors); 
          }
          return throwError(error); 
        })
      )
      .subscribe({
        next: () => {
          this.save.emit();  
        },
        error: err => {
          console.error('Wystąpił błąd podczas aktualizacji badania:', err);
        }
      });
  } else {
    
    this.badanieService.create(dataDoWyslania)  
      .pipe(
        catchError(error => {
          console.error('Błąd odpowiedzi z serwera:', error);
          if (error.error && error.error.errors) {
            console.error('Błędy walidacji:', error.error.errors); 
          }
          return throwError(error); 
        })
      )
      .subscribe({
        next: () => {
          this.save.emit();  
        },
        error: err => {
          console.error('Wystąpił błąd podczas tworzenia badania:', err);
        }
      });
  }
}}