import { Component, OnInit } from '@angular/core';
import { LoginUser } from '../model/usersmodel';
import { UserService } from '../service/users.service';
import { Router } from '@angular/router';
import { Users } from '../model/usersmodel';
import { ToastrService } from 'ngx-toastr';
import { BooksService } from '../service/books.service';

@Component({
  selector: 'app-login-comp',
  templateUrl: './login-comp.component.html',
  styleUrls: ['./login-comp.component.css']
})
export class LoginCompComponent implements OnInit {

  title = 'users';
  loginuser:LoginUser[] = [];
  loggedinuser : LoginUser = {
    UserName:'',
    Password:''
  }
  token: any;
  users:Users[]=[];
  showSuccMsg: Boolean = false;

  constructor(private userservice: UserService,private router:Router,private toaster:ToastrService,private bookService: BooksService) {}

  ngOnInit(): void {
  }
  onSubmit(){
    if(this.loggedinuser.UserName != '' && this.loggedinuser.Password != ''){
      this.userservice.UserLogin(this.loggedinuser)
      .subscribe(
        response => {
          this.token = response;
          console.log(this.token.token);
          this.toaster.success("Login Successful!!", "Success");
          //localStorage.setItem("Token",'');
           localStorage.setItem("Token", this.token.token);
           console.log("Bearer " + this.token.token);
          this.userservice.GetLoginUserDtls(this.loggedinuser).subscribe(
            data => {
              this.users=data;
              localStorage.setItem("UserId", (this.users[0].user_Id).toString());
              localStorage.setItem("UserName",this.users[0].userName);
              localStorage.setItem("UserType",this.users[0].user_Type);
              localStorage.setItem("UserEmailId",this.users[0].emailId);
              this.bookService.updateSite.next(true);
              if(this.users[0].user_Type == "Author"){
              this.router.navigate(['/author']);
              }
              else this.router.navigate(['/reader']);
            }
          )
          
        },Error => {
          //this.showSuccMsg = false;
          console.log("failed");
          this.toaster.error("Login Failed!!", "Failed")
        }
      );
    }
    // else{
    //   this.updateCard(this.card);
    // }    
  }
}
