import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Book } from 'src/models/book';
import { BookCollectionService } from 'src/services/book-collection.service';
import { CollectionService } from 'src/services/collection.service';

@Component({
  selector: 'app-collection',
  templateUrl: './collection.component.html',
  styleUrls: ['../readerStyles.component.scss']
})
export class CollectionComponent implements OnInit {

  booksList: any;
  collectionId: any;

  constructor(public bookCollectionService: BookCollectionService, private router: Router, public collectionService: CollectionService) { }

  ngOnInit(): void {
    this.collectionId = this.router.url.substring(24, this.router.url.length);
    this.getBooksByCollection(this.collectionId);
  }

  getBooksByCollection(id: any) {
    this.bookCollectionService.getBooksByCollection(id).subscribe(
      result => {
        this.booksList = result as Book[]
      },
      error => {
        console.log(error);
      }
    )
  }

  deleteCollection(id: any) {
    this.collectionService.deleteById(id).subscribe(
      result => {
        this.router.navigateByUrl("/reader/my-book-collection");
      },
      error => {
        console.log(error);
      }
    )
  }

}
