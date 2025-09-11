import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PacjentDaneComponent } from './pacjent-dane.component';

describe('PacjentDaneComponent', () => {
  let component: PacjentDaneComponent;
  let fixture: ComponentFixture<PacjentDaneComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PacjentDaneComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PacjentDaneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
