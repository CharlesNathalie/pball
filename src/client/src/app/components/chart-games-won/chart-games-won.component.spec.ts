import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ChartGamesWonComponent } from './chart-games-wons.component';

describe('ChartGamesWonComponent', () => {
  let component: ChartGamesWonComponent;
  let fixture: ComponentFixture<ChartGamesWonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChartGamesWonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChartGamesWonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
