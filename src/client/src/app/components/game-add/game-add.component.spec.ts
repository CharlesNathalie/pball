import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameAddComponent } from 'src/app/components/game-add/game-add.component';

describe('GameAddComponent', () => {
  let component: GameAddComponent;
  let fixture: ComponentFixture<GameAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ GameAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GameAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
