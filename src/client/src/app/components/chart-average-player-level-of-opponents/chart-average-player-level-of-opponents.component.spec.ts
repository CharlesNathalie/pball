import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ChartAveragePlayerLevelOfOpponentsComponent } from './chart-average-player-level-of-opponents.component';

describe('ChartAveragePlayerLevelOfOpponentsComponent', () => {
  let component: ChartAveragePlayerLevelOfOpponentsComponent;
  let fixture: ComponentFixture<ChartAveragePlayerLevelOfOpponentsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChartAveragePlayerLevelOfOpponentsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChartAveragePlayerLevelOfOpponentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
