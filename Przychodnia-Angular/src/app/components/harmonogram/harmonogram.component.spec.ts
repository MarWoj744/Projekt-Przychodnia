import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HarmonogramComponent } from './harmonogram.component';

describe('HarmonogramComponent', () => {
  let component: HarmonogramComponent;
  let fixture: ComponentFixture<HarmonogramComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HarmonogramComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HarmonogramComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
