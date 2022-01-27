import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StateJsonComponent } from 'src/app/components/state-json/state-json.component';

describe('StateJsonComponent', () => {
  let component: StateJsonComponent;
  let fixture: ComponentFixture<StateJsonComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ StateJsonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StateJsonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
