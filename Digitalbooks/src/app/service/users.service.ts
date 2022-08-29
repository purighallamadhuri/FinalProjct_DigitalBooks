import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Users } from '../model/usersmodel';
import { LoginUser } from '../model/usersmodel';

@Injectable({
    providedIn: 'root'
  })

  export class UserService {

    //baseUrl = 'https://localhost:7287/';
    baseUrl = 'https://authorapi20220828121035.azurewebsites.net/';
  
    constructor(private http: HttpClient) { }
    token = localStorage.getItem("Token");

    httpheader = new HttpHeaders(
        {
        'Authorization' : 'Bearer ' + localStorage.getItem("Token"),
        'Content-Type' : 'application/json'
        }
    )
  
    //Get all cards
    UserLogin(loginuser: LoginUser):Observable<LoginUser[]>{
        return this.http.post<LoginUser[]>('https://authenticationapi20220829002022.azurewebsites.net/api/Authetication/Authenticate',loginuser,{headers : this.httpheader});
    }
    AddUser(users: Users): Observable<Users[]>{
      return this.http.post<Users[]>(this.baseUrl + 'api/AuthorDetails/AddUser',users,{headers : this.httpheader});
    }
    GetAllAuthors():Observable<Users[]>{
      return this.http.get<Users[]>(this.baseUrl + 'api/AuthorDetails/GetAuthors',{headers : this.httpheader});
    }
    GetLoginUserDtls(loginuser: LoginUser):Observable<Users[]>{
      return this.http.post<Users[]>(this.baseUrl + 'api/AuthorDetails/GetLoginUserDtls',loginuser,{headers : this.httpheader});
    }
}