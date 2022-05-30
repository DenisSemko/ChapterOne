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

  public formModel = this.fb.group({
    Title : [''],
    Author : [''],
    Publisher : [''],
    PublishedDate : [''],
    Genre : [''],
    Language : [''],
    ISBN : [''],
    ShortDescription : [''],
    ReadingAge : ['']
  })

  public bookTypeModel = this.fb.group({
    Book : [''],
    Type : [''],
    Price : ['']
  })

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

  getBooksListFreeFilter(page?: number, itemsPerPage?: number, subscriptionId?: any) {
    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get(environment.baseURI + 'BooksPagination/' + subscriptionId + '/free-books' + '/', { observe: 'response', params }).pipe(
      map((response: any) => {
        this.paginatedResult.result = response.body;
        if (response.headers.get('Pagination') != null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }
        return this.paginatedResult;
      })
    );
  }

  getBooksListGenreFilter(page?: number, itemsPerPage?: number, name?: any) {
    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get(environment.baseURI + 'BooksPagination/' + name + '/genre-books' + '/', { observe: 'response', params }).pipe(
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

  getBookTypeById(id: any) {
    return this.http.get(environment.baseURI + 'BookType/' + id);
  }

  getTypes() {
    return this.http.get(environment.baseURI + 'BookType');
  }

  getWebFileByBookId(id: any) {
    return this.http.get(environment.baseURI + 'BookType/' + id + "/web-file");
  }

  getGenres() {
    return this.http.get(environment.baseURI + 'Genre');
  }

  addBook(){
    var body = {
      Title : this.formModel.value.Title,
      Author : this.formModel.value.Author,
      Publisher : this.formModel.value.Publisher,
      PublishedDate : this.formModel.value.PublishedDate,
      Genre : this.formModel.value.Genre,
      Language : this.formModel.value.Language,
      ISBN : this.formModel.value.ISBN,
      ShortDescription : this.formModel.value.ShortDescription,
      ReadingAge : this.formModel.value.ReadingAge
    };
    return this.http.post(environment.baseURI + 'Books', body)
  }

  addBookType(){
    var body = {
      Book : this.bookTypeModel.value.Book,
      Type : this.bookTypeModel.value.Type,
      Price : this.bookTypeModel.value.Price
    };
    return this.http.post(environment.baseURI + 'BookType', body)
  }

  uploadImagesFiles(bookId: string, file: File): Observable<any>  {
    const formData = new FormData();
    formData.append("profileImage", file);
    return this.http.post(environment.baseURI + 'BookImageFiles/' + bookId + '/upload-files', 
    formData, {
      responseType: 'text'
    });
  }

  sendFreeBook(userId: any, bookId: any) {
    return this.http.get(environment.baseURI + 'BookImageFiles/' + userId + "/" + bookId + "/send-free-book");
  }



}

