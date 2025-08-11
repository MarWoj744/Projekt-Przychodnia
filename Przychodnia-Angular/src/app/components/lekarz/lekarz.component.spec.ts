import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LekarzComponent } from './lekarz.component';

describe('LekarzComponent', () => {
  let component: LekarzComponent;
  let fixture: ComponentFixture<LekarzComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LekarzComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LekarzComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
