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
  initBarChartOpts: any;
  options: any;
  barChartOpt: any;
  subscriptionData: any;
  bookData: any;
  numbers = [];
  subscriptions = [];
  constructor(private statistic: StatisticsService, private http: HttpClient) {
   }

  ngOnInit(): void {
    this.getSubscriptionUserStatistic();
    this.getBookRateStatistic();
  }

  getSubscriptionUserStatistic() {
    this.http.get(environment.baseURI + 'Statistic').subscribe(
      res => {
      this.subscriptionData = res as SubscriptionUserStatistic;

      this.initOpts = {
        renderer: "svg",
        width: 800,
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
    });
  }

  getBookRateStatistic() {
    this.http.get(environment.baseURI + 'Statistic' + '/most-popular').subscribe(
      res => {
      this.bookData = res as BookRateStatistic;

      this.initBarChartOpts = {
        renderer: "svg",
        width: 600,
        height: 500
      };

      this.barChartOpt = {
        title: {
          text: "Most Popular Books by its Marks",
          left: "center"
        },
        tooltip: {
          trigger: "item",
          width: "100%"
        },
        xAxis: {
          type: 'category',
          data: [this.bookData.books[0], this.bookData.books[1], this.bookData.books[2]]
        },
        yAxis: {
          type: 'value'
        },
        series: [
          {
            data: [this.bookData.marks[0], this.bookData.marks[1], this.bookData.marks[2]],
            type: 'bar',
            showBackground: true,
            backgroundStyle: {
              color: 'rgba(180, 180, 180, 0.2)'
            }
          }
        ]
      };
    });
  }
}

interface SubscriptionUserStatistic {  
  subscriptions: Array<string>;  
  number: Array<number>;  
} 

interface BookRateStatistic {
  books: Array<string>;
  marks: Array<number>;
}
