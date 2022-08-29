export interface Book{
    book_Id:string,
    logo:string,
    book_Title:string,
    category:string,
    price:number,
    author_Id:number,
    publisher:string,
    published_Date:Date,
    content:string,
    active:boolean,
    created_Date:Date,
    modified_Date:Date
}
export interface Category {
    id : number,
    title : string,
}
export interface BooksSearchCriteria{
    Category: string,
    Author_Id: number,
    Publisher:string
}
export interface GetBooksByAuthor{
    Author_Id: number
}
