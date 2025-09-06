import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PacjentHomeComponent } from './pacjent-home.component';

describe('PacjentHomeComponent', () => {
  let component: PacjentHomeComponent;
  let fixture: ComponentFixture<PacjentHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PacjentHomeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PacjentHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
