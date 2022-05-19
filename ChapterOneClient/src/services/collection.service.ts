import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CollectionService {

  tokenHeader = new HttpHeaders({'Authorization' : 'Bearer ' + localStorage.getItem('accessToken')});

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) { }

  public formModel = this.fb.group({
    Id : [''],
    Name : [''],
    User : [''],
    Category : ['']
  })

  addCollection(){
    var body = {
      Id : this.formModel.value.Id,
      Name : this.formModel.value.Name,
      User : this.formModel.value.User,
      Category : this.formModel.value.Category
    };
    return this.http.post(environment.baseURI + 'Collections', body)
  }

  getByUserId(userId: any) {
    return this.http.get(environment.baseURI + 'Collections/' + userId);
  }

  deleteById(id: any) {
    return this.http.delete(environment.baseURI + 'Collections/' + id);
  }
}
