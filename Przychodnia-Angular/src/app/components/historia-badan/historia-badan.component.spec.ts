import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HistoriaBadanComponent } from './historia-badan.component';

describe('HistoriaBadanComponent', () => {
  let component: HistoriaBadanComponent;
  let fixture: ComponentFixture<HistoriaBadanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HistoriaBadanComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HistoriaBadanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
