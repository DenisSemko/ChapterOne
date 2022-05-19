import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { PaginatedResult } from 'src/models/pagination';
import { Book } from 'src/models/book';

@Injectable({
  providedIn: 'root'
})
export class CombinationService {

  tokenHeader = new HttpHeaders({'Authorization' : 'Bearer ' + localStorage.getItem('accessToken')});

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) { }

  public formModel = this.fb.group({
    Id : [''],
    Reader : [''],
    Author : [''],
    Year : [''],
    Genre : [''],
    Publisher : [''],
    ShortDescription : [''],
    TempCombination : [''],
    ResultPercentage: 0,
    IsSuccessful: false
  })

  public formUpdateModel = this.fb.group({
    Id : [''],
    Reader : [''],
    Author : [''],
    Year : [''],
    Genre : [''],
    Publisher : [''],
    ShortDescription : [''],
    TempCombination : [''],
    ResultPercentage: 0,
    IsSuccessful: false
  })

  generateCombination() {
    return this.http.get(environment.baseURI + 'Combination');
  }

  generateCombinationFromCollections(userId: any) {
    return this.http.get(environment.baseURI + 'Combination/' + userId + '/from-collection');
  }

  addCombination() {
    var body = {
      Id : this.formModel.value.Id,
      Reader : this.formModel.value.Reader,
      Author : this.formModel.value.Author,
      Year : this.formModel.value.Year,
      Genre : this.formModel.value.Genre,
      Publisher : this.formModel.value.Publisher,
      ShortDescription : this.formModel.value.ShortDescription,
      TempCombination : this.formModel.value.TempCombination,
      ResultPercentage : this.formModel.value.ResultPercentage,
      IsSuccessful : this.formModel.value.IsSuccessful
    };
    return this.http.post(environment.baseURI + 'Combination', body)
  }

  getCombinationByUser(id: any) {
    return this.http.get(environment.baseURI + 'Combination/' + id + "/by-user")
  }

  getOldSchemes(id: any) {
    return this.http.get(environment.baseURI + 'Combination/' + id + '/load-old');
  }

  findBooks(author: any, year: any, genre: any, publisher: any, description: any, page?: any, itemsPerPage?: any) {
    let params = new HttpParams();

    params = params.append('author', author);
    params = params.append('year', year);
    params = params.append('genre', genre);
    params = params.append('publisher', publisher);
    params = params.append('shortDescription', description);

    return this.http.get(environment.baseURI + 'BooksSearching', { observe: 'response', params });
  }

  updateCombination(body: any) {
    body = {
      Id : this.formUpdateModel.value.Id,
      Reader : this.formUpdateModel.value.Reader,
      Author : this.formUpdateModel.value.Author,
      Year : this.formUpdateModel.value.Year,
      Genre : this.formUpdateModel.value.Genre,
      Publisher : this.formUpdateModel.value.Publisher,
      ShortDescription : this.formUpdateModel.value.ShortDescription,
      TempCombination : this.formUpdateModel.value.TempCombination,
      ResultPercentage : this.formUpdateModel.value.ResultPercentage,
      IsSuccessful : this.formUpdateModel.value.IsSuccessful
    };
    return this.http.put(environment.baseURI + 'Combination', body)
  }

}
