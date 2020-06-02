import { Component, OnInit, Input } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { CartService } from 'src/app/modules/cart/services/cart.service';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
    @Input() scrolled = false;
    
    cartItemCount = 0;

    public isCollapsed = true;
    constructor(public authService: AuthenticationService,
                public cartService: CartService) { }

    ngOnInit(): void { 
        this.cartService.getCartItemCount().subscribe(count => {
            this.cartItemCount = count;
        });
    }

    signOut() {
        this.authService.signOut();
    }

}
