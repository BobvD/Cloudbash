import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-concert-grid-item',
    templateUrl: './concert-grid-item.component.html',
    styleUrls: ['./concert-grid-item.component.scss']
})
export class ConcertGridItemComponent implements OnInit {

    backgroundImage = '/assets/images/concert-1.jpg';

    constructor() { }

    ngOnInit(): void { }

    get backgroundImageUrl() {
        if (this.backgroundImage) {
          return `url("${this.backgroundImage}")`;
       }
     
       return null
     }
     
}
