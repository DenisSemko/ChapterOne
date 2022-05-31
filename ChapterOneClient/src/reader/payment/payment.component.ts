import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/models/user';
import { CollectionService } from 'src/services/collection.service';
import { MockCartService } from 'src/services/mock-cart.service';
import { SubscriptionService } from 'src/services/subscription.service';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['../readerStyles.component.scss']
})
export class PaymentComponent implements OnInit {

  cc: any;
  userDetails: any;
  constructor(private toastr: ToastrService, public router: Router, public subscriptionPayment: SubscriptionService,
    public service: UserService, public mockCartService: MockCartService) { }

  ngOnInit(): void {
    this.onClick();
  }

  onClick() {
    var buttons = document.getElementsByClassName("cc-form__button");
    for (var i = 0; i < buttons.length; i++) {
      buttons[i].addEventListener('click', () => {
        this.afterClickMessage();
      });
    }
  }

  afterClickMessage() {
    this.toastr.success("Your payment is successful!", "Success!",  {
      timeOut: 5000
    });
    this.toastr.success("Please check your email", "Success!",  {
      timeOut: 5000
    });

    let token = localStorage.getItem('accessToken') as string;
    let tokenInfo = this.service.getDecodedAccessToken(token);
    
    if(this.checkUrl()) {
      this.mockCartService.sendBookPaymentSuccess(tokenInfo.id).subscribe(
        result => {
          this.router.navigateByUrl("/reader/dashboard");
        }, 
        error => {
          console.log(error);
        }
      );
    } else {
      this.updateUserSubscription(tokenInfo.id);
      this.subscriptionPayment.sendSubscriptionPaymentSuccess(tokenInfo.id).subscribe(
        result => {
          this.router.navigateByUrl("/reader/dashboard");
        }, 
        error => {
          console.log(error);
        }
      );
    }
  }

  updateUserSubscription(userId: any) {
    this.service.getUserById(userId).subscribe(
      result => {
        this.userDetails = result as User;
        this.subscriptionPayment.updateUserSubscription(this.userDetails.userName).subscribe(
          result => {
    
          },
          error => {
            console.log(error);
          }
        ); 
      },
      error => {
        console.log(error);
      }
    );
  }

  checkUrl() : boolean{
    if(this.router.url.endsWith("book")) 
      return true;
    return false;
  }

}
