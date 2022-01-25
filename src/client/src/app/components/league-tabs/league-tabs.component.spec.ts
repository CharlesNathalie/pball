import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeagueTabsComponent } from 'src/app/components/league-tabs/league-tabs.component';

describe('LeagueTabsComponent', () => {
  let component: LeagueTabsComponent;
  let fixture: ComponentFixture<LeagueTabsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ LeagueTabsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeagueTabsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
