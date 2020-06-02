import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Concert } from 'src/app/shared/models/concert.model';
import { CartService } from 'src/app/modules/cart/services/cart.service';
import { TicketType } from 'src/app/shared/models/ticket-type.model';

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
              this.concert = data.concert;
            }
          );
    }

    ngOnInit(): void { }


    addToCart(ticketType: TicketType, quantity: number){
      this.cartService.addToCart(ticketType, quantity);
    }
}
