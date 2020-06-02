import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { I18n } from 'aws-amplify';
import { Router } from '@angular/router';


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


    constructor(private authService: AuthenticationService,
        private router: Router) { }

    ngOnInit(): void {
        I18n.setLanguage('en');
        const dict = {
            'en': {
                'Username': 'Email'
            }
        }
        I18n.putVocabularies(dict);

        this.authService.loggedIn.subscribe(res => {
            if (res === true) {
                this.router.navigate(['/']);
            }
        })
    }
}
