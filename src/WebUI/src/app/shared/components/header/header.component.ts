import { Component, OnInit, HostListener } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { CartService } from 'src/app/modules/cart/services/cart.service';
import { Router } from '@angular/router';

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
                public cartService: CartService,
                private router: Router) { }

    ngOnInit(): void { 
        this.cartService.getCartItemCount().subscribe(count => {
            this.cartItemCount = count;
        });
    }

    signOut() {
        this.authService.signOut();
    }

    search(term: string, time: string) {
        if(term){
            this.router.navigate(['/concert/search/'], { queryParams: { q: term, t: time } });
        }        
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
