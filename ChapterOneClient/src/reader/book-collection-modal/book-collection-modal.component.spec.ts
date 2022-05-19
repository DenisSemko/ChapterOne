import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookCollectionModalComponent } from './book-collection-modal.component';

describe('BookCollectionModalComponent', () => {
  let component: BookCollectionModalComponent;
  let fixture: ComponentFixture<BookCollectionModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BookCollectionModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BookCollectionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
