import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-concert-grid-item',
    templateUrl: './concert-grid-item.component.html',
    styleUrls: ['./concert-grid-item.component.scss']
})
export class ConcertGridItemComponent implements OnInit {

    backgroundImage = "";

    constructor() { }

    ngOnInit(): void { 
        this.backgroundImage = `https://cloudbash-frontend.s3.amazonaws.com/concer_placehold_${this.getRandomInt(1,11)}.jpg`;
    }

    get backgroundImageUrl() {
        if (this.backgroundImage) {
          return `url("${this.backgroundImage}")`;
       }
     
       return null
     }


     getRandomInt(min, max) {
        min = Math.ceil(min);
        max = Math.floor(max);
        return Math.floor(Math.random() * (max - min)) + min; 
      }
     
}
