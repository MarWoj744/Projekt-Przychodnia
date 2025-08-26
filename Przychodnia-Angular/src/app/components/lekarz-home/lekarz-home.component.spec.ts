import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LekarzHomeComponent } from './lekarz-home.component';

describe('LekarzHomeComponent', () => {
  let component: LekarzHomeComponent;
  let fixture: ComponentFixture<LekarzHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LekarzHomeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LekarzHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
