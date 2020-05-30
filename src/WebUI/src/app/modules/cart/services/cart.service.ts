import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from 'src/app/shared/services/config.service';
import { Observable, BehaviorSubject } from 'rxjs';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { Cart, CartItem } from 'src/app/shared/models/cart.model';

@Injectable({
    providedIn: 'root',
  })
export class CartService {

    private itemsInCartSubject: BehaviorSubject<CartItem[]> = new BehaviorSubject([]);
    private itemsInCart: CartItem[] = [];

    private _cartURL: string;
    private userId: string;
    private cart: Cart;


    constructor(private http: HttpClient,
                private configService: ConfigService,
                private authService: AuthenticationService) {
        this._cartURL = this.configService.getApiBaseUrl() + '/carts/';
       
        this.itemsInCartSubject.subscribe(_ => this.itemsInCart = _);
        this.authService.loggedIn.subscribe(state => {            
            if(state) {
                this.userId = this.authService.user['attributes']['sub'];
                this.loadCartItems(this.userId);
            }
        });
    }

    loadCartItems(userid: string) {
        this.get().subscribe(res => {
            this.cart = res;
            res.Items.forEach(element => {
                // this.addToCart(element);
            });
        });
    }

    get(): Observable<Cart> {
        return this.http.get<any>(this._cartURL + this.userId);
    }
    
    public addToCart(ticketTypeId: string, quantity: number) {
        const url = `${this._cartURL}${this.userId}/item`;
        const command = { CartId : this.cart.Id, TicketTypeId: ticketTypeId, Quantity: quantity };
        return this.http.post<any>(url, command).subscribe(res => {
            let item = new CartItem();
            item.Id = res;
            item.Quantity = quantity;
            item.TicketTypeId = ticketTypeId;
            this.itemsInCartSubject.next([...this.itemsInCart, item]);
        });        
    }

    public getItems(): Observable<CartItem[]> {
        return this.itemsInCartSubject;
    }

}