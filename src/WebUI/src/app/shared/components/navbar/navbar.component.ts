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
    
    public isCollapsed = true;
    constructor(public authService: AuthenticationService,
                public cartService: CartService) { }

    ngOnInit(): void { 
    }

    signOut() {
        this.authService.signOut();
    }

}
