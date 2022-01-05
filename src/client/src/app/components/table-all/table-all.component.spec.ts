import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TableAllComponent } from 'src/app/components/table-all/table-all.component';

describe('TableAllComponent', () => {
  let component: TableAllComponent;
  let fixture: ComponentFixture<TableAllComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ TableAllComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TableAllComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
