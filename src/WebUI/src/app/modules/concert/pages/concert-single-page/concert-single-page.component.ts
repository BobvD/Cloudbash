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
    busy = false;
    selected: TicketType = null;
    showCartButton = false;

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
      this.busy = true;
      this.selected = ticketType;
      this.cartService.addToCart(ticketType, quantity).subscribe(res => {
        this.busy = false;
        this.selected = null;
        this.showCartButton = true;
      })
    }

    checkIfCartContainsTicketType(type: TicketType){
      if(this.cartService.checkIfCartContainsItem(type)){
        this.showCartButton = true;
        return true;
      }
      return false;
    }
}
