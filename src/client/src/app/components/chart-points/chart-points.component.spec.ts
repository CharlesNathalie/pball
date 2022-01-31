import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ChartPointsComponent } from './chart-points.component';

describe('ChartPointsComponent', () => {
  let component: ChartPointsComponent;
  let fixture: ComponentFixture<ChartPointsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChartPointsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChartPointsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
