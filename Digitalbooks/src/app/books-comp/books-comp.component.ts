import { Component, OnInit,TemplateRef } from '@angular/core';
import { BooksService } from '../service/books.service';
import { UserService } from '../service/users.service';
import {Book,Category,BooksSearchCriteria} from '../model/booksmodel';
import { Users } from '../model/usersmodel';
import { PaymentDetails } from '../model/paymentmodel';
import {PaymentService} from '../service/payment.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';

@Component({
  selector: 'app-books-comp',
  templateUrl: './books-comp.component.html',
  styleUrls: ['./books-comp.component.css']
})
export class BooksCompComponent implements OnInit {
  pageTitle: string = "Books";
  showSuccMsg:Boolean = false;
  iconWidth="35";
  imageMargin="2";
  showIcon: Boolean = false;
  filter = "";
  category : Category[] = [
    {id : 1, title : "Comic"},
    {id : 2, title : "Thriller"},
    {id : 3, title : "Knowledge"},
    {id : 4, title : "Humour"}
  ];
  books:Book[] = [];
  book: Book ={
  book_Id:'string',
    logo:'../assets/Images/636359.jpg',
    book_Title:'',
    category:'',
    price:0,
    author_Id:3,
    publisher:'',
    published_Date:new Date(),
    content:'',
    active:true,
    created_Date:new Date(),
    modified_Date:new Date()
}
authors: Users[] = [];
publishers:Book[] = [];
booksSearch:BooksSearchCriteria[]=[];
bookSearch:BooksSearchCriteria={
  Category:'',
  Author_Id:0,
  Publisher:''
}
hidemenu: Boolean =false;
showGrid: Boolean=false;
payment: PaymentDetails[]=[];
paymentDtls: PaymentDetails={
  payment_Id :'',
  emailId:'',
  User_Id : 0,
    book_Id : '',
    payment_Date : new Date(),
    transaction_Id : '',
    payment_Mode : 'Guest',
    bookName :'',
    price: 0
}
showEmailField: Boolean =false;
emptyshowEmailField: Boolean = false;
modalRef?: BsModalRef;
showFailedMsg: Boolean =false;
userEmailId: any;
showErrMsg:Boolean=false;
proceedPayment(){
  if(this.userEmailId != ''){
    this.paymentDtls.book_Id = localStorage.getItem("BookId") || "";
    this.paymentDtls.bookName = localStorage.getItem("BookName") || "";
    this.paymentDtls.User_Id = parseInt(localStorage.getItem("UserId") || "0");
    this.paymentDtls.price = parseInt(localStorage.getItem("Price") || "0");
    this.paymentDtls.emailId = this.userEmailId;
    //localStorage.setItem("BuyerEmailId",this.paymentDtls.emailId);
    this.paymentService.ProceedPayment(this.paymentDtls).subscribe(
      response => {
        console.log(response);
        
        localStorage.setItem("BookId","");
        localStorage.setItem("BookName","");
        console.log(response.result);
        if(response.result == "This book was already bought by you!!")
        {
          this.showErrMsg = true;
          this.showSuccMsg = false;
        }
        else{
          this.showSuccMsg = true;
          this.showErrMsg = false;
        }
      },
      error=>{
        console.log(error);
        this.showFailedMsg = true;
        localStorage.setItem("BookId","");
        localStorage.setItem("BookName","");
      }
    )
  }
}
openModal(template: TemplateRef<any>,book:any) {
  console.log(book);
    // this.showGrid = false;
    // this.showEmailField = true;
    localStorage.setItem("BookId",book.book_Id);
    localStorage.setItem("BookName",book.book_Title);
    localStorage.setItem("Price",book.price);
    this.userEmailId = localStorage.getItem("UserEmailId");
    //localStorage.removeItem("UserEmailId");
    console.log(this.userEmailId);
    if(this.userEmailId != null)
    {
      console.log("disabled")
      this.showEmailField = true;
    }
    else{
      console.log("not disabled")
      this.emptyshowEmailField=true;
    }
    this.modalRef = this.modalService.show(template,book);
}
OpenPaymentWindow(paymentDtls : any){
  this.router.navigate(["/payment"]);
}
  GetBookLogo() : void{
    this.showIcon = !this.showIcon;
  }
  UpdateBookPrice(book: any): void{
    book.price = Number(book.price) + 10;
    // console.log(book.price);
    // return book.price;
  }
  BuyBook(book:any): void{
    console.log(book);
    this.showGrid = false;
    this.showEmailField = true;
    
  }
   constructor(private booksService:BooksService,private userService: UserService,private paymentService: PaymentService,private modalService: BsModalService,private router:Router) { }

  ngOnInit(): void {
    this.hidemenu =false;
    this.showSuccMsg = false;
    this.getAllBooks();
    this.GetAllPublishers();
    this.GetAllAuthors();
  }
  GetAllAuthors(){
    this.userService.GetAllAuthors().subscribe( response => {
      console.log(response);
      this.authors = response;
      console.log("hi" + this.authors);
    },
    error =>{
      console.log("error");
    });
  }
  HideMainbar(){
    this.hidemenu = true;
  }
  GetAllPublishers(){
    this.booksService.GetAllPublishers().subscribe( response => {
      console.log(response);
      this.publishers = response;
    },
    error =>{
      console.log("error");
    });
  }
  getAllBooks() {
    console.log("Hi");
    this.booksService.getAllBooks()
    .subscribe(
      response => {this.books = response}
    )
    //console.log(response);
  }
  SearchBooks() {
    console.log(this.bookSearch);
    this.booksService.SearchBooks(this.bookSearch)
    .subscribe(
      response =>{
        console.log(response);
        this.books = response;
        this.showGrid=true;
      },
      error=>{
        console.log("error");
      }
    );
  }

}
