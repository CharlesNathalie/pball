import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TableMostGamesPlayedComponent } from 'src/app/components/table-most-games-played/table-most-games-played.component';

describe('TableMostGamesPlayedComponent', () => {
  let component: TableMostGamesPlayedComponent;
  let fixture: ComponentFixture<TableMostGamesPlayedComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ TableMostGamesPlayedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TableMostGamesPlayedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
