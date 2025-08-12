import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BadaniaComponent } from './badania.component';

describe('BadaniaComponent', () => {
  let component: BadaniaComponent;
  let fixture: ComponentFixture<BadaniaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BadaniaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BadaniaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
