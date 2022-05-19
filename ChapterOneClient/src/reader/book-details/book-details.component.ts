import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/models/user';
import { BookService } from 'src/services/book.service';
import { RateService } from 'src/services/rate.service';
import { UserService } from 'src/services/user.service';
import { UUID } from 'angular2-uuid';

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


  constructor(public bookService: BookService, public router: Router, 
    public rateService: RateService, public userService: UserService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.bookId = this.router.url.substring(13, this.router.url.length);
    let token = localStorage.getItem('accessToken') as string;
    let tokenInfo = this.userService.getDecodedAccessToken(token);

    this.getBookById(this.bookId);
    this.getBookRating(this.bookId);
    this.getNumberOfReviews(this.bookId);
    this.getReviews(this.bookId);
    this.getCurrentUser(tokenInfo.id);
    this.getBookPrices(this.bookId);
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
          } else if(result[i].type.name == 'AudioFile') {
            this.audioBookPrice = result[i].price;
          } else if(result[i].type.name == 'Paperback') {
            this.paperBookPrice = result[i].price;
          }
        }
      }, 
      error => {
        console.log(error);
      }
    )
  }
}
