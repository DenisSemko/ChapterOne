import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RateService {
  tokenHeader = new HttpHeaders({'Authorization' : 'Bearer ' + localStorage.getItem('accessToken')});
  
  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) { }

  public formModel = this.fb.group({
    Id : [''],
    Mark : [''],
    Comment : [''],
    Date : [''],
    Book : [''],
    User : ['']
  })

  getRatingsByBookId(id: any) {
    return this.http.get(environment.baseURI + 'Rate/' + id);
  }

  getAverageMarkByBookId(id: any) {
    return this.http.get(environment.baseURI + 'Rate/' + id + '/average-mark');
  }

  getNumberOfReviewsByBookId(id: any) {
    return this.http.get(environment.baseURI + 'Rate/' + id + '/number-reviews');
  }

  addRatingReview() {
    var body = {
      Id : this.formModel.value.Id,
      Mark : this.formModel.value.Mark,
      Comment : this.formModel.value.Comment,
      Date : this.formModel.value.Date,
      Book : this.formModel.value.Book,
      User : this.formModel.value.User
    };
    return this.http.post(environment.baseURI + 'Rate', body)
  }
}
