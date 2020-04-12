import { Component, OnInit, HostListener } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
    
    searchTerm: string = "";
    scrolled = false;

    constructor(public authService: AuthenticationService) { }

    ngOnInit(): void { }

    signOut() {
        this.authService.signOut();
    }

    @HostListener("window:scroll", [])
    onWindowScroll() {
  
      const number = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;
      if (number > 300) {
        this.scrolled = true;
      } else {
          this.scrolled = false;
      }
  
    }

  

    
}
