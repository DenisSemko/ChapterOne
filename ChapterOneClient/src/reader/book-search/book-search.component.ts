import { Component, OnInit, ViewChild } from '@angular/core';
import { Combination } from 'src/models/combination';
import { CombinationService } from 'src/services/combination.service';
import { UUID } from 'angular2-uuid';
import { UserService } from 'src/services/user.service';
import { Book } from 'src/models/book';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { Pagination } from 'src/models/pagination';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { CollectionService } from 'src/services/collection.service';

@Component({
  selector: 'app-book-search',
  templateUrl: './book-search.component.html',
  styleUrls: ['../readerStyles.component.scss']
})
export class BookSearchComponent implements OnInit {
  combination: Combination;
  data: any;
  uuidValue: string = '';
  public isSuccessful: boolean = false;
  readerId: any;
  author: any;
  year: any;
  genre: any;
  publisher: any;
  short: any;
  isOldButtonVisible: boolean = false;
  isOldButtonDisable: boolean = true;
  isCollectionButtonVisible: boolean = false;
  isDataTableVisible: boolean = false;
  isSuccessfulDivVisible: boolean = false;
  books: Book[] = [];
  displayedBooks: any;
  oldCombinationsList: Combination[];
  displayedColumns: string[] = ['Title', 'Author', 'PublishedDate', 'Publisher', 'Language'];
  dataSource = new MatTableDataSource<Book>(this.books);
  displayedCombinationColumns: string[] = ['Author', 'Year', 'Genre', 'Publisher', 'ShortDescription', 'ResultPercentage'];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 4;
  checked: boolean = false;

  @ViewChild(MatPaginator, { static: true })
  paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true })
  sort!: MatSort;


  constructor(public combinationService: CombinationService, private userService: UserService, public collectionService: CollectionService) { }

  ngOnInit(): void {

    //to have a reader id
    this.getReaderId();

    //to show the button
    this.getCombinationByUser(this.readerId);

    //to check collections
    this.checkCollection(this.readerId);
  }
  
  //Working with Cloud and Generation combination
  onWorkClick(event: any) {
    this.author = this.combination.author;
    this.year = this.combination.year.toString();
    this.genre = this.combination.genre;
    this.publisher = this.combination.publisher;
    this.short = this.combination.shortDescription;
  }

  getGeneratedCombination() {
    this.combinationService.generateCombination().subscribe(
      result => {
        this.combination = result as Combination;
        this.dataSource.data = [];
        this.isDataTableVisible = false;
        this.isSuccessfulDivVisible = false;
        this.getReaderId();
        this.data = [this.combination.author, this.combination.year, this.combination.genre, this.combination.publisher, this.combination.shortDescription].map(function (d) {
          return { text: d, value: 10 + Math.random() * 80};
        });
      },
      error => {
        console.log(error);
      }
    )
  }

  generateCombination() {
    this.checked = false;
    this.isOldButtonDisable = false;
    document.getElementById("old-button")!.style.backgroundColor="#62716a";
    this.getGeneratedCombination();
  }

  generateCombinationFromCollection(userId: any) {
    this.combinationService.generateCombinationFromCollections(userId).subscribe(
      result => {
        this.combination = result as Combination;
        this.dataSource.data = [];
        this.isDataTableVisible = false;
        this.isSuccessfulDivVisible = false;
        this.getReaderId();
        this.data = [this.combination.author, this.combination.year, this.combination.genre, this.combination.publisher, this.combination.shortDescription].map(function (d) {
          return { text: d, value: 10 + Math.random() * 80};
        });
      },
      error => {
        console.log(error);
      }
    )

  }

  checkCollection(userId: any) {
    this.collectionService.getByUserId(userId).subscribe(
      result => {
        if(result) {
          this.isCollectionButtonVisible = true;
        }
      },
      error => {
        console.log(error);
      }
    )
  }

  //Working with the form to POST Combination
  onSubmit() {
    this.getReaderId();
    this.combinationService.addCombination().subscribe(
      (res:any) => {
        if(res) {
          this.combination = res;
          this.isDataTableVisible = !this.isDataTableVisible;
          this.findBooks();
          document.getElementById("message")!.innerHTML = "PLEASE REFRESH PAGE TO GENERATE AGAIN";
          this.isSuccessfulDivVisible = !this.isSuccessfulDivVisible;
        }
      },
      (err: any) => {
        console.log(err);
      }
    )
  }

  generateUUID(){
    this.uuidValue=UUID.UUID();
    return this.uuidValue;
  }

  getCombinationByUser(id: any) {
    this.combinationService.getCombinationByUser(id).subscribe(
      (res:any) => {
        if(res.length > 0) {
          this.isOldButtonVisible = !this.isOldButtonVisible;
        }
      },
      err => {
        console.log(err);
      }
    );
  }

  getOldSchemes() {
    this.combinationService.getOldSchemes(this.readerId).subscribe(
      (res:any) => {
        this.oldCombinationsList = res as Combination[];
      },
      err => {
        console.log(err);
      }
    )
  }

  applyOldScheme(combination: any) {
    this.author = combination.author;
    this.year = combination.year.toString();
    this.genre = combination.genre;
    this.publisher = combination.publisher;
    this.short = combination.shortDescription;
  }

  findBooks() {
    this.combinationService.findBooks(this.combination.author, this.combination.year, this.combination.genre, this.combination.publisher, this.combination.shortDescription).subscribe(
      (res:any) => {
        this.displayedBooks = (res.body as Book[]);
        this.dataSource.data = this.displayedBooks;
      },
      err => {
        console.log(err);
      }
    )
  }

  getReaderId() {
    let token = localStorage.getItem('accessToken') as string;
    let tokenInfo = this.userService.getDecodedAccessToken(token);
    this.readerId = tokenInfo.id
    this.isSuccessful = false;
  }

  onUpdate() {
    this.getReaderId();
    this.combinationService.updateCombination(this.combination).subscribe( 
      result => {
        window.location.reload();
      }, 
      error  => {
        console.log(error);
      }
    )
  }

  pageChanged(event: any): void {
    this.pageNumber = event.pageIndex + 1;
  }

  crossAuthor(event: MatCheckboxChange) {
    if(event.checked) {
      this.author = "";
      document.getElementById("authorLabel")!.style.textDecoration = "line-through";
    } else {
      this.author = this.combination.author;
      document.getElementById("authorLabel")!.style.textDecoration = "none";
    }
  }
  crossYear(event: MatCheckboxChange) {
    if(event.checked) {
      this.year = 0;
      document.getElementById("yearLabel")!.style.textDecoration = "line-through";
    } else {
      this.year = this.combination.year;
      document.getElementById("yearLabel")!.style.textDecoration = "none";
    }
  }
  crossGenre(event: MatCheckboxChange) {
    if(event.checked) {
      this.genre = "";
      document.getElementById("genreLabel")!.style.textDecoration = "line-through";
    } else {
      this.genre = this.combination.genre;
      document.getElementById("genreLabel")!.style.textDecoration = "none";
    }
  }
  crossPublisher(event: MatCheckboxChange) {
    if(event.checked) {
      this.publisher = "";
      document.getElementById("publisherLabel")!.style.textDecoration = "line-through";
    } else {
      this.publisher = this.combination.publisher;
      document.getElementById("publisherLabel")!.style.textDecoration = "none";
    }
  }
  crossDescription(event: MatCheckboxChange) {
    if(event.checked) {
      this.short = "";
      document.getElementById("shortLabel")!.style.textDecoration = "line-through";
    } else {
      this.short = this.combination.shortDescription;
      document.getElementById("shortLabel")!.style.textDecoration = "none";
    }
  }
}
