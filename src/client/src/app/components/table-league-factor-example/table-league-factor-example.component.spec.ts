import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TableLeagueFactorExampleComponent } from 'src/app/components/table-league-factor-example/table-league-factor-example.component';

describe('TableLeagueFactorExampleComponent', () => {
  let component: TableLeagueFactorExampleComponent;
  let fixture: ComponentFixture<TableLeagueFactorExampleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ TableLeagueFactorExampleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TableLeagueFactorExampleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
