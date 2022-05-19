import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UUID } from 'angular2-uuid';
import { ToastrService } from 'ngx-toastr';
import { Book } from 'src/models/book';
import { Category } from 'src/models/category';
import { Collection } from 'src/models/collection';
import { BookCollectionService } from 'src/services/book-collection.service';
import { CategoryService } from 'src/services/category.service';
import { CollectionService } from 'src/services/collection.service';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-book-collection-modal',
  templateUrl: './book-collection-modal.component.html',
  styleUrls: ['../readerStyles.component.scss']
})
export class BookCollectionModalComponent implements OnInit {

  local_data:any;
  userDetails: any;
  collections: any;
  uuidValue: string = '';


  constructor(public dialogRef: MatDialogRef<BookCollectionModalComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: Book, public collectionService: CollectionService, 
    public bookCollectionService: BookCollectionService, public userService: UserService, private toastr: ToastrService) { 
      this.local_data = {...data};
    }

  ngOnInit(): void {
    this.bookCollectionService.formModel.reset();
    let token = localStorage.getItem('accessToken') as string;
    let tokenInfo = this.userService.getDecodedAccessToken(token);
    this.getUserDetails(tokenInfo.id);
  }

  closeDialog(){
    this.dialogRef.close({event:'Cancel'});
  }

  getUserDetails(id: any) {
    this.userService.getUserById(id).subscribe(
      res => {
        this.userDetails = res
        this.getCollection(this.userDetails.id);
      },
      err =>{
        console.log(err);
      }
    );
  }

  getCollection(userId: any) {
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
    this.bookCollectionService.addBookCollection().subscribe(
      (res:any) => {
          this.bookCollectionService.formModel.reset();
          this.toastr.success('New Book Collection has been successfully created!', 'Successful.', {
            timeOut: 5000
          });
          this.closeDialog();
      },
      (err: HttpErrorResponse) => {
        this.toastr.error(`${err.error.title}`);
        for(let i in err.error.errors) {
          this.toastr.error(`${err.error.errors[i]}`);
        }
        console.log(err);
      }
    )
  }

  generateUUID(){
    this.uuidValue=UUID.UUID();
    return this.uuidValue;
  }

}
