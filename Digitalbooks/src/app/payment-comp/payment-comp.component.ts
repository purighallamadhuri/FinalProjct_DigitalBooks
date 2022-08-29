import { Component, OnInit,TemplateRef } from '@angular/core';
import { PaymentService } from '../service/payment.service';
import { PaymentDetails, UserPayments,RefundUserPayment } from '../model/paymentmodel';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { BooksService } from '../service/books.service';
import { Book } from '../model/booksmodel';
import { Router } from '@angular/router';
const pdfMake = require('pdfmake/build/pdfmake.js');
import * as pdfFonts from "pdfmake/build/vfs_fonts.js";
(pdfMake as any).vfs = pdfFonts.pdfMake.vfs;


@Component({
  selector: 'app-payment-comp',
  templateUrl: './payment-comp.component.html',
  styleUrls: ['./payment-comp.component.css']
})
export class PaymentCompComponent implements OnInit {

  userPayments: UserPayments[]=[];
  userPayment: UserPayments={
    EmailId : localStorage.getItem("BuyerEmailId")
  }
  showEmailField: Boolean =false;
  emptyshowEmailField: Boolean = false;
  modalRef?: BsModalRef;
  payDetails:PaymentDetails[]=[];
  paymentDetails:any;
  refundPayment:RefundUserPayment={
    Payment_Id : ''
  }
  bookdetails: any;
  showInvoice:Boolean=false;
  showContent:Boolean=false;
  userEmailId:any;
  showGrid: Boolean = false;
  constructor(private paymentService : PaymentService,private router: Router,private modalService: BsModalService,private toaster:ToastrService,private booksService: BooksService) { }
  Refund(payment: any){
    console.log(payment.payment_Id)
    this.refundPayment.Payment_Id=payment.payment_Id;
    this.paymentService.RefundPayment(this.refundPayment).subscribe(
      response=>{
        console.log(response);
        this.toaster.success("Refund Successfull","Success");
        this.ShowPayments();
      },
      error=>{
        console.log(error);
        this.toaster.error(error,"Failed");
      }
    )
  }
  OpenContent(payment:any){
    console.log(payment.book_Id);
    //localStorage.setItem("CurrBookId",payment.book_Id);
    this.booksService.getBookbyId(payment.book_Id).subscribe(
      Response=>{
        this.bookdetails = Response;
        console.log(this.bookdetails);
        console.log(this.bookdetails.content)
        localStorage.setItem("BookContent",this.bookdetails.content);
        //this.router.navigate(['/Content']);
        //window.location = "#/test.html";
        this.generatePDF("open",this.bookdetails.content);
      }
    )
  }
  generatePDF(action = "download", Content:any) {
    let docDefinition = {  
      content: [  
          // Previous configuration  
          {  
              text: 'Book Content',  
              style: 'sectionHeader'  
          }  ,
          {  
            columns: [                  
                [  
                    {  
                        text: `Content: ${Content}`  
                    }
                ]  
            ]  
          }
      ],  
      styles: {  
          sectionHeader: {  
              bold: true,  
              decoration: 'underline',  
              fontSize: 14,  
              margin: [0, 15, 0, 15]  
          }  
      }  
  }

    if(action==='download'){
      pdfMake.createPdf(docDefinition).download();
    }else if(action === 'print'){
      pdfMake.createPdf(docDefinition).print();      
    }else{
      pdfMake.createPdf(docDefinition).open();      
    }

  } 
  openModal(template: TemplateRef<any>,payment : any) {
    this.paymentDetails = payment;
    this.modalRef = this.modalService.show(template,payment);
  }
  ngOnInit(): void {
    //this.ShowPayments();
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
  }

  ShowPayments(){
    this.userPayment.EmailId = this.userEmailId;
    this.paymentService.GetAllPayments(this.userPayment).subscribe(
      response=>{
        console.log(response);
        this.showGrid=true;
        this.payDetails = response;
        console.log(this.payDetails);
      }
    )
  }

}
