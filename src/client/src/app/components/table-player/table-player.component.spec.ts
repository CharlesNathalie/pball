import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TablePlayerComponent } from 'src/app/components/table/table-player/table-player.component';

describe('TablePlayerComponent', () => {
  let component: TablePlayerComponent;
  let fixture: ComponentFixture<TablePlayerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ TablePlayerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TablePlayerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
