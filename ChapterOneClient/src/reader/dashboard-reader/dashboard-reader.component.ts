import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SubscriptionService } from 'src/services/subscription.service';
import { UserService } from 'src/services/user.service';
import { ModalSubscriptionComponent } from '../modal-subscription/modal-subscription.component';

@Component({
  selector: 'app-dashboard-reader',
  templateUrl: './dashboard-reader.component.html',
  styleUrls: ['../readerStyles.component.scss']
})
export class DashboardReaderComponent implements OnInit {
  userDetails: any;

  constructor(public userService: UserService, public dialog: MatDialog, private subscriptionService: SubscriptionService) { }

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
  }

  checkSubscriptionPayment(user: any) {
    if(user.timeSubscriptionPaid === null) {
      this.openDialog();
    } else if(user.timeSubscriptionPaid) {
      this.subscriptionService.getSubscriptionPaymentDays(user.id).subscribe(
        res => {
          if(res)
          this.subscriptionService.sendSubscriptionPayment(user.id).subscribe(
            result => {
              this.openDialog();
            }
          )
            
      },
      err =>{
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



}
