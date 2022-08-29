import { Component, OnInit } from '@angular/core';
import {Book,Category,BooksSearchCriteria,GetBooksByAuthor} from '../model/booksmodel';
import { BooksService } from '../service/books.service';

@Component({
  selector: 'app-author-comp',
  templateUrl: './author-comp.component.html',
  styleUrls: ['./author-comp.component.css']
})
export class AuthorCompComponent implements OnInit {
  searchText: any;
  pageTitle: string = "Books";
  iconWidth="35";
  imageMargin="2";
  filter = "";
  displayBookGrid: Boolean = true;
  displayAddBook: Boolean = false;
  showIcon: Boolean = false;
  bookAddedSucc: Boolean = false;
  category : Category[] = [
    {id : 1, title : "Comic"},
    {id : 2, title : "Thriller"},
    {id : 3, title : "Knowledge"},
    {id : 4, title : "Humour"}
  ];
books:Book[] = [];
book: Book ={
  book_Id:'string',
    logo:'/assets/Images/636359.jpg',
    book_Title:'',
    category:'',
    price:0,
    author_Id: parseInt(localStorage.getItem("UserId") || "0"),
    publisher:'',
    published_Date:new Date(),
    content:'',
    active:true,
    created_Date:new Date(),
    modified_Date:new Date()
}
show: boolean = true;
searchBooks: BooksSearchCriteria[]=[];
searchBook: GetBooksByAuthor={
    Author_Id: parseInt(localStorage.getItem("UserId") || "0")
}
Showfields : Boolean = false;
GetBookLogo() : void{
  this.showIcon = !this.showIcon;
}

onToggleClicked(event: any) {
  this.show = event.target.checked;
  console.log(this.show);
}

UpdateBookPrice(book: any): void{
  this.Showfields = true;
  this.displayBookGrid = false;
  this.book = book;
  //(document.getElementById("BookTitle") as HTMLInputElement).value = this.book.book_Title;
  //book.price = Number(book.price) + 10;
  // console.log(book.price);
  // return book.price;
}
  constructor(private booksService:BooksService) { }

  ngOnInit(): void {
    this.getAllBooks();
  }

  getAllBooks() {
    console.log(this.searchBook);
    this.booksService.GetBooksByAuthorId(this.searchBook)
    .subscribe(
      response => {
        this.books = response;
      }
    )
  }
  CancelAddBook(){
    this.displayBookGrid = true;
    this.displayAddBook = false;
    this.Showfields = false;
  }
  AddBook(){
    this.displayBookGrid = false;
    this.displayAddBook = true;
    if(this.book.book_Title != ''){
      console.log(this.book);
      this.booksService.UpdateBooks(this.book).subscribe(
        response =>{
          this.displayBookGrid =true;
          this.Showfields =false;
          this.getAllBooks();
        }, error => {
        this.bookAddedSucc = false;
      })
    }
  }
}
