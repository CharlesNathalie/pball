import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgressTabsComponent } from 'src/app/components/progress-tabs/progress-tabs.component';

describe('ProgressTabsComponent', () => {
  let component: ProgressTabsComponent;
  let fixture: ComponentFixture<ProgressTabsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ ProgressTabsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgressTabsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
