import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Badanie } from '../../models/badanie.model';
import { BadanieService } from '../../services/badanie.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

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
    throw new Error('Method not implemented.');
  }

  submitForm(): void {
    if (this.badanie.id) {
      this.badanieService.update(this.badanie).subscribe(() => this.save.emit());
    } else {
      this.badanieService.create(this.badanie).subscribe(() => this.save.emit());
    }
  }
}
