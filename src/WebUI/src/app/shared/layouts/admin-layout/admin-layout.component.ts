import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
    selector: 'app-admin-layout',
    templateUrl: './admin-layout.component.html',
    styleUrls: ['./admin-layout.component.scss']
})
export class AdminLayoutComponent implements OnInit {
    public isCollapsed = true;
    constructor(public authService: AuthenticationService) { }

    ngOnInit(): void { }

    signOut() {
        this.authService.signOut();
    }
}
