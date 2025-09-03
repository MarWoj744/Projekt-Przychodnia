import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecepcjaComponent } from './recepcja.component';

describe('RecepcjaComponent', () => {
  let component: RecepcjaComponent;
  let fixture: ComponentFixture<RecepcjaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RecepcjaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecepcjaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
