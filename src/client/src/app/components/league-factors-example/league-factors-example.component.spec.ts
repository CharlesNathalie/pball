import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeagueFactorsExampleComponent } from 'src/app/components/league-factors-example/league-factors-example.component';

describe('LeagueFactorsExampleComponent', () => {
  let component: LeagueFactorsExampleComponent;
  let fixture: ComponentFixture<LeagueFactorsExampleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ LeagueFactorsExampleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeagueFactorsExampleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
