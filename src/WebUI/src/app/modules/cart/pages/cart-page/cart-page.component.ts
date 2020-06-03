import { Component, OnInit } from '@angular/core';
import { Cart, CartItem } from 'src/app/shared/models/cart.model';
import { CartService } from '../../services/cart.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-cart-page',
    templateUrl: './cart-page.component.html',
    styleUrls: ['./cart-page.component.scss']
})
export class CartPageComponent implements OnInit {

    cart: Cart;
    busy = false;
    selected: CartItem = null;

    constructor(private cartService: CartService) {}

    ngOnInit(): void {
        if(!this.cart){
            this.cartService.getCart().subscribe(cart => {
                this.cart = cart;
            });
        }
       
     }

    removeFromCart(item: CartItem) {
        this.busy = true;
        this.selected = item;
        this.cartService.removeFromCart(item).subscribe(res => {
            this.busy = false;
            this.selected = null;
        }, err => {
            this.busy = false;
            this.selected = null;
        });
    }
}
