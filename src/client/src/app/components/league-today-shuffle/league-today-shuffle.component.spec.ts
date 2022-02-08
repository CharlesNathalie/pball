import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeagueTodayShuffleComponent } from 'src/app/components/league-today-shuffle/league-today-shuffle.component';

describe('LeagueTodayShuffleComponent', () => {
  let component: LeagueTodayShuffleComponent;
  let fixture: ComponentFixture<LeagueTodayShuffleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ LeagueTodayShuffleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeagueTodayShuffleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
