import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TablePlayerHeadToHeadComponent } from 'src/app/components/table-player-head-to-head/table-player-head-to-head.component';

describe('TablePlayerHeadToHeadComponent', () => {
  let component: TablePlayerHeadToHeadComponent;
  let fixture: ComponentFixture<TablePlayerHeadToHeadComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ TablePlayerHeadToHeadComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TablePlayerHeadToHeadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
