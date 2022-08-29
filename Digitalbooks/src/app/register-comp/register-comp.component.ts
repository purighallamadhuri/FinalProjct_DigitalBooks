import { Component, OnInit } from '@angular/core';
import { Users } from '../model/usersmodel';
import { UserService } from '../service/users.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register-comp',
  templateUrl: './register-comp.component.html',
  styleUrls: ['./register-comp.component.css']
})
export class RegisterCompComponent implements OnInit {
 users:Users[]=[];
 user : Users ={
  user_Id: 0,
  userName: '',
  password:'',
  user_Type:'',
  created_On: new Date(),
  emailId:'',
  mobile:''
 }
 showSuccMsg:Boolean =false;
  constructor(private userservice: UserService, private route:Router,private toaster:ToastrService) { }

  ngOnInit(): void {
  }
  AddUser(){
    console.log(this.user);
    if(this.user.emailId != '' && this.user.password != '' && this.user.user_Type != ''){
      this.userservice.AddUser(this.user).subscribe(
        response=>{
          console.log(response);
          this.toaster.success("Registration Successful!!", "Success");
          this.route.navigate(['/login']);
        },error =>{
          this.toaster.error("Registration Failed", "Failed");
          //console.log("error");
        }
      );
    }
  }
}
