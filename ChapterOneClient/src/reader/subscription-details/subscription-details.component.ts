import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-subscription-details',
  templateUrl: './subscription-details.component.html',
  styleUrls: ['../readerStyles.component.scss']
})
export class SubscriptionDetailsComponent implements OnInit {

  userDetails: any;
  subscriptionStatus: any;
  subscriptionDate: any;
  constructor(private router: Router, public userService: UserService, private toastr: ToastrService) { }

  ngOnInit(): void {
    let token = localStorage.getItem('accessToken') as string;
    let tokenInfo = this.userService.getDecodedAccessToken(token);
    this.userService.getUserById(tokenInfo.id).subscribe(
      res => {
        this.userDetails = res
        this.checkStatusAndDate(this.userDetails.isSubscriptionPaid, this.userDetails.timeSubscriptionPaid);
      },
      err =>{
        console.log(err);
      }
    );
  }

  checkStatusAndDate(status: any, date: any) {
    if(!status) {
      this.subscriptionStatus = "Not defined";
    } else if(status === true){
      this.subscriptionStatus = "Active";
    } else {
      this.subscriptionStatus = "Inactive";
    }
    if(date) {
      let paymentButton = document.getElementById("pay-button");
      paymentButton!.style.pointerEvents = "none";
    }
  }
}
