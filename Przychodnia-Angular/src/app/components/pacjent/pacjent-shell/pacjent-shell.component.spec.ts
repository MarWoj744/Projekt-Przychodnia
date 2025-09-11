import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PacjentShellComponent } from './pacjent-shell.component';

describe('PacjentShellComponent', () => {
  let component: PacjentShellComponent;
  let fixture: ComponentFixture<PacjentShellComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PacjentShellComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PacjentShellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
