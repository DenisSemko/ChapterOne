import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  tokenHeader = new HttpHeaders({'Authorization' : 'Bearer ' + localStorage.getItem('accessToken')});

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) { }

  getCategoryByUser(id: any) {
    return this.http.get(environment.baseURI + 'Category/' + id);
  }

  addCategory(body: any) {
    return this.http.post(environment.baseURI + 'Category', body);
  }

  deleteCategory(id: any) {
    return this.http.delete(environment.baseURI + 'Category/' + id, {headers: this.tokenHeader});
  }
 

}
