import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-default-reader',
  templateUrl: './default-reader.component.html',
  styleUrls: ['../readerStyles.component.scss']
})
export class DefaultReaderComponent implements OnInit {

  sideBarOpen = false;

  constructor() { }

  ngOnInit(): void {

  }

  sideBarToggler(event: Event) {
    this.sideBarOpen = !this.sideBarOpen;
  }
}
