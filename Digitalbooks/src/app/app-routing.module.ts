import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginCompComponent } from './login-comp/login-comp.component';
// import {UserDetailsCompComponent} from './userDetails-comp/userDetails-comp.component';
import {AppComponent} from './app.component';
import { AuthorCompComponent } from './author-comp/author-comp.component';
import { RegisterCompComponent } from './register-comp/register-comp.component';
import {BooksCompComponent} from'./books-comp/books-comp.component';



const routes: Routes = [
  {path: 'login-comp', component: LoginCompComponent} ,
  {path:'author-comp',component:AuthorCompComponent},
  {path:'register-comp',component:RegisterCompComponent},
  {path:'books-comp',component:BooksCompComponent}
];



@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }