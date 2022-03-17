import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileReaderComponent } from './profile-reader.component';

describe('ProfileReaderComponent', () => {
  let component: ProfileReaderComponent;
  let fixture: ComponentFixture<ProfileReaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileReaderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileReaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
