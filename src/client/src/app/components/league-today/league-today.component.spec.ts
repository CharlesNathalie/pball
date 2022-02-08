import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeagueTodayComponent } from 'src/app/components/league-today/league-today.component';

describe('LeagueTodayComponent', () => {
  let component: LeagueTodayComponent;
  let fixture: ComponentFixture<LeagueTodayComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ LeagueTodayComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeagueTodayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
