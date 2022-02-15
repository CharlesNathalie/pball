import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersRequestingTempCodeComponent } from 'src/app/components/users-requesting-temp-code/users-requesting-temp-code.component';

describe('UsersRequestingTempCodeComponent', () => {
  let component: UsersRequestingTempCodeComponent;
  let fixture: ComponentFixture<UsersRequestingTempCodeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ UsersRequestingTempCodeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersRequestingTempCodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
