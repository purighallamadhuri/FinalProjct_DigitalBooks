export interface PaymentDetails{
    payment_Id : string,
    emailId : string,
    User_Id : number,
    book_Id : string,
    payment_Date : Date,
    transaction_Id : string,
    payment_Mode : string,
    bookName : string,
    price: number

}
export interface UserPayments{
    EmailId : string | null
}
export interface RefundUserPayment{
    Payment_Id : string
}