import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ChartAveragePlayerLevelOfPartnersComponent } from './chart-average-player-level-of-partners.component';

describe('ChartAveragePlayerLevelOfPartnersComponent', () => {
  let component: ChartAveragePlayerLevelOfPartnersComponent;
  let fixture: ComponentFixture<ChartAveragePlayerLevelOfPartnersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChartAveragePlayerLevelOfPartnersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChartAveragePlayerLevelOfPartnersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
