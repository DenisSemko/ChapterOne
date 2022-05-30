import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { Observable, BehaviorSubject, Subscriber } from 'rxjs';
import { CartItem } from 'src/models/cart-item';
import { Book } from 'src/models/book';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs/operators';
import { BookService } from './book.service';


let books = JSON.parse(localStorage.getItem("cart")) || [];

@Injectable({
  providedIn: 'root'
})
export class MockCartService {

  public cartItems  :  BehaviorSubject<CartItem[]> = new BehaviorSubject([]);
  public webPrice: any;
  public audioPrice: any;
  public bookPrice: number;
  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router,
    private toastr: ToastrService, private bookService: BookService) { 
    this.cartItems.subscribe(
      books => books = books
    );
  }

  addBookPayment(body: any) {
    return this.http.post(environment.baseURI + 'BookPayment', body)
  }

     public addToCart(book: Book, type: string, price: any) {
       var item: CartItem | boolean = false;
       let hasItem = books.find((items, index) => {
         if(items.book.id == book.id) {
            if(items.type == type) {
              this.toastr.error("This item is already in the cart!")
            } else {
              item = { book: book, type: type, price: price};
              books.push(item);   
              this.toastr.success("You added a book to the cart!", "Success");
              localStorage.setItem("cart", JSON.stringify(books));
            }
           return true;
         }
         return false;
       });
  
       if(!hasItem) {
        item = { book: book, type: type, price: price};
        books.push(item);        
        this.toastr.success("You added a book to the cart!", "Success");
      }
  
       localStorage.setItem("cart", JSON.stringify(books));
       return item;
    }

  public removeFromCart(item: CartItem) {
    if (item === undefined) return false;
      const index = books.indexOf(item);
      books.splice(index, 1);
      localStorage.setItem("cart", JSON.stringify(books));
      return true;
  }

  public getItems(): Observable<CartItem[]> {
    const itemsStream = new Observable(observer => {
      observer.next(books);
      observer.complete();
    });
    return <Observable<CartItem[]>>itemsStream;
  }

  sendBookPaymentSuccess(userId: string) {
    return this.http.get(environment.baseURI + 'BookPayment/' + userId + "/send-book");
  }

  getBookPayment(userId: any) {
    return this.http.get(environment.baseURI + 'BookPayment/' + userId);
  }

  
}
