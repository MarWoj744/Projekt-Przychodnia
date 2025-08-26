import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WizytyComponent } from './wizyty.component';

describe('WizytyComponent', () => {
  let component: WizytyComponent;
  let fixture: ComponentFixture<WizytyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WizytyComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WizytyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
