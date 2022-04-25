import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Book } from 'src/models/book';
import { PaginatedResult } from 'src/models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  paginatedResult: PaginatedResult<Book[]> = new PaginatedResult<Book[]>();
  tokenHeader = new HttpHeaders({'Authorization' : 'Bearer ' + localStorage.getItem('accessToken')});
  
  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) { }

  getBooksList(page?: number, itemsPerPage?: number) {
    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get(environment.baseURI + 'BooksPagination', { observe: 'response', params }).pipe(
      map((response: any) => {
        this.paginatedResult.result = response.body;
        if (response.headers.get('Pagination') != null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }
        return this.paginatedResult;
      })
    );
  }

  getImagePath(relativePath: string) : Observable<Blob> {
    let result = relativePath.substring(16, relativePath.length);
    return this.http.get(environment.baseURI + 'BookImageFiles?fileName=' + result, {responseType: 'blob'});
  }

  getBookById(id: any) {
    return this.http.get(environment.baseURI + 'Books/' + id);
  }

}

