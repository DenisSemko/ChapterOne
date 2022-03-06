import { Component, OnInit } from '@angular/core';
import { StatisticsService } from 'src/services/statistics.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-statistic',
  templateUrl: './statistic.component.html',
  styleUrls: ['../adminStyles.component.scss']
})
export class StatisticComponent implements OnInit {

  initOpts: any;
  options: any;
  subscriptionData: any;
  numbers = [];
  subscriptions = [];
  constructor(private statistic: StatisticsService, private http: HttpClient) {
   }

  ngOnInit(): void {
    this.http.get(environment.baseURI + 'Statistic').subscribe(
      res => {
      this.subscriptionData = res as SubscriptionUserStatistic;

      this.initOpts = {
        renderer: "svg",
        width: 500,
        height: 500
      };
    
      this.options = {
        title: {
          text: "Subscriptions - Number of Users",
          left: "center"
        },
        tooltip: {
          trigger: "item",
          width: "100%"
        },
        legend: {
          top: "90%",
          left: "center"
        },
        series: [
          {
            type: "pie",
            radius: "80%",
            avoidLabelOverlap: false,
            itemStyle: {
              borderRadius: 10,
              borderColor: "#fff",
              borderWidth: 2
            },
            label: {
              show: false,
              position: "center"
            },
            emphasis: {
              label: {
                show: false,
                fontSize: "40",
                fontWeight: "bold"
              }
            },
            labelLine: {
              show: false
            },
            data: [{value: this.subscriptionData.number[0], name: this.subscriptionData.subscriptions[0]},
            {value: this.subscriptionData.number[1], name: this.subscriptionData.subscriptions[1]},
            {value: this.subscriptionData.number[2], name: this.subscriptionData.subscriptions[2]}]
          }
        ]
      };
    });;
    
  
  }
}

interface SubscriptionUserStatistic {  
  subscriptions: Array<string>;  
  number: Array<number>;  
} 
