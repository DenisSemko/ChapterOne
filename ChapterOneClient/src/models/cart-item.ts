import { Book } from "./book";

export interface CartItem {
    book: Book;
    type: string;
    price: any;
  }