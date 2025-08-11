import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BadanieEditComponent } from './badanie-edit.component';

describe('BadanieEditComponent', () => {
  let component: BadanieEditComponent;
  let fixture: ComponentFixture<BadanieEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BadanieEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BadanieEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
