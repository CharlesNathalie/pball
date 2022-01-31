import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ChartGamesPercentWonComponent } from './chart-games-percent-won.component';

describe('ChartGamesPercentWonComponent', () => {
  let component: ChartGamesPercentWonComponent;
  let fixture: ComponentFixture<ChartGamesPercentWonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChartGamesPercentWonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChartGamesPercentWonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
