import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {

  constructor(private http: HttpClient, private router: Router) { }

  getSubscriptionsList() {
    return this.http.get(environment.baseURI + 'Subscription')
  }

  getSubscriptionPaymentDays(userId: string) {
    return this.http.get(environment.baseURI + 'SubscriptionPayment/' + userId);
  }

  sendSubscriptionPayment(userId: string) {
    return this.http.get(environment.baseURI + 'SubscriptionPayment/' + userId + "/send-email");
  }

  sendSubscriptionPaymentSuccess(userId: string) {
    return this.http.get(environment.baseURI + 'SubscriptionPayment/' + userId + "/send-subscription-success");
  }

  updateUserSubscription(username: string) {
    return this.http.get(environment.baseURI + "User/" + username + "/update-subscription");
  }


}
