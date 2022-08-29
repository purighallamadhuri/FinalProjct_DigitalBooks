import { Component, OnInit } from '@angular/core';
import {Book,Category,BooksSearchCriteria,GetBooksByAuthor} from '../model/booksmodel';
import { BooksService } from '../service/books.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-addbooks-comp',
  templateUrl: './addbooks-comp.component.html',
  styleUrls: ['./addbooks-comp.component.css']
})
export class AddbooksCompComponent implements OnInit {
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
  author_Id:parseInt(localStorage.getItem("UserId") || "0"),
  publisher:'',
  published_Date:new Date(),
  content:'',
  active:true,
  created_Date:new Date(),
  modified_Date:new Date()
}
searchBooks: BooksSearchCriteria[]=[];
searchBook: GetBooksByAuthor={
  Author_Id: 3
}
GetBookLogo() : void{
this.showIcon = !this.showIcon;
}
UpdateBookPrice(book: any): void{
book.price = Number(book.price) + 10;
// console.log(book.price);
// return book.price;
}
constructor(private booksService:BooksService,private toaster:ToastrService,private router:Router) { }

ngOnInit(): void {
  this.getAllBooks();
}

getAllBooks() {
  //console.log(this.searchBook);
  this.booksService.GetBooksByAuthorId(this.searchBook)
  .subscribe(
    response => {
      this.books = response
      //console.log(response);
    }
  )
  //console.log(response);
}
CancelAddBook(){
  this.displayBookGrid = true;
  this.displayAddBook = false;
}
AddBook(){
  this.displayBookGrid = false;
  this.displayAddBook = true;
  if(this.book.book_Title != ''){
    //console.log(this.book);
    this.booksService.addBook(this.book).subscribe(response =>{
      this.toaster.success("Book Added Successful!!", "Success");
        this.bookAddedSucc = true;
        this.displayBookGrid=true;
        this.displayAddBook = false;
        this.router.navigate(['/author']);
        //this.getAllBooks();
        //console.log(response);
    }, error =>{
      this.bookAddedSucc = false;
      this.toaster.error("Book Adding failed!!", "Failed");
    })
    

  }
}

}
