import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from 'src/app/shared/services/config.service';
import { Observable, BehaviorSubject, Subject } from 'rxjs';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { Cart, CartItem } from 'src/app/shared/models/cart.model';
import { TicketType } from 'src/app/shared/models/ticket-type.model';

@Injectable({
    providedIn: 'root',
  })
export class CartService {
    private cartItemCountSubject: Subject<number> = new BehaviorSubject(0);
    private cartItemCount = 0;

    private cartSubject: Subject<Cart> = new BehaviorSubject(new Cart());
    private cart = new Cart();

    private _cartURL: string;
    private userId: string;


    constructor(private http: HttpClient,
                private configService: ConfigService,
                private authService: AuthenticationService) {
        this._cartURL = this.configService.getApiBaseUrl() + '/carts/';
                     console.log("load data")
        this.cartSubject.subscribe(_ => this.cart = _);
        this.authService.loggedIn.subscribe(state => {            
            if(state) {
                this.userId = this.authService.user['attributes']['sub'];
                this.loadCart(this.userId);
            }
        });
    }


    private loadCart(userid: string) {
        this.get().subscribe(res => {
            console.log(res);
            this.cart = res;       
            this.cartSubject.next(res);
            this.cartItemCountSubject.next(this.cart.Items.length);
        });
    }

    private get(): Observable<Cart> {
        return this.http.get<any>(this._cartURL + this.userId);
    }
    
    public addToCart(ticketType: TicketType, quantity: number) {
        const url = `${this._cartURL}${this.userId}/item`;
        const command = { CartId : this.cart.Id, TicketTypeId: ticketType.Id, Quantity: quantity };
        return this.http.post<any>(url, command).subscribe(res => {
            let item = new CartItem();
            item.Id = res;
            item.Quantity = quantity;
            item.TicketType = ticketType;
            this.cart.Items = [...this.cart.Items, item];
            this.cartSubject.next(this.cart);
            this.cartItemCountSubject.next(this.cart.Items.length);
        });        
    }

    public removeFromCart(item: CartItem) {
        const url = `${this._cartURL}${this.userId}/item/remove`;
        const command = { CartId : this.cart.Id, CartItemId: item.Id };
        return this.http.post<any>(url, command).subscribe(res => {  
            this.cart.Items = this.cart.Items.filter(i =>  i.Id !== item.Id )          
            this.cartSubject.next(this.cart);
            this.cartItemCountSubject.next(this.cart.Items.length);
        });        
    }

    public getCart(): Observable<Cart> {
        return this.cartSubject.asObservable();
    }

    public getCartItemCount(): Observable<number> {
        return this.cartItemCountSubject.asObservable();
    }
}