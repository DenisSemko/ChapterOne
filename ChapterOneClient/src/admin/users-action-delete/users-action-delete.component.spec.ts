import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersActionDeleteComponent } from './users-action-delete.component';

describe('UsersActionDeleteComponent', () => {
  let component: UsersActionDeleteComponent;
  let fixture: ComponentFixture<UsersActionDeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UsersActionDeleteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersActionDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
