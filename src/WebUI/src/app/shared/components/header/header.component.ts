import { Component, OnInit, HostListener } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { CartService } from 'src/app/modules/cart/services/cart.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

   cartItemCount = 0;
    searchTerm: string = "";
    scrolled = false;

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

    @HostListener("window:scroll", [])
    onWindowScroll() {
  
      const number = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;
      if (number > 300) {
        this.scrolled = true;
      } else {
          this.scrolled = false;
      }
  
    }

}
