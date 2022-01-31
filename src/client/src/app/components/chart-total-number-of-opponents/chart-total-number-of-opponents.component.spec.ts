import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ChartTotalNumberOfOpponentsComponent } from './chart-total-number-of-opponents.component';

describe('ChartTotalNumberOfOpponentsComponent', () => {
  let component: ChartTotalNumberOfOpponentsComponent;
  let fixture: ComponentFixture<ChartTotalNumberOfOpponentsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChartTotalNumberOfOpponentsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChartTotalNumberOfOpponentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
