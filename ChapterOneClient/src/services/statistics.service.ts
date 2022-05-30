import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {

  public subscriptionUserStatistic: any;  

  constructor(private http: HttpClient, private router: Router) { }

  getSubscriptionUserStat() {
    return this.http.get(environment.baseURI + 'Statistic');
  }

  
}

