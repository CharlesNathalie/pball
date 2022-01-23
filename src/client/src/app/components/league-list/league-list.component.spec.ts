import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeagueListComponent } from 'src/app/components/league-list/league-list.component';

describe('LeagueListComponent', () => {
  let component: LeagueListComponent;
  let fixture: ComponentFixture<LeagueListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ LeagueListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeagueListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
