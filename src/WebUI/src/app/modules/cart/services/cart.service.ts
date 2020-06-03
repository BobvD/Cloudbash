import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from 'src/app/shared/services/config.service';
import { Observable, BehaviorSubject, Subject } from 'rxjs';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { Cart, CartItem } from 'src/app/shared/models/cart.model';
import { TicketType } from 'src/app/shared/models/ticket-type.model';
import { map, tap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root',
})
export class CartService {
    private cartItemCountSubject: Subject<number> = new BehaviorSubject(0);
    private cartItemCount = 0;

    private cartSubject: Subject<Cart> = new BehaviorSubject(new Cart());
    private cart = null;

    private _cartURL: string;
    private userId: string;


    constructor(private http: HttpClient,
        private configService: ConfigService,
        private authService: AuthenticationService,
        private router: Router) {
        this._cartURL = this.configService.getApiBaseUrl() + '/carts/';
        this.cartSubject.subscribe(_ => this.cart = _);
        this.authService.loggedIn.subscribe(state => {
            if (state && this.cartItemCount == 0) {
                console.log('load new cart')
                this.userId = this.authService.user['attributes']['sub'];
                this.loadCart();
            }
        });
    }

    public checkIfCartContainsItem(ticketType: TicketType) {
        return this.cart.Items.some(e => e.TicketType.Id === ticketType.Id);
    }

    public loadCart() {
        this.get().subscribe(res => {
            this.cart = res;
            this.cartSubject.next(res);
            this.cartItemCountSubject.next(this.cart.Items.length);
        });
    }

    private get(): Observable<Cart> {
        return this.http.get<any>(this._cartURL + this.userId);
    }

    public addToCart(ticketType: TicketType, quantity: number): Observable<any> {
        if (!this.authService.signedIn) {
            this.router.navigate(['sign-in']);
            return null;
        }
        const url = `${this._cartURL}${this.userId}/item`;
        const command = { CartId: this.cart.Id, TicketTypeId: ticketType.Id, Quantity: quantity };
        return this.http.post<any>(url, command)
            .pipe(map(id => {
                console.log(id);
                let item = new CartItem();
                item.Id = id;
                item.Quantity = quantity;
                item.TicketType = ticketType;
                console.log(item);
                this.cart.Items = [...this.cart.Items, item];
                this.cartSubject.next(this.cart);
                this.cartItemCount = this.cart.Items.length;
                this.cartItemCountSubject.next(this.cartItemCount);
                tap()
            }));
    }

    public removeFromCart(item: CartItem) {
        const url = `${this._cartURL}${this.userId}/item/remove`;
        const command = { CartId: this.cart.Id, CartItemId: item.Id };
        return this.http.post<any>(url, command)
            .pipe(map(res => {
                this.cart.Items = this.cart.Items.filter(i => i.Id !== item.Id)
                this.cartSubject.next(this.cart);
                this.cartItemCount = this.cart.Items.length;
                this.cartItemCountSubject.next(this.cartItemCount);
                tap()
            }));
    }

    public getCart(): Observable<Cart> {
        return this.cartSubject;
    }

    public getCartItemCount(): Observable<number> {
        return this.cartItemCountSubject.asObservable();
    }
}