import { L } from '@angular/cdk/keycodes';
import { HttpClient } from '@angular/common/http';
import { ThisReceiver } from '@angular/compiler';
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Book } from 'src/models/book';
import { CartItem } from 'src/models/cart-item';
import { User } from 'src/models/user';
import { MockCartService } from 'src/services/mock-cart.service';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-card-modal',
  templateUrl: './card-modal.component.html',
  styleUrls: ['./card-modal.component.scss']
})
export class CardModalComponent implements OnInit {

  books: Book[];
  userDetails: any;
  bookPayments: [] = [];
  public cartItems : Observable<CartItem[]> = of([]);
  @Input() shoppingCartItems: CartItem[] = [];
  
  constructor(public mockCartService: MockCartService, public userService: UserService,
    public router: Router, private http: HttpClient) { }

  ngOnInit(): void {
    this.cartItems = this.mockCartService.getItems();
    this.cartItems.subscribe(shoppingCartItems => this.shoppingCartItems = shoppingCartItems);

    let token = localStorage.getItem('accessToken') as string;
    let tokenInfo = this.userService.getDecodedAccessToken(token);

    this.getCurrentUser(tokenInfo.id);
  }

  public removeItem(item: CartItem) {
    this.mockCartService.removeFromCart(item);
  }

  public getTotal(): number {
    let totalPrice = 0.0;
    this.shoppingCartItems.forEach(element => {
      var numberValue = Number(element.price);
      totalPrice += numberValue;
    });
    return totalPrice;
  }

  getCurrentUser(id: any) {
    this.userService.getUserById(id).subscribe(
      result => {
        this.userDetails = result as User;
      }, 
      error => {
        console.log(error);
      }
    )
  }

  onSubmit() {
    this.shoppingCartItems.forEach(element => {
      const body = { User: this.userDetails.id, Book: element.book.id, Type: element.type };
      this.mockCartService.addBookPayment(body).subscribe(
        result => {
          this.mockCartService.removeFromCart(element);
        },
        error => {
          console.log(error);
        }
      );
    });
    this.router.navigateByUrl("/reader/payment/book");
  }

}
