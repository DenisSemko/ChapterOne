import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarReaderComponent } from './sidebar-reader.component';

describe('SidebarReaderComponent', () => {
  let component: SidebarReaderComponent;
  let fixture: ComponentFixture<SidebarReaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SidebarReaderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SidebarReaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
