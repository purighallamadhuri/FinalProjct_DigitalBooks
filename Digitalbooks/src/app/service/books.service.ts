import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import {Book,BooksSearchCriteria,GetBooksByAuthor} from '../model/booksmodel';

@Injectable({
    providedIn:'root'
})

export class BooksService{
    //baseUrl = 'https://localhost:7284/';
    baseUrl = 'https://booksapi20220828212050.azurewebsites.net/';
    token = localStorage.getItem("Token");
    constructor(private http:HttpClient) {} 
    public updateSite = new BehaviorSubject<any>(false);
    httpheader = new HttpHeaders(
        {
        'Authorization' : 'Bearer ' + localStorage.getItem("Token"),
        'Content-Type' : 'application/json'
        }
    )

    getAllBooks():Observable<Book[]>{
        return this.http.get<Book[]>(this.baseUrl + 'api/BooksDetails/GetAllBooks',{headers : this.httpheader});
    }
    getBookbyId(bookid:any):Observable<Book[]>{
        return this.http.get<Book[]>( this.baseUrl + 'api/BooksDetails/GetBookWithId/'+ bookid,{headers:this.httpheader})
    }

    addBook(book: Book):Observable<Book[]>{
        return this.http.post<Book[]>(this.baseUrl + 'api/BooksDetails/AddBook',book,{headers : this.httpheader});
    }

    GetAllPublishers():Observable<Book[]>{
        return this.http.get<Book[]>( this.baseUrl + 'api/BooksDetails/GetAllPublishers',{headers : this.httpheader});
    }
    SearchBooks(booksSearchCriteria : BooksSearchCriteria):Observable<Book[]>{
        return this.http.post<Book[]>(this.baseUrl + 'api/BooksDetails/SearchBookByCriteria',booksSearchCriteria,{headers : this.httpheader});
    }
    GetBooksByAuthorId(getBooksByAuthor :GetBooksByAuthor):Observable<Book[]>{
        return this.http.post<Book[]>(this.baseUrl + 'api/BooksDetails/GetAuthorBooks',getBooksByAuthor,{headers : this.httpheader});
    }
    UpdateBooks(book:Book):Observable<Book[]>{
        return this.http.put<Book[]>(this.baseUrl + 'api/BooksDetails/ModifyBook',book,{headers:this.httpheader})
    }

    



}