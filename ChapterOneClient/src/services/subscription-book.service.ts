import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionBookService {

  tokenHeader = new HttpHeaders({'Authorization' : 'Bearer ' + localStorage.getItem('accessToken')});

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) { }

  public formModel = this.fb.group({
    Book : [''],
    Subscription : ['']
  })

  addSubscriptionBook(){
    var body = {
      Book : this.formModel.value.Book,
      Subscription : this.formModel.value.Subscription
    };
    return this.http.post(environment.baseURI + 'SubscriptionBook', body)
  }

  getBooksBySubscription(subscribtionId: any) {
    return this.http.get(environment.baseURI + 'SubscriptionBook/' + subscribtionId);
  }

  findBookInFreeBooks(userId: any, bookId: any) {
    return this.http.get(environment.baseURI + 'SubscriptionBook/' + userId + '/' + bookId);
  }
}
