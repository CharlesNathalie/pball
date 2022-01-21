import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TableLeagueStatsComponent } from 'src/app/components/table-league-stats/table-league-stats.component';

describe('TableLeagueStatsComponent', () => {
  let component: TableLeagueStatsComponent;
  let fixture: ComponentFixture<TableLeagueStatsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ TableLeagueStatsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TableLeagueStatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
