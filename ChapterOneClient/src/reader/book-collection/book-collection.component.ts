import {COMMA, ENTER} from '@angular/cdk/keycodes';
import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {FormControl} from '@angular/forms';
import {MatAutocompleteSelectedEvent} from '@angular/material/autocomplete';
import {MatChipInputEvent} from '@angular/material/chips';
import { UUID } from 'angular2-uuid';
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import { Book } from 'src/models/book';
import { Category } from 'src/models/category';
import { Collection } from 'src/models/collection';
import { BookCollectionService } from 'src/services/book-collection.service';
import { CategoryService } from 'src/services/category.service';
import { CollectionService } from 'src/services/collection.service';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-book-collection',
  templateUrl: './book-collection.component.html',
  styleUrls: ['../readerStyles.component.scss']
})
export class BookCollectionComponent implements OnInit {

  separatorKeysCodes: number[] = [ENTER, COMMA];
  categoryCtrl = new FormControl();
  categories: Category[] = [];
  userDetails: any;
  uuidValue: string = '';
  collections: Collection[];

  @ViewChild('fruitInput') fruitInput: ElementRef<HTMLInputElement>;

  constructor(public userService: UserService, public categoryService: CategoryService, public bookCollectionService: BookCollectionService,
    public collectionService: CollectionService) {

  }

  ngOnInit(): void {

    let token = localStorage.getItem('accessToken') as string;
    let tokenInfo = this.userService.getDecodedAccessToken(token);
    this.getUserDetails(tokenInfo.id);
    this.getCollectionsByUserId(tokenInfo.id);
  }

  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    if (value) {
      this.addCategory(event.value);
    }

    event.chipInput!.clear();

    this.categoryCtrl.setValue(null);
  }

  remove(category: Category): void {
    let id = category.id;
    if (id) {
      this.categoryService.deleteCategory(id).subscribe(
        result => {
          const index = this.categories.indexOf(category);
          this.categories.splice(index, 1);
        },
        error => {
          console.log(error);
        }
      );
    }
  }

  addCategory(name: any) {
    this.generateUUID();
    var body = {
      Id: this.uuidValue,
      User : this.userDetails.id,
      Name : name
    };
    this.categoryService.addCategory(body).subscribe(
      result => {
        this.categories.push(result as Category);
      },
      error => {
        console.log(error);
      }
    )

  }
  getUserDetails(id: any) {
    this.userService.getUserById(id).subscribe(
      res => {
        this.userDetails = res
        this.getCategory(this.userDetails.id);
      },
      err =>{
        console.log(err);
      }
    );
  }

  getCategory(userId: any) {
    this.categoryService.getCategoryByUser(userId).subscribe(
      result => {
        this.categories = result as Category[];
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
  
  getCollectionsByUserId(userId: any) {
    this.collectionService.getByUserId(userId).subscribe(
      result => {
        this.collections = result as Collection[];
      },
      error => {
        console.log(error);
      }
    )
    
  }

  onSubmit()
  {
    this.collectionService.addCollection().subscribe( 
      result => {
        this.collectionService.formModel.reset();
        window.location.reload();
      }, 
      error  => {
        console.log(error);
      }
    )

  }



}
