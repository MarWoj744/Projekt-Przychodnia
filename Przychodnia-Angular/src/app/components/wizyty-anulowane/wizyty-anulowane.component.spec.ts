import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WizytyAnulowaneComponent } from './wizyty-anulowane.component';

describe('WizytyAnulowaneComponent', () => {
  let component: WizytyAnulowaneComponent;
  let fixture: ComponentFixture<WizytyAnulowaneComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WizytyAnulowaneComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WizytyAnulowaneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
