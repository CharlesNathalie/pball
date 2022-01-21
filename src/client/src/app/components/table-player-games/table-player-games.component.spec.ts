import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TablePlayerGamesComponent } from 'src/app/components/table-player-games/table-player-games.component';

describe('TablePlayerGamesComponent', () => {
  let component: TablePlayerGamesComponent;
  let fixture: ComponentFixture<TablePlayerGamesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ TablePlayerGamesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TablePlayerGamesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
