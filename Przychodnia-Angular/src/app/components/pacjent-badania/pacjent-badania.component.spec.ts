import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PacjentBadaniaComponent } from './pacjent-badania.component';

describe('PacjentBadaniaComponent', () => {
  let component: PacjentBadaniaComponent;
  let fixture: ComponentFixture<PacjentBadaniaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PacjentBadaniaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PacjentBadaniaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
