import { Component, Input, OnInit } from '@angular/core';
import { Book } from 'src/models/book';

@Component({
  selector: 'app-card-modal',
  templateUrl: './card-modal.component.html',
  styleUrls: ['./card-modal.component.scss']
})
export class CardModalComponent implements OnInit {

  @Input() book: Book;
  constructor() { }

  ngOnInit(): void {
  }

}
