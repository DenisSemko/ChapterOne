import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Book } from 'src/models/book';
import { Pagination } from 'src/models/pagination';
import { BookService } from 'src/services/book.service';
import { SubscriptionService } from 'src/services/subscription.service';
import { UserService } from 'src/services/user.service';
import { ModalSubscriptionComponent } from '../modal-subscription/modal-subscription.component';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-dashboard-reader',
  templateUrl: './dashboard-reader.component.html',
  styleUrls: ['../readerStyles.component.scss']
})
export class DashboardReaderComponent implements OnInit {
  userDetails: any;
  booksList: Book[];
  genresList: any;
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 4;
  filterButtonStatus: string = "";
  genreName: any;

  constructor(public userService: UserService, public dialog: MatDialog, private subscriptionService: SubscriptionService,
    public bookService: BookService) { }

  ngOnInit(): void {
    let token = localStorage.getItem('accessToken') as string;
    let tokenInfo = this.userService.getDecodedAccessToken(token);
    this.userService.getUserById(tokenInfo.id).subscribe(
      res => {
        this.userDetails = res
        this.checkSubscriptionPayment(this.userDetails);
      },
      err =>{
        console.log(err);
      }
    );
    this.getBooks();
    this.getGenres();
  }

  checkSubscriptionPayment(user: any) {
    if(user.timeSubscriptionPaid === null) {
      this.openDialog();
    } else if(user.timeSubscriptionPaid) {
      this.subscriptionService.getSubscriptionPaymentDays(user.id).subscribe(
        (res: any) => {
          if(res)
          this.subscriptionService.sendSubscriptionPayment(user.id).subscribe(
            (result:any) => {
              this.openDialog();
            }
          )
            
      },
      (err:any) =>{
          console.log(err);
        }
      );
    }
  }
  openDialog() {
    const dialogRef = this.dialog.open(ModalSubscriptionComponent, {
      width: '60%',
      height: '55%',
      panelClass: 'custom-modalbox',
      disableClose: true
    });
  }

  getBooks() {
    this.bookService.getBooksList(this.pageNumber, this.pageSize).subscribe(
      result => {
        this.booksList = result.result;
        this.pagination = result.pagination
      },
      error => {
        console.log(error);
      }
    )
  }

  pageChanged(event: any): void {
    this.pageNumber = event.pageIndex + 1;
    if(this.filterButtonStatus === "") {
      this.getBooks();
    } else if(this.filterButtonStatus === "Free") {
      this.onFreeClick();
    } else if(this.filterButtonStatus == "Genre") {
      this.onGenreClick(this.genreName);
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

  onFreeClick() {
    this.bookService.getBooksListFreeFilter(this.pageNumber, this.pageSize, this.userDetails.subscription.id).subscribe(
      result => {
        this.booksList = result.result;
        this.pagination = result.pagination
        this.filterButtonStatus = "Free";
      },
      error => {
        console.log(error);
      }
    )
  }

  onGenreClick(name: any) {
    this.bookService.getBooksListGenreFilter(this.pageNumber, this.pageSize, name).subscribe(
      result => {
        this.booksList = result.result;
        this.pagination = result.pagination
        this.filterButtonStatus = "Genre";
        this.genreName = (<HTMLInputElement>document.getElementById("genre"))?.value;
      },
      error => {
        console.log(error);
      }
    )
  }

  resetFilter() {
    this.filterButtonStatus = "";
    this.getBooks();
  }



}
