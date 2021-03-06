import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/models/user';
import { BookService } from 'src/services/book.service';
import { RateService } from 'src/services/rate.service';
import { UserService } from 'src/services/user.service';
import { UUID } from 'angular2-uuid';
import { SubscriptionBookService } from 'src/services/subscription-book.service';
import { MockCartService } from 'src/services/mock-cart.service';
import { Book } from 'src/models/book';
import { Observable, of } from 'rxjs';
import { CartItem } from 'src/models/cart-item';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.scss']
})
export class BookDetailsComponent implements OnInit {

  bookId: any;
  bookDetails: any;
  imageToShow: any;
  currentRating: any;
  numberOfReviews: any;
  reviews: any;
  userDetails: any;
  uuidValue: string = '';
  currentDate = new Date();
  webBookPrice: any;
  audioBookPrice: any;
  paperBookPrice: any;
  showHideFreeCard: boolean = false;
  showHideWebCard: boolean = false;
  showHideAudioCard: boolean = false;
  showHidePaperCard: boolean = false;

  public cartItems : Observable<CartItem[]> = of([]);
  public shoppingCartItems  : CartItem[] = [];


  constructor(public bookService: BookService, public router: Router, 
    public rateService: RateService, public userService: UserService,
    private toastr: ToastrService, public subscriptionBookService: SubscriptionBookService,
    public mockCartService: MockCartService) { }

  ngOnInit(): void {
    this.bookId = this.router.url.substring(13, this.router.url.length);
    let token = localStorage.getItem('accessToken') as string;
    let tokenInfo = this.userService.getDecodedAccessToken(token);

    this.getCurrentUser(tokenInfo.id);
    this.getBookById(this.bookId);
    this.getBookRating(this.bookId);
    this.getNumberOfReviews(this.bookId);
    this.getReviews(this.bookId);
    this.getBookPrices(this.bookId);
    this.findBookInFreeList(tokenInfo.id, this.bookId);

    this.cartItems = this.mockCartService.getItems();
    this.cartItems.subscribe(shoppingCartItems => this.shoppingCartItems = shoppingCartItems);

  }

  getBookById(id: any) {
    this.bookService.getBookById(id).subscribe(
      result => {
        this.bookDetails = result;
        this.setBookImage(this.bookDetails.bookImage);
      },
      error => {
        console.log(error);
      }
    )
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

  public setBookImage(imageUrl: any) {
    this.bookService.getImagePath(imageUrl).subscribe(data => {
      this.createBookImageFromBlob(data);
    }, error => {
      console.log(error);
    });
  }

  private createBookImageFromBlob(image: Blob) {
    let reader = new FileReader();
    reader.addEventListener("load", () => {
        this.imageToShow = reader.result;
    }, false);

    if (image) {
        reader.readAsDataURL(image);
    }
  }

  public getBookRating(id: any) {
    this.rateService.getAverageMarkByBookId(id).subscribe(
      result => {
        this.currentRating = result;
      },
      error => {
        console.log(error);
      }
    )
  }

  public getNumberOfReviews(id: any) {
    this.rateService.getNumberOfReviewsByBookId(id).subscribe(
      result => {
        this.numberOfReviews = result;
      },
      error => {
        console.log(error);
      }
    )
  }

  public getReviews(id: any) {
    this.rateService.getRatingsByBookId(id).subscribe(
      result => {
        this.reviews = result;
      },
      error => {
        console.log(error);
      }
    )
  }

  onSubmit() {
    this.rateService.addRatingReview().subscribe(
      (result:any) => {
        this.rateService.formModel.reset();
        this.toastr.success('New review has been successfully added!');
        window.location.reload();
      },
      error => {
        console.log(error);
      }
    )
  }

  generateUUID(){
    this.uuidValue=UUID.UUID();
    return this.uuidValue;
  }

  getBookPrices(id: any) {
    this.bookService.getBookTypeById(id).subscribe(
      (result:any) => {
        for(var i = 0; i < result.length; i++) {
          if(result[i].type.name == 'WebFile') {
            this.webBookPrice = result[i].price;
            this.showHideWebCard = true;
          } else if(result[i].type.name == 'AudioFile') {
            this.audioBookPrice = result[i].price;
            this.showHideAudioCard = true;
          } else if(result[i].type.name == 'Paperback') {
            this.paperBookPrice = result[i].price;
            this.showHidePaperCard = true;
          } 
        }
      }, 
      error => {
        console.log(error);
      }
    )
  }

  findBookInFreeList(userId: any, bookId: any) {
    this.subscriptionBookService.findBookInFreeBooks(userId, bookId).subscribe(
      result => {
        if(result) {
          this.showHideWebCard = false;
          this.showHideFreeCard = true;
        } else {
          this.showHideFreeCard = false;
          this.showHideWebCard = true;
        }
      }, error => {
        console.log(error);
      }
    )
  }

  sendFreeBook(userId: any, bookId: any) {
    this.bookService.sendFreeBook(userId, bookId).subscribe(
      result => {
        this.toastr.success("You may get the book on your email!", "Success!")
      }, 
      error => {
        console.log(error);
      }
    )
  }

    onWebClick(book: Book, price: any) {
      this.mockCartService.addToCart(book, "Web", price);
    }

    onAudioClick(book: Book, price: any) {
      this.mockCartService.addToCart(book, "Audio", price);
    }

    onPaperbackClick(book: Book, price: any) {
      this.mockCartService.addToCart(book, "Paperback", price);
    }
}
