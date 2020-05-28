import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Concert } from 'src/app/shared/models/concert.model';
import { CartService } from 'src/app/modules/cart/services/cart.service';

@Component({
    selector: 'app-concert-single-page',
    templateUrl: './concert-single-page.component.html',
    styleUrls: ['./concert-single-page.component.scss']
})
export class ConcertSinglePageComponent implements OnInit {
    concert: Concert;

    constructor(private route: ActivatedRoute,
                private cartService: CartService) {

        this.route.data.subscribe(
            (data: { concert: Concert }) => {
            console.log(data);
              this.concert = data.concert;
            }
          );
    }

    ngOnInit(): void { }


    addToCart(id: string, quantity: number){
      this.cartService.addToCart(id, quantity);
    }
}
