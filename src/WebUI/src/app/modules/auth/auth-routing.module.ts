import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SignInPageComponent } from './pages/signin-page/signin-page.component';


const routes: Routes = [
    { path: 'sign-in', component: SignInPageComponent },
];

@NgModule({
    imports: [CommonModule, RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AuthRoutingModule { }