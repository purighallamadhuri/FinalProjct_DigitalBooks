import { Component } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { BooksService } from './service/books.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Digitalbooks';
  hidesignupmenu: Boolean =false;
  hideloginmenu: Boolean =false;
  private UnsubcsribeAll = new Subject();
  loginDetails : any = '';
  userName: any ='';
  userType: any ='';

  constructor(private booksService:BooksService){}

  ngOnInit(): void {
   this.HideNavMenu();
   this.booksService.updateSite.pipe(takeUntil(this.UnsubcsribeAll)).subscribe((r: any) => {
     this.loginDetails = localStorage.getItem("UserId");
     console.log("Digital");
     console.log(this.loginDetails);
     this.userName = localStorage.getItem("UserName");
     this.userType = localStorage.getItem("UserType");
    });
  }
  ngOnDestroy(){
    this.UnsubcsribeAll.next(false);
    this.UnsubcsribeAll.complete();
  }
  HideMainbar(){
    this.hidesignupmenu = true;
  }
  Logout(){
    localStorage.removeItem("Token");
    //localStorage.setItem("UserId", '');
    localStorage.removeItem("UserId");
    localStorage.removeItem("BookName");
    localStorage.removeItem("UserType");
    localStorage.removeItem("UserName");
    localStorage.removeItem("BookId");
    localStorage.removeItem("Price");
    localStorage.removeItem("UserEmailId");
    this.booksService.updateSite.next(true);
  }
  HideNavMenu(){
    console.log(localStorage.getItem("UserId"));
    if(localStorage.getItem("UserId") == null)
    {
      this.hidesignupmenu =false;
      this.hideloginmenu = true;
    }
    else
    {
      this.hidesignupmenu = false;
      this.hideloginmenu = true;
    }
  }

}

