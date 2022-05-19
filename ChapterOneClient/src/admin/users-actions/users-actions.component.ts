import { Component, Inject, OnInit, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { User } from 'src/models/user';

@Component({
  selector: 'app-users-actions',
  templateUrl: './users-actions.component.html',
  styleUrls: ['../adminStyles.component.scss']
})
export class UsersActionsComponent implements OnInit {

  action:string;
  local_data:any;
  constructor(public dialogRef: MatDialogRef<UsersActionsComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: User) { 
      this.local_data = {...data};
      this.action = this.local_data.action;
    }

    doAction(){
      this.dialogRef.close({event:this.action,data:this.local_data});
    }
  
    closeDialog(){
      this.dialogRef.close({event:'Cancel'});
    }

  ngOnInit(): void {
  }

}
