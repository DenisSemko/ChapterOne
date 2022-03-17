import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersActionAddAdminComponent } from './users-action-add-admin.component';

describe('UsersActionAddAdminComponent', () => {
  let component: UsersActionAddAdminComponent;
  let fixture: ComponentFixture<UsersActionAddAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UsersActionAddAdminComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersActionAddAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
