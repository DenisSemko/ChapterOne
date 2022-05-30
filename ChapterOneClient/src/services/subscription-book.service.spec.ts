import { TestBed } from '@angular/core/testing';

import { SubscriptionBookService } from './subscription-book.service';

describe('SubscriptionBookService', () => {
  let service: SubscriptionBookService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SubscriptionBookService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
