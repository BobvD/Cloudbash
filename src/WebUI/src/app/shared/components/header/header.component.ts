import { Component, OnInit, HostListener } from '@angular/core';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
    
    searchTerm: string = "";
    scrolled = false;

    constructor() { }

    ngOnInit(): void { }

    @HostListener("window:scroll", [])
    onWindowScroll() {
  
      const number = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;
      if (number > 330) {
        this.scrolled = true;
      } else {
          this.scrolled = false;
      }
  
    }

  
}
