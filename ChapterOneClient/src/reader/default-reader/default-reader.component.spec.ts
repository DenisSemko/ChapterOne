import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefaultReaderComponent } from './default-reader.component';

describe('DefaultReaderComponent', () => {
  let component: DefaultReaderComponent;
  let fixture: ComponentFixture<DefaultReaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DefaultReaderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DefaultReaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
