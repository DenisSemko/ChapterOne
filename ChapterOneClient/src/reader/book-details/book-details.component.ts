import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Book } from 'src/models/book';
import { BookService } from 'src/services/book.service';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.scss']
})
export class BookDetailsComponent implements OnInit {

  book: Book;

  constructor(public bookService: BookService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadBook();
  }

  loadBook() {
    this.bookService.getBookById(this.route.snapshot.paramMap.get('id')).subscribe(
      result => {
        this.book = result as Book;
      }, 
      error => {
        console.log(error);
      }
    )
  }

}
