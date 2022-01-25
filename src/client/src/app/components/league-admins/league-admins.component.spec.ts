import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeagueAdminsComponent } from 'src/app/components/league-admins/league-admins.component';

describe('LeagueAdminsComponent', () => {
  let component: LeagueAdminsComponent;
  let fixture: ComponentFixture<LeagueAdminsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ LeagueAdminsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeagueAdminsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
