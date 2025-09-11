import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PacjentWizytyComponent } from './pacjent-wizyty.component';

describe('PacjentWizytyComponent', () => {
  let component: PacjentWizytyComponent;
  let fixture: ComponentFixture<PacjentWizytyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PacjentWizytyComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PacjentWizytyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
