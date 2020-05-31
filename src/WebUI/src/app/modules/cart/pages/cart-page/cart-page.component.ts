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
    
    
    constructor(private route: ActivatedRoute,
        private cartService: CartService) {

        this.route.data.subscribe(
            (data: { cart: Cart }) => {
            this.cart = data.cart;
            }
        );
    }


    ngOnInit(): void { }

    removeFromCart(item: CartItem) {
        this.cartService.removeFromCart(item);
    }
}
