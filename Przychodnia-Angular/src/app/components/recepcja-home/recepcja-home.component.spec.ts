import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecepcjaHomeComponent } from './recepcja-home.component';

describe('RecepcjaHomeComponent', () => {
  let component: RecepcjaHomeComponent;
  let fixture: ComponentFixture<RecepcjaHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RecepcjaHomeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecepcjaHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
