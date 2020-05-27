import { Component, OnInit, Input } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
    @Input() scrolled = false;
    
    public isCollapsed = true;
    constructor(public authService: AuthenticationService) { }

    ngOnInit(): void { 
        console.log(this.authService.user);
    }

    signOut() {
        this.authService.signOut();
    }

}
