import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PacjentWizytaAddComponent } from './pacjent-wizyta-add.component';

describe('PacjentWizytaAddComponent', () => {
  let component: PacjentWizytaAddComponent;
  let fixture: ComponentFixture<PacjentWizytaAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PacjentWizytaAddComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PacjentWizytaAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
