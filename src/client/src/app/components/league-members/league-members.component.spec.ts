import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeagueMembersComponent } from 'src/app/components/league-members/league-members.component';

describe('LeagueMembersComponent', () => {
  let component: LeagueMembersComponent;
  let fixture: ComponentFixture<LeagueMembersComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ LeagueMembersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeagueMembersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
