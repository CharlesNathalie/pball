import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ChartGamesPlayedComponent } from './chart-games-played.component';

describe('ChartGamesPlayedComponent', () => {
  let component: ChartGamesPlayedComponent;
  let fixture: ComponentFixture<ChartGamesPlayedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChartGamesPlayedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChartGamesPlayedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
