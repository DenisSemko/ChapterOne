import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { SubscriptionService } from 'src/services/subscription.service';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-users-action-add-admin',
  templateUrl: './users-action-add-admin.component.html',
  styleUrls: ['../adminStyles.component.scss']
})
export class UsersActionAddAdminComponent implements OnInit {

  hide = true;
  minDate: Date;
  subscriptionsList: any;
  constructor(public service: UserService, private toastr: ToastrService, public subscriptionService: SubscriptionService,
    public dialogRef: MatDialogRef<UsersActionAddAdminComponent>) {
    const currentYear = new Date().getFullYear();
    this.minDate = new Date(currentYear - 14, 11, 31);
   }

  ngOnInit(): void {
    this.service.formModel.reset();

    this.subscriptionService.getSubscriptionsList().subscribe(
      res => {
        this.subscriptionsList = res
      },
      err =>{
        console.log(err);
      }
    );
    
  }
  

  onSubmit()
  {
    this.service.registerAdmin().subscribe(
      (res:any) => {
          this.service.formModel.reset();
          this.toastr.success('New admin has been successfully created!', 'Successful.', {
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

  closeDialog(){
    this.dialogRef.close({event:'Cancel'});
  }

}
