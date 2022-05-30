import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-delivery',
  templateUrl: './delivery.component.html',
  styleUrls: ['../readerStyles.component.scss']
})
export class DeliveryComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  onClick() {
    window.close();
  }

}
