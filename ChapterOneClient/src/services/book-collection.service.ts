import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BookCollectionService {

  tokenHeader = new HttpHeaders({'Authorization' : 'Bearer ' + localStorage.getItem('accessToken')});

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) { }

  public formModel = this.fb.group({
    Id : [''],
    Book : [''],
    Collection : ['']
  })

  addBookCollection(){
    var body = {
      Id : this.formModel.value.Id,
      Book : this.formModel.value.Book,
      Collection : this.formModel.value.Collection
    };
    return this.http.post(environment.baseURI + 'BookCollection', body)
  }

  getBooksByCollection(collectionId: any) {
    return this.http.get(environment.baseURI + 'BookCollection/' + collectionId);
  }


}
