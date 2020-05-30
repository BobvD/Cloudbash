import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CartPageComponent } from './pages/cart-page/cart-page.component';
import { CartResolver } from './services/cart-resolver.service';


const routes: Routes = [
    { path: '', component: CartPageComponent, pathMatch: 'full',  resolve: { cart: CartResolver } },
];

@NgModule({
    imports: [CommonModule, RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CartRoutingModule { }