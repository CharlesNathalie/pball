import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NoDataForUserComponent } from 'src/app/components/no-data-for-user/no-data-for-user.component';

describe('NoDataForUserComponent', () => {
  let component: NoDataForUserComponent;
  let fixture: ComponentFixture<NoDataForUserComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ NoDataForUserComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NoDataForUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
