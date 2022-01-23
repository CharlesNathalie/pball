import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeagueNotMemberComponent } from 'src/app/components/league-not-member/league-not-member.component';

describe('LeagueNotMemberComponent', () => {
  let component: LeagueNotMemberComponent;
  let fixture: ComponentFixture<LeagueNotMemberComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ LeagueNotMemberComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeagueNotMemberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
