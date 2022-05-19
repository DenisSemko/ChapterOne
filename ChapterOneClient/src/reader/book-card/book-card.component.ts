import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Book } from 'src/models/book';
import { BookService } from 'src/services/book.service';
import { RateService } from 'src/services/rate.service';
import { BookCollectionModalComponent } from '../book-collection-modal/book-collection-modal.component';

@Component({
  selector: 'app-book-card',
  templateUrl: './book-card.component.html',
  styleUrls: ['./book-card.component.scss']
})
export class BookCardComponent implements OnInit {

  imageToShow: any;
  @Input() book: Book;
  currentRating: any;
  numberOfReviews: any;
  bookType: any;
  currentUrl: any;
  showAddCollectionButton: boolean = false;

  constructor(public bookService: BookService, private router: Router, public rateService: RateService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.setImage(this.book.bookImage);
    this.getBookType(this.book.id);
    this.getBookRating(this.book.id);
    this.getNumberOfReviews(this.book.id);
    this.currentUrl = this.router.url.substring(0, this.router.url.length);
    this.showCollectionButton(this.currentUrl);
  }

  public setImage(imageUrl: any) {
    this.bookService.getImagePath(imageUrl).subscribe(data => {
      this.createImageFromBlob(data);
    }, error => {
      console.log(error);
    });
}

  private createImageFromBlob(image: Blob) {
    let reader = new FileReader();
    reader.addEventListener("load", () => {
        this.imageToShow = reader.result;
    }, false);

    if (image) {
        reader.readAsDataURL(image);
    }
  }

  public getBookType(id: any) {
    this.bookService.getWebFileByBookId(id).subscribe(
      result => {
        this.bookType = result;
      },
      error => {
        console.log(error);
      }
    )
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

  public openCollectionDialog(obj: any) {
    const dialogRef = this.dialog.open(BookCollectionModalComponent, {
      width: '60%',
      data:obj,
      panelClass: 'custom-modalbox'
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result === true)
        console.log(result);
    });
  }

  showCollectionButton(url: any) {
    if(url == "/reader/dashboard") {
      this.showAddCollectionButton = !this.showAddCollectionButton;
    } else {
      this.showAddCollectionButton = false;
    }
  }

}
