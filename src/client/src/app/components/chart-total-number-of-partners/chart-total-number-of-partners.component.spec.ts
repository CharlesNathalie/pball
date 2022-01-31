import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ChartTotalNumberOfPartnersComponent } from './chart-total-number-of-partners.component';

describe('ChartTotalNumberOfPartnersComponent', () => {
  let component: ChartTotalNumberOfPartnersComponent;
  let fixture: ComponentFixture<ChartTotalNumberOfPartnersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChartTotalNumberOfPartnersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChartTotalNumberOfPartnersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
