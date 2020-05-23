import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';

@Component({
    selector: 'app-auth-page',
    templateUrl: './auth-page.component.html',
    styleUrls: ['./auth-page.component.scss']
})
export class AuthPageComponent implements OnInit {
    signUpConfig = {
        header: 'Sign up',
        hideAllDefaults: true,
        defaultCountryCode: '1',
        signUpFields: [
            {
                label: 'Full name',
                key: 'name',
                required: true,
                displayOrder: 1,
                type: 'string'
            },
            {
                label: 'Email',
                key: 'username',
                required: true,
                displayOrder: 2,
                type: 'string'
            },
            {
                label: 'Password',
                key: 'password',
                required: true,
                displayOrder: 3,
                type: 'password'
            }
        ]
    }


    constructor(private authService: AuthenticationService) { }

    ngOnInit(): void { }
}
