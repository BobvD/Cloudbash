import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { tap } from 'rxjs/internal/operators/tap';

@Injectable({
    providedIn: 'root',
  })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private auth: AuthenticationService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return this.auth.isAuthenticated()
            .pipe(
                tap(loggedIn => {
                    if (!loggedIn) {
                        this.router.navigate(['/sign-in']);
                    } else {
                        const role = this.auth.currentUserRole;
                        if (route.data.roles && route.data.roles.indexOf(role) === -1) {
                            // role not authorised so redirect to home page
                            this.router.navigate(['/sign-in']);
                            return false;
                        }
                        // authorised so return true
                        return true;
                    }
                })
            );
    }
}