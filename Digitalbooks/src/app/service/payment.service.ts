import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import { PaymentDetails,UserPayments,RefundUserPayment } from '../model/paymentmodel';

@Injectable({
    providedIn: 'root'
  })

  export class PaymentService {
    //baseUrl = 'https://localhost:7280/';
    baseUrl = 'https://readerapi20220828224547.azurewebsites.net/';
  
    constructor(private http: HttpClient) { }
    token = localStorage.getItem("Token");

    httpheader = new HttpHeaders(
        {
        'Authorization' : 'Bearer ' + localStorage.getItem("Token"),
        'Content-Type' : 'application/json'
        }
    )
    ProceedPayment(paymentDetails:PaymentDetails):Observable<any>{
        return this.http.post<any>(this.baseUrl +'api/PaymentDetails/CompletePayment',paymentDetails,{headers:this.httpheader});
    }
    GetAllPayments(userPayments : UserPayments):Observable<any>{
      return this.http.post<any>(this.baseUrl+'api/PaymentDetails/GetAllPayments',userPayments,{headers :this.httpheader})
    }
    RefundPayment(refundUserPayment:RefundUserPayment):Observable<any>{
      return this. http.post<any>(this.baseUrl+'api/PaymentDetails/RefundPayment',refundUserPayment,{headers:this.httpheader});
    }
  }