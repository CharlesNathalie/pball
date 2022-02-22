import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TablePartnerWinsComponent } from 'src/app/components/table-partner-wins/table-partner-wins.component';

describe('TablePartnerWinsComponent', () => {
  let component: TablePartnerWinsComponent;
  let fixture: ComponentFixture<TablePartnerWinsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ TablePartnerWinsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TablePartnerWinsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
