import { Observable, BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { AmplifyService } from 'aws-amplify-angular';
import { Auth } from 'aws-amplify';
import { fromPromise } from 'rxjs/observable/fromPromise';
import { map, catchError } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';
import { Router } from '@angular/router';
import { Role } from '../models/user.model';

@Injectable({
    providedIn: 'root',
  })
export class AuthenticationService {
    public loggedIn: BehaviorSubject<boolean>;
    signedIn: boolean;
    public user: any;
    public currentUserRole: Role;

    constructor(private amplifyService: AmplifyService,
        private router: Router) {
        this.loggedIn = new BehaviorSubject<boolean>(false);
        this.amplifyService.authStateChange$
            .subscribe(authState => {
                this.signedIn = authState.state === 'signedIn';
                if (!authState.user) {
                    this.user = null;
                } else {
                    this.user = authState.user;
                    this.currentUserRole = this.getRole(authState.user);
                    // this.router.navigate(['/']);
                }
            });
    }

    /** get authenticat state */
    public isAuthenticated(): Observable<boolean> {
        return fromPromise(Auth.currentAuthenticatedUser())
            .pipe(
                map(result => {
                    this.loggedIn.next(true);
                    return true;
                }),
                catchError(error => {
                    this.loggedIn.next(false);
                    return of(false);
                })
            );
    }

    /** signout */
    public signOut() {
        fromPromise(Auth.signOut())
            .subscribe(
                result => {
                    this.loggedIn.next(false);
                    this.router.navigate(['/log-in']);
                },
                error => console.log(error)
            );
    }

    get isAdmin() {
        return true;
    }

    public getRole(user: any): Role {
        const roles: any[] = user.signInUserSession
            .idToken.payload['cognito:groups'];
        if (roles) {
            if (roles[0] == 'Admin') {
                return Role.Admin;
            } else {
                return Role.User;
            }
        }
    }


}