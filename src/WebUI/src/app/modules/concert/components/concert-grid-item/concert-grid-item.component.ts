import { Component, OnInit, Input } from '@angular/core';
import { Concert } from 'src/app/shared/models/concert.model';

@Component({
    selector: 'app-concert-grid-item',
    templateUrl: './concert-grid-item.component.html',
    styleUrls: ['./concert-grid-item.component.scss']
})
export class ConcertGridItemComponent implements OnInit {
    @Input() concert: Concert;
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
