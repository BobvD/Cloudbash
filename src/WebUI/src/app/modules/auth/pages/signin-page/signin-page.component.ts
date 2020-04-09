import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';

@Component({
    selector: 'app-signin-page',
    templateUrl: './signin-page.component.html',
    styleUrls: ['./signin-page.component.scss']
})
export class SignInPageComponent implements OnInit {
    
    constructor(private authService: AuthenticationService) { }

    ngOnInit(): void { }
}
