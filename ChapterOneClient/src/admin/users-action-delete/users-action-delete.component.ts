import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-users-action-delete',
  templateUrl: './users-action-delete.component.html',
  styleUrls: ['../adminStyles.component.scss']
})
export class UsersActionDeleteComponent implements OnInit {

  action:string;
  constructor(public dialogRef: MatDialogRef<UsersActionDeleteComponent>) {
    this.action = "Delete";
   }

  ngOnInit(): void {
  }

  doAction(){
    this.dialogRef.close({event:this.action});
  }

  closeDialog(){
    this.dialogRef.close({event:'Cancel'});
  }

}
