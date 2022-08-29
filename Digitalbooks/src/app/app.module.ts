import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule} from '@angular/common/http';
import { FormsModule } from '@angular/forms'; 
import { AppComponent } from './app.component';
import { BooksCompComponent } from './books-comp/books-comp.component';
import { LoginCompComponent } from './login-comp/login-comp.component';
import { AuthorCompComponent } from './author-comp/author-comp.component';
import { AppRoutingModule} from './app-routing.module';
import { RegisterCompComponent } from './register-comp/register-comp.component';
import { RouterModule} from '@angular/router';
import { HomeCompComponent} from './home-comp/home-comp.component';
import { PaymentCompComponent } from './payment-comp/payment-comp.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AddbooksCompComponent } from './addbooks-comp/addbooks-comp.component';
import { ModalModule } from 'ngx-bootstrap/modal';

@NgModule({
  declarations: [
    AppComponent,
    BooksCompComponent,
    LoginCompComponent,
    AuthorCompComponent,
    RegisterCompComponent,
    PaymentCompComponent,
    AddbooksCompComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    RouterModule.forRoot([
      {path:'',component : BooksCompComponent},
      {path:'login',component : LoginCompComponent},
      {path:'register',component : RegisterCompComponent},
      {path:'author',component : AuthorCompComponent},
      {path:'reader',component : BooksCompComponent},
      {path:'payment',component : PaymentCompComponent},
      {path:'addbook',component : AddbooksCompComponent}
    ]),
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    ModalModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
