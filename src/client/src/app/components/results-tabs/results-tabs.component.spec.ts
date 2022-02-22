import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResultsTabsComponent } from 'src/app/components/results-tabs/results-tabs.component';

describe('ResultsTabsComponent', () => {
  let component: ResultsTabsComponent;
  let fixture: ComponentFixture<ResultsTabsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ ResultsTabsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResultsTabsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
