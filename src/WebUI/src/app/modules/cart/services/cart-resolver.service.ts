import { Injectable, } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';

import { catchError } from 'rxjs/operators';
import { CartService } from './cart.service';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { Cart } from 'src/app/shared/models/cart.model';

@Injectable()
export class CartResolver implements Resolve<Cart> {
  constructor(
    private cartService: CartService,
    private router: Router
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {


    return this.cartService.get();


    /*
    return this.concertService.getById(route.params['id'])
      .pipe(catchError((err) => this.router.navigateByUrl('/')));
      */
  }
}