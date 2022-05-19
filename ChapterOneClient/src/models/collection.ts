import { Category } from "./category";

export interface Collection {
    id: string,
    name: string,
    user: string,
    category: Category
}