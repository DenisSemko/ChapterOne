import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Book } from 'src/models/book';
import { BookService } from 'src/services/book.service';
import { SubscriptionBookService } from 'src/services/subscription-book.service';
import { SubscriptionService } from 'src/services/subscription.service';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['../adminStyles.component.scss']
})
export class AddBookComponent implements OnInit {

  bookImageToShow: any;
  showSubscriptionPriceDiv: boolean = false;
  genresList: any;
  subscriptionsList: any;
  typesList: any;
  addedBook: Book;
  maxDate: Date;
  constructor(public bookService: BookService, private toastr: ToastrService, public subsBookService: SubscriptionBookService,
    public subscriptionService: SubscriptionService) { 
    this.maxDate = new Date();
  }

  ngOnInit(): void {
    this.getGenres();
    this.getSubscriptions();
    this.getTypes();
    this.bookService.formModel.reset();
    this.subsBookService.formModel.reset();
    this.bookService.bookTypeModel.reset();
  }

  OnSelectBookImage(event: any) {
    if (event.target.files && event.target.files[0]) {
      const file: File = event.target.files[0];
      this.bookService.uploadImagesFiles(this.addedBook.id, file).subscribe(
        result => {
          this.toastr.success('Image for book has been successfully added!', 'Success');
        }, 
        error => {
          console.log(error);
        }
      )


      var reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]); 

      reader.onload = (event) => { 
        this.bookImageToShow = <string>reader.result;
      }
    }
  }

  OnSelectBookFile(event: any) {
    if (event.target.files && event.target.files[0]) {
      const file: File = event.target.files[0];
      this.bookService.uploadImagesFiles(this.addedBook.id, file).subscribe(
        result => {
          this.toastr.success('This book file has been successfully added!', 'Success');
        }, 
        error => {
          console.log(error);
        }
      )
    }
  }

  getGenres() {
    this.bookService.getGenres().subscribe(
      result => {
        this.genresList = result;
      }, error => {
        console.log(error);
      }
    )
  }

  getSubscriptions() {
    this.subscriptionService.getSubscriptionsList().subscribe(
      result => {
        this.subscriptionsList = result;
        const index = this.subscriptionsList.indexOf(this.subscriptionsList[0]);
        this.subscriptionsList.splice(index, 1);
      }, error => {
        console.log(error);
      }
    )
  }

  getTypes() {
    this.bookService.getTypes().subscribe(
      result => {
        this.typesList = result;
      }, error => {
        console.log(error);
      }
    )
  }

  onSubmit() {
    this.bookService.addBook().subscribe(
      (res:any) => {
          this.addedBook = res;
          this.toastr.success('New book has been successfully created!', 'Success');
          this.showSubscriptionPriceDiv = true;
      },
      (err: any) => {
        console.log(err);
      }
    )
  }

  onSubscriptionSubmit() {
    this.subsBookService.addSubscriptionBook().subscribe(
      (res:any) => {
          this.toastr.success('Added successfully!', 'Success');
      },
      (err: any) => {
        console.log(err);
      }
    )
  }

  onBookTypeSubmit() {
    this.bookService.addBookType().subscribe(
      (res:any) => {
          this.toastr.success('Added successfully!', 'Success');
      },
      (err: any) => {
        console.log(err);
      }
    )
  }
  
  reload() {
    window.location.reload();
  }
 

}
