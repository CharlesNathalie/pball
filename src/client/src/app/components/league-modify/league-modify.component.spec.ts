import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeagueModifyComponent } from 'src/app/components/league-modify/league-modify.component';

describe('LeagueModifyComponent', () => {
  let component: LeagueModifyComponent;
  let fixture: ComponentFixture<LeagueModifyComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ LeagueModifyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeagueModifyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
